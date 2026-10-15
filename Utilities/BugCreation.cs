using Newtonsoft.Json.Linq;
using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using RestSharp;
using System.Text;
using System.Text.RegularExpressions;

namespace PIMQAAutomation.Utilities
{
    public class BugCreation
    {

        private readonly string organization;
        private readonly string project;
        private readonly string testProject;
        private readonly string personalAccessToken;
        private readonly string areaPath;

        private readonly string alternatesOwner;
        private readonly string lobOwner;
        private readonly string bomOwner;
        private readonly string itemOwner;
        private readonly string adminOwner;
        private readonly string defaultOwner;

        private readonly string baseUri;
        private readonly string testBaseUri;
        private readonly string iterationBaseUri;
        private readonly string projectTeam;

        public BugCreation()
        {
            organization = TestContext.Parameters[RunSettingsConstants.ADOOrganization];
            project = TestContext.Parameters[RunSettingsConstants.BugProject];
            projectTeam = TestContext.Parameters[RunSettingsConstants.ProjectTeam];
            testProject = TestContext.Parameters[RunSettingsConstants.TestProject];
            personalAccessToken = SecureDataHelper.DecryptWithMachineKey(TestContext.Parameters[RunSettingsConstants.PIMQAAutomationPAT]);
            areaPath = TestContext.Parameters[RunSettingsConstants.BugAreaPath]; // "Item Management";

            alternatesOwner = TestContext.Parameters[RunSettingsConstants.AlternatesOwner];
            lobOwner = TestContext.Parameters[RunSettingsConstants.LOBOwner];
            bomOwner = TestContext.Parameters[RunSettingsConstants.BOMOwner];
            itemOwner = TestContext.Parameters[RunSettingsConstants.ItemOwner];
            adminOwner = TestContext.Parameters[RunSettingsConstants.AdminOwner];
            defaultOwner = TestContext.Parameters[RunSettingsConstants.DefaultOwner];

            string url = TestContext.Parameters[RunSettingsConstants.AzureUri];
            Console.WriteLine($"ADO URL - {url} ");
            baseUri = $"{url}{organization}/{project}/_apis";
            iterationBaseUri = $"{url}{organization}/{project}/{projectTeam}/_apis";
            testBaseUri = $"{url}{organization}/{testProject}/_apis";
        }

        public async Task<int> CreateBugAsync(string title, string description, string base64Image, string traceLogPath, string? moduleName)
        {
            try
            {
                string iterationPath = await GetIterationForCurrentDateAsync();
                Console.WriteLine($"Current Iteration is - {iterationPath}");

                var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}"));

                var testCaseId = GetTestCaseIdByTitle(title);
                Console.WriteLine($"TestCaseId: {testCaseId}");

                var attachments = new List<(string? Url, string? Comment)>();

                string? imageAttachUrl = string.Empty;
                if (!string.IsNullOrEmpty(traceLogPath))
                {
                    imageAttachUrl = UploadAttachmentAsync(Convert.FromBase64String(base64Image), testCaseId + ".jpg", authToken);
                    attachments.Add((imageAttachUrl, "Failure screenshot"));
                }

                string? traceLogUrl = string.Empty;
                if (!string.IsNullOrEmpty(traceLogPath))
                {
                    traceLogUrl = UploadAttachmentAsync(File.ReadAllBytes(traceLogPath), testCaseId + "_tracelog.zip", authToken);
                    attachments.Add((traceLogUrl, "Trace Log"));
                }

                // Get module owner to assign to Bug
                var bugOwner = string.Empty;

                switch (moduleName.Trim().ToUpper())
                {
                    case "ALTERNATIVES":
                        bugOwner = alternatesOwner;
                        break;
                    case "LOB":
                        bugOwner = lobOwner;
                        break;
                    case "BOM":
                        bugOwner = bomOwner;
                        break;
                    case "ITEMS":
                        bugOwner = itemOwner;
                        break;
                    case "ADMIN":
                        bugOwner = adminOwner;
                        break;
                    default:
                        bugOwner = defaultOwner;
                        break;
                }

                Console.WriteLine($"Module {moduleName} owner is {bugOwner}");

                var client = new RestClient(baseUri);
                var request = new RestRequest("wit/workitems/$Bug?api-version=7.1", Method.Patch);


                request.AddHeader("Authorization", $"Basic {authToken}");
                request.AddHeader("Content-Type", "application/json-patch+json");

                string safeProject = project.Replace("%20", " ");
                Console.WriteLine($"Area -  {safeProject}\\{areaPath}");

                var payload = new JArray
                    {
                        new JObject
                        {
                            { "op", "add" },
                            { "path", "/fields/System.Title" },
                            { "value", $"[AUTO] - {title}" }
                        },
                        new JObject
                        {
                            { "op", "add" },
                            { "path", "/fields/System.Description" },
                            { "value", description }
                        }
                         ,
                        new JObject
                            {
                                { "op", "add" },
                                { "path", "/relations/-" },
                                { "value", new JObject
                                    {
                                        { "rel", "System.LinkTypes.Hierarchy-Reverse" },
                                        { "url", $"{testBaseUri.Replace("_apis", string.Empty)}_apis/wit/workItems/{testCaseId}" }
                                    }
                                }
                            },
                        new JObject { { "op", "add" }, { "path", "/fields/System.AreaPath" }, { "value", $"{safeProject}\\{areaPath}" } },
                        new JObject { { "op", "add" }, { "path", "/fields/System.IterationPath" }, { "value", iterationPath } },
                        new JObject
                        {
                            ["op"] = "add",
                            ["path"] = "/fields/System.AssignedTo",
                            ["value"] = bugOwner
                        }
                    };

                foreach (var (urlVal, comment) in attachments )
                {
                    payload.Add(
                        new JObject
                            {
                                { "op", "add" },
                                { "path", "/relations/-" },
                                { "value", new JObject
                                    {
                                        { "rel", "AttachedFile" },
                                        { "url", urlVal },
                                        { "attributes", new JObject
                                            {
                                                { "comment", comment }
                                            }
                                        }
                                    }
                                }
                            }
                        );
                }

                string jsonBody = payload.ToString(Newtonsoft.Json.Formatting.None);
                request.AddStringBody(jsonBody, DataFormat.Json);

                Console.WriteLine("Sending Bug creation request to Azure DevOps...");
                var response = client.Execute(request);

                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Content: {response.Content}");

                if (!response.IsSuccessful)
                {
                    Console.WriteLine($"Bug creation failed: {response.ErrorMessage ?? response.Content}");
                    return 0;
                }
                
                    var json = JObject.Parse(response.Content);
                return int.Parse(json["id"].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Exception in CreateBug: {ex.Message}");
            }
            return 0;
        }

        private static string GetTestCaseIdByTitle(string input)
        {
            // Match only numbers with 3 or more digits
            var match = Regex.Match(input, @"\d{3,}");
            return match.Success ? match.Value : string.Empty;
        }

        private string? UploadAttachmentAsync(byte[] fileBytes, string fileName, string pat)
        {
            string uploadUrl = $"wit/attachments?fileName={fileName}&api-version=7.1";

            var client = new RestClient(baseUri);
            var request = new RestRequest(uploadUrl, Method.Post);

            request.AddHeader("Authorization", $"Basic {pat}");
            request.AddHeader("Content-Type", "application/octet-stream");

            request.AddParameter("application/octet-stream", fileBytes, ParameterType.RequestBody);

            var response = client.Execute(request);
            var json = JObject.Parse(response.Content);
            return json["url"]?.ToString();
        }

        /// <summary>
        /// Checks if a Bug already exists with same Title or Description
        /// </summary>
        public async Task<int?> FindExistingBugAsync(string title, string description)
        {
            string safeTitle = SanitizeForWiql(title);
            string safeDescription = SanitizeForWiql(description);
            string safeProject = project.Replace("%20", " ");

            string wiql = $@"
            {{
                ""query"": ""SELECT [System.Id]
                            FROM WorkItems
                            WHERE [System.TeamProject] = '{safeProject}'
                            AND [System.WorkItemType] = 'Bug'
                            AND [System.State] <> 'Closed'
                            AND [System.Title] CONTAINS '{safeTitle}'""
            }}";

            Console.WriteLine($"WIQL - {wiql}");

            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}"));

            var client = new RestClient(baseUri);
            var request = new RestRequest("wit/wiql?api-version=7.1", Method.Post);
            request.AddHeader("Authorization", $"Basic {authToken}");
            request.AddHeader("Content-Type", "application/json");
            request.AddStringBody(wiql, DataFormat.Json);

            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                Console.WriteLine($"WIQL query failed: {response.Content}");
                return null;
            }
            var json = JObject.Parse(response.Content);
            var workItems = json["workItems"] as JArray;

            return (workItems != null && workItems.Count > 0)
                ? (int?)workItems[0]["id"]
                : null;
        }

        private static string SanitizeForWiql(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return string.Empty;
            }

            // 1️ Normalize whitespace: newlines, carriage returns, tabs → single space
            string sanitized = input.Replace("\r", " ")
                                    .Replace("\n", " ")
                                    .Replace("\t", " ");

            // 2️ Escape single quotes for WIQL
            sanitized = sanitized.Replace("'", "''");

            // 3️ Escape double quotes just to avoid confusion
            sanitized = sanitized.Replace("\"", "\"\"");

            // 4️ Remove control characters (non-printable)
            sanitized = new string(sanitized.Where(c => !char.IsControl(c)).ToArray());

            // 5️ Optional: trim very long strings (avoids WIQL 32KB limit)
            if (sanitized.Length > 800)
            {
                sanitized = sanitized.Substring(0, 800);
            }

            // 6️⃣ Trim extra spaces
            sanitized = sanitized.Trim();

            return sanitized;
        }

        private async Task<string> GetIterationForCurrentDateAsync()
        {
            var authToken = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{personalAccessToken}"));
            var currentProject = project.Replace("%20", " ");
            var client = new RestClient(iterationBaseUri);
            var request = new RestRequest("work/teamsettings/iterations?api-version=7.1", Method.Get);
            // var request = new RestRequest("wit/classificationnodes/iterations?$depth=10&api-version=7.1", Method.Get);

            request.AddHeader("Authorization", $"Basic {authToken}");
            var response = await client.ExecuteAsync(request);
            if (!response.IsSuccessful)
            {
                Console.WriteLine($"❌ Failed to get iterations: {response.Content}");
                return currentProject;
            }
            var json = JObject.Parse(response.Content);
            var iterations = json["value"]?.ToArray();

            if (iterations == null || iterations.Length == 0)
            {
                Console.WriteLine("⚠️ No iterations found for this team.");
                return currentProject;
            }
            else
            {
                DateTime today = DateTime.UtcNow.Date;
                string? iterationPath = null;

                // ✅ 1. Try to find current active iteration
                foreach (var iter in iterations)
                {
                    var attributes = iter["attributes"];
                    if (attributes == null)
                        continue;

                    DateTime? start = attributes["startDate"]?.ToObject<DateTime?>();
                    DateTime? end = attributes["finishDate"]?.ToObject<DateTime?>();

                    if (start.HasValue && end.HasValue && today >= start.Value.Date && today <= end.Value.Date)
                    {
                        iterationPath = iter["path"]?.ToString();
                        Console.WriteLine($"✅ Current iteration found: {iterationPath}");
                        break;
                    }
                }

                // ✅ 2. If today is weekend and no active iteration, pick next future iteration
                if (string.IsNullOrEmpty(iterationPath) && (today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday))
                {
                    var nextIter = iterations
                        .Select(i => new
                        {
                            Path = i["path"]?.ToString(),
                            Start = i["attributes"]?["startDate"]?.ToObject<DateTime?>()
                        })
                        .Where(x => x.Start.HasValue && x.Start.Value.Date > today)
                        .OrderBy(x => x.Start)
                        .FirstOrDefault();

                    if (nextIter != null)
                    {
                        iterationPath = nextIter.Path;
                        Console.WriteLine($"📅 Weekend detected — using next available iteration: {iterationPath}");
                    }
                }

                // ✅ 3. If still none, use last available iteration
                if (string.IsNullOrEmpty(iterationPath))
                {
                    iterationPath = iterations.LastOrDefault()?["path"]?.ToString();
                    if (!string.IsNullOrEmpty(iterationPath))
                    {
                        Console.WriteLine($"⚠️ No active or future iteration found — using last configured: {iterationPath}");
                    }
                }

                // ✅ 4. Final fallback to project name
                if (string.IsNullOrEmpty(iterationPath))
                {
                    iterationPath = project;
                    Console.WriteLine($"⚠️ No iteration found at all — using project as iteration: {iterationPath}");
                }

                return iterationPath;
            }

            // fallback: if no active iteration, return project
            Console.WriteLine($"⚠️ No current iteration found");
            return currentProject;
        }


    }
}