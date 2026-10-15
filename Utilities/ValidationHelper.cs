using NUnit.Framework;
using PIMQAAutomation.Modules.Common.Constants;
using System.Collections;

namespace PIMQAAutomation.Utilities
{
    public static class ValidationHelper
    {
        private static readonly string TimestampFormat = "MM/dd/yyyy hh:mm:ss tt";

        public static void AssertDictionariesMatch(Dictionary<string, string> actualValues, Dictionary<string, string> expectedValues, string context = "")
        {
            ArgumentNullException.ThrowIfNull(actualValues);
            ArgumentNullException.ThrowIfNull(expectedValues);

            var assertionMessage = "Expected value for key '{1}' in '{0}' to be '{2}', but found '{3}'.";

            foreach (var key in expectedValues.Keys)
            {
                if (!actualValues.TryGetValue(key, out var actual))
                {
                    throw new KeyNotFoundException($"Key '{key}' expected in 'actualValues' during {context}, but was not found.");
                }

                var expected = expectedValues[key];

                Assert.AreEqual(expected, actual, string.Format(assertionMessage, context, key, expected, actual));
            }
        }

        /// <summary>
        /// Generic version to compare any expected and actual values.
        /// </summary>
        public static void AssertEqual<T>(string fieldName, T expectedValue, T actualValue, string details = "")
        {
            var typeName = typeof(T).FullName ?? "Unknown";
            var baseMessage = "Expected value of type '{3}' for '{0}' to be '{1}', but found '{2}'.";
            var assertionMessage = string.Format(baseMessage, fieldName, expectedValue, actualValue, typeName);

            if (!string.IsNullOrWhiteSpace(details))
            {
                assertionMessage += $" {details}";
            }

            if (typeof(T) == typeof(string))
            {
                var expectedString = expectedValue as string;
                var actualString = actualValue as string;

                // Try numeric comparison
                if (double.TryParse(expectedString, out double expectedNumber) &&
                    double.TryParse(actualString, out double actualNumber))
                {
                    Assert.IsTrue(
                        expectedNumber == actualNumber,
                        assertionMessage);
                }
                else
                {
                    Assert.IsTrue(
                        string.Equals(expectedString, actualString, StringComparison.OrdinalIgnoreCase),
                        assertionMessage);
                }
            }
            else
            {
                Assert.AreEqual(expectedValue, actualValue, assertionMessage);
            }
        }


        public static void FailIfNullOrEmpty<T>(string fieldName, T value, string details)
        {
            var baseMessage = "Assertion failed: {0} - {1} is null or empty. {2}";

            if (value == null)
            {
                Fail(fieldName, "Value", details);
            }

            if (value is string str && string.IsNullOrWhiteSpace(str))
            {
                Fail(fieldName, "String", details);
            }

            if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any())
            {
                Fail(fieldName, "Collection", details);
            }

            void Fail(string field, string typeDescription, string context)
            {
                var assertionMessage = string.Format(baseMessage, field, typeDescription, context);
                Console.WriteLine($"ERROR: {assertionMessage}");
                Assert.Fail(assertionMessage);
            }
        }

        public static void Fail(string message, Exception? ex = null)
        {
            var timestamp = DateTime.Now.ToString(TimestampFormat);
            var assertionMessage = $"Assertion failed: {message} | Timestamp: {timestamp}";

            if (ex != null)
            {
                assertionMessage += $" Error: {ex.Message}";
            }
            Console.WriteLine($"Error: {assertionMessage}");
            Assert.Fail(assertionMessage);
        }
        public static bool AreValuesEqual(string expected, string actual, bool ignoreCase = false)
        {
            if (expected == null || actual == null)
                return expected == actual;

            expected = expected.Trim();
            actual = actual.Trim();

            // Try numeric comparison
            if (double.TryParse(expected, out double expectedNumber) &&
                double.TryParse(actual, out double actualNumber))
            {
                return Math.Abs(expectedNumber - actualNumber) < 0.0001;
            }

            // Fallback to string comparison
            return string.Equals(expected, actual, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        public static void FailIfNotNullOrNotEmpty<T>(string fieldName, T value, string details)
        {
            if (value == null)
                return;

            if (value is string str && string.IsNullOrWhiteSpace(str))
                return;

            if (value is IEnumerable enumerable && !enumerable.Cast<object>().Any())
                return;

            var message = $"{fieldName} was expected to be null or empty, but it contains data. {details}";
            Console.WriteLine($"ERROR: {message}");
            Assert.Fail(message);
        }
    }
}