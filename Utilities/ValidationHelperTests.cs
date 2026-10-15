using NUnit.Framework;

namespace PIMQAAutomation.Utilities
{
    [TestFixture]
    public class AssertEqualTests
    {
        [Test]
        public void AssertEqual_WithDetailsOnly_ShowsDetailsInMessage()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.AssertEqual("Level", 5, 3, "Level must be equal for ranking."));

            StringAssert.Contains("Level must be equal for ranking.", ex.Message);
        }

        [Test]
        public void AssertEqual_WithExpectedNullAndActualNotNull_Fails()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.AssertEqual("Note", null, "Something", "Null check mismatch."));

            StringAssert.Contains("Expected value of type 'System.String' for 'Note' to be '', but found 'Something'. Null check mismatch.", ex.Message);
        }

        [Test]
        public void AssertEqual_WithMatchingIntegers_Passes()
        {
            Assert.DoesNotThrow(() =>
                ValidationHelper.AssertEqual("UserId", 123, 123, string.Empty));
        }

        [Test]
        public void AssertEqual_WithMatchingStrings_CaseInsensitive_Passes()
        {
            Assert.DoesNotThrow(() =>
                ValidationHelper.AssertEqual("Status", "Active", "active", string.Empty));
        }

        [Test]
        public void AssertEqual_WithMismatchedIntegers_ThrowsAssertionException()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.AssertEqual("UserId", 123, 456, "IDs should match."));

            StringAssert.Contains("Expected value of type 'System.Int32' for 'UserId' to be '123', but found '456'. IDs should match.", ex.Message);
        }

        [Test]
        public void AssertEqual_WithMismatchedStrings_ThrowsAssertionException()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.AssertEqual("Status", "Active", "Inactive", "Status must match expected state."));

            StringAssert.Contains("Expected value of type 'System.String' for 'Status' to be 'Active', but found 'Inactive'. Status must match expected state.", ex.Message);
        }

        [Test]
        public void AssertEqual_WithNullStrings_ThatMatch_Passes()
        {
            string expected = null;
            string actual = null;

            Assert.DoesNotThrow(() =>
                ValidationHelper.AssertEqual("Comment", expected, actual, string.Empty));
        }
    }

    [TestFixture]
    public class FailIfNullOrEmptyTests
    {
        [Test]
        public void FailIfEmptyCollection_ShouldThrow_WhenCollectionIsEmpty()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.FailIfNullOrEmpty("Items", new List<int>(), "Empty list test"));

            StringAssert.Contains("Collection is null or empty", ex.Message);
            StringAssert.Contains("Items", ex.Message);
        }

        [Test]
        public void FailIfEmptyString_ShouldThrow_WhenStringIsEmpty()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.FailIfNullOrEmpty("Name", "", "Empty string test"));

            StringAssert.Contains("String is null or empty", ex.Message);
            StringAssert.Contains("Name", ex.Message);
        }

        [Test]
        public void FailIfNull_ShouldThrow_WhenValueIsNull()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.FailIfNullOrEmpty<object>("TestField", null, "Null value test"));

            StringAssert.Contains("Value is null or empty", ex.Message);
            StringAssert.Contains("TestField", ex.Message);
        }

        [Test]
        public void FailIfWhitespaceString_ShouldThrow_WhenStringIsWhitespace()
        {
            var ex = Assert.Throws<AssertionException>(() =>
                ValidationHelper.FailIfNullOrEmpty("Comment", "   ", "Whitespace test"));

            StringAssert.Contains("String is null or empty", ex.Message);
            StringAssert.Contains("Comment", ex.Message);
        }

        [Test]
        public void PassIfNonEmptyCollection_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
                ValidationHelper.FailIfNullOrEmpty("Numbers", new[] { 1, 2 }, "Should pass"));
        }

        [Test]
        public void PassIfNonEnumerableNonString_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
                ValidationHelper.FailIfNullOrEmpty("Age", 42, "Should pass"));
        }

        [Test]
        public void PassIfValidString_ShouldNotThrow()
        {
            Assert.DoesNotThrow(() =>
                ValidationHelper.FailIfNullOrEmpty("Greeting", "hello", "Should pass"));
        }
    }
}