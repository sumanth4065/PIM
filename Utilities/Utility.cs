using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OfficeOpenXml;
using PIMQAAutomation.Modules.Common.Constants;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;

namespace PIMQAAutomation.Utilities
{
    public class Utility
    {
        private static readonly string ProjectPath = AppContext.BaseDirectory;
        private static readonly string TestDataPath = Path.Combine(Directory.GetParent(ProjectPath).Parent.Parent.Parent.FullName, FolderName.FolderResources, FolderName.JsonFileTestData);
        //path to the excel file
        public static readonly string ImportFilePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Resources\\");
        public Utility()
        {

        }

        //Method to Generate Current Working Directory of the Project
        public void CurrentWorkingDirectoryPath()
        {
            var currentDirectory = ProjectPath;
            Console.WriteLine(currentDirectory);
            var dir = new DirectoryInfo(currentDirectory + "\\FileDownloads");
            Console.WriteLine(dir);
            var filedownloadPath = dir.ToString();
            Console.WriteLine(filedownloadPath);
            try
            {
                if (dir.Exists)
                {
                    Console.WriteLine("Already Folder Exists..");
                }
                else
                {
                    dir.Create();
                    Console.WriteLine("Directory Created");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("unable to create folder" + ex.Message);
            }

            var di = new DirectoryInfo(filedownloadPath);

            foreach (var file in di.GetFiles())
            {
                Console.WriteLine(file.Name);
                file.Delete();
            }
        }

        public static string ReadDataFromJsonString(ScenarioContext scenarioContext, string keyPath)
        {
            var testData = FileUtil.BuildTestDataPath(scenarioContext);
            if (!File.Exists(testData))
            {
                throw new FileNotFoundException($"JSON file not found at: {testData}");
            }
            string json = File.ReadAllText(testData);

            JsonNode root = JsonNode.Parse(json);
            if (root is null)
            {
                throw new Exception("Failed to parse the JSON file.");
            }
            string[] keys = keyPath.Split('.');

            JsonNode currentNode = root;
            foreach (string key in keys)
            {
                if (currentNode?[key] is null)
                {
                    throw new KeyNotFoundException($"Key '{key}' not found in the JSON path: '{keyPath}'. Please check the JSON file.");
                }
                currentNode = currentNode[key];
            }
            return currentNode?.ToString() ?? throw new Exception($"Value for '{keyPath}' is null.");
        }

        //get the header data from the excel
        public static List<string> GetExcelHeaders(string filePath)
        {

            var headers = new List<string>();
            using (var package = new ExcelPackage(new FileInfo(filePath)))

            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                int columnCount = worksheet.Dimension.End.Column;
                for (int col = 1; col <= columnCount; col++)
                {
                    string header = worksheet.Cells[1, col].Text.Trim();
                    headers.Add(header);
                }
            }
            return headers;
        }

        // Method to validate if there is at least 1 valid row of data in the Excel sheet
        public static void ValidateDataRows(string filePath)
        {
            bool validRowFound = false;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                int rowCount = worksheet.Dimension.Rows;
                int columnCount = worksheet.Dimension.Columns;
                for (int row = 2; row <= rowCount; row++)
                {
                    bool isRowValid = false;
                    for (int col = 1; col <= columnCount; col++)
                    {

                        var cellValue = worksheet.Cells[row, col].Text.Trim();
                        if (!string.IsNullOrEmpty(cellValue))
                        {
                            isRowValid = true;
                            break; 
                        }
                    }
                    if (isRowValid)
                    {
                        validRowFound = true;
                        break;
                    }
                }
            }
        }

        // Method to read data from the Excel file
        public static List<Dictionary<string, string>> ReadExcelData(string fileName)
        {
            using var package = new ExcelPackage(new FileInfo(fileName));
            var worksheet = package.Workbook.Worksheets["Sheet1"];
            if (worksheet?.Dimension == null)
                return new List<Dictionary<string, string>>();

            var rowCount = worksheet.Dimension.Rows;
            var colCount = worksheet.Dimension.Columns;

            var headers = Enumerable.Range(1, colCount)
                .Select(col => worksheet.Cells[1, col].Text.Trim())
                .ToList();

            return Enumerable.Range(2, rowCount - 1)
                .Select(row => headers
                    .Select((header, colIndex) => new { header, value = worksheet.Cells[row, colIndex + 1].Text.Trim() })
                    .ToDictionary(x => x.header, x => x.value))
                .ToList();
        }

        public static List<Dictionary<string, string>> ReadExcel(string filePath)
        {
            var data = new List<Dictionary<string, string>>();
            if (!File.Exists(filePath))
            {
                throw new Exception($"The file {filePath} does not exist.");
            }
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets["Sheet1"];
                var rowCount = worksheet.Dimension?.Rows ?? 0;
                var colCount = worksheet.Dimension?.Columns ?? 0;
                var headers = new List<string>();
                for (int col = 1; col <= colCount; col++)
                {
                    string headerText = worksheet.Cells[1, col].Text.Trim();
                    if (string.IsNullOrEmpty(headerText))
                    {
                        throw new Exception($"Header in column {col} is empty.");
                    }
                    headers.Add(headerText);
                }
                for (int row = 2; row <= rowCount; row++)
                {

                    var rowData = new Dictionary<string, string>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        string cellValue = worksheet.Cells[row, col].Text.Trim();
                        rowData[headers[col - 1]] = string.IsNullOrEmpty(cellValue) ? "" : cellValue;
                    }
                    data.Add(rowData);
                }
            }
            return data;
        }

        public static string GetTestCaseIdByTitle(string input)
        {
            // Match only numbers with 3 or more digits
            var match = Regex.Match(input, @"\d{3,}");
            return match.Success ? match.Value : string.Empty;
        }

        public static string GetModuleNameFromTags(ScenarioContext scenarioContext)
        {
            var moduleTag = scenarioContext.ScenarioInfo.Tags.FirstOrDefault(t => t.StartsWith("Module-", StringComparison.OrdinalIgnoreCase));
            if (moduleTag == null)
            {
                throw new InvalidOperationException("No @Module-XXX tag found in the scenario.");
            }
            return moduleTag.Split('-')[1]; // Extract 'BOM' from 'Module-BOM'
        }
    }
}
