using System.Text;

namespace Utilities
{
    public static class Logger
    {
        private static readonly string logPath = "TestLog.txt";
        private static readonly object _lock = new object(); // For thread safety within the same process

        public static void LogInfo(string message)
        {
            Log("[INFO]", message);
        }

        public static void LogError(string message)
        {
            Log("[ERROR]", message);
        }

        private static void Log(string level, string message)
        {
            string log = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level} {message}";
            Console.WriteLine(log);

            try
            {
                lock (_lock) // Ensure thread-safe logging within the process
                {
                    using (var stream = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    using (var writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        writer.WriteLine(log);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"[LOGGER ERROR] Failed to write to log file: {ex.Message}");
                // Optionally log to a fallback file or ignore
            }
        }
    }
}
