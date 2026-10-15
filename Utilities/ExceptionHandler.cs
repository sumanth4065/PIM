using Utilities;

namespace PIMQAAutomation.Utilities
{
    public class ExceptionHandler
    {
        public static CustomException BuildCustomException(string actionDescription, Exception ex, string testCaseName = "")
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Unknown";

            string errorMessage = $"[ERROR] Failed to {actionDescription}.{Environment.NewLine}" + $"Test Case: {testCaseName}{Environment.NewLine}" + $"Environment: {environment}{Environment.NewLine}" + $"Reason: {ex.Message}{Environment.NewLine}" + $"StackTrace: {ex.StackTrace}";

            Console.WriteLine($"[DEBUG] {errorMessage}");

            return new CustomException(errorMessage, ex);
        }
    }
}
