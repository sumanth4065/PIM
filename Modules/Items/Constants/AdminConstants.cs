namespace PIMQAAutomation.Modules.Items.Constants
{
    public static class AdminConstants
    {
        public const string IsNecessary = "Is Necessary";
        public const string IsOptional = "Is Optional";
        public const string IsStandard = "Is Standard";
        public const string SaveChangesTitle = "Save changes";
        public const string SaveChangesMessage = "Do you want to save your changes?";
        public const string NoTranslation = "No Translation";
        public const string AuthorizedUsers = "Authorized Users";
        public const string NewGroup = "New Group";
        public const string ExistingGroup = "Existing Group";
        public const string ActiveState = "Active";
        public const string InactiveState = "Inactive";
        public const char MaxLengthTestChar = 'a'; // Character used to exceed the maximum length
        public const string LossOfDataTitle = "Warning: Potential Loss of Data";
        public const string LossOfDataMessage = "You are about to navigate away from this page, and will lose any unsaved. Do you wish to continue?";
        public const string PopUpYesButton = "Yes";

        // Dictionary<FieldKey, (MaxLength, ErrorLocator, EmptyError, MaxLengthError)>
        public static readonly Dictionary<string, (int MaxLength, string ErrorLocator, string EmptyError, string MaxLengthError)>
        FieldValidations = new()
        {
            { "OemTextbox", (30, "AdminOem.OemNameError", "OEM name is required!", "The input exceeds the allowed character limit!") },
            { "GroupNameDropdown", (0, "AdminOem.GroupNameError", "Group name is required!", null) }, // only empty validation
            { "DescriptionTextbox", (50, "AdminOem.DescriptionError", "Description is required!", "The input exceeds the allowed character limit!") }
        };
        public static class ErrorToastMessages
        {
            public const string OemInactive = "Data entered doesn’t exist or is inactive in the database, Try with some other proper data";
        }
        public static class FeatureContextValues
        {
            public const string OemName = "OemName";
            public const string GroupName = "GroupName";
        }

        public static class ProductClassConstants
        {
            public const int DescriptionCharLimit = 55;
            public const int ClassCharLimit = 35;

            public class ErrorMessages
            {
                public const string ProductClassRequired = "Product Class name is required!";
                public const string DescriptionRequired = "Description is required!";
                public const string InternalNotesRequired = "Internal Notes is required!";
                public const string CharacterLimitExceeded = "The input exceeds the allowed character limit!";
            }
        }

        public static class ProductCategoryConstants
        {
            public const int DescriptionCharLimit = 55;
            public const int CategoryCharLimit = 35;

            public class ErrorMessages
            {
                public const string ProductCategoryRequired = "Product Category name is required!";
                public const string DescriptionRequired = "Description is required!";
                public const string InternalNotesRequired = "Internal Notes is required!";
                public const string CharacterLimitExceeded = "The input exceeds the allowed character limit!";
            }
        }
    public static class CharacterLimit
    {
        public const int KindName = 35;
        public const int KindRank = 6;
        public const int KindRankMemo = 21;
        public const int KindDescription = 55;
        public const int TypeName = 31;
        public const int TypeRankMemo = 4;
        public const int TypeDescription = 51;
        public const int LovName = 110;
        public const int LovDescription = 51;
        }

        public static class ErrorMessages
        {
            public const string CharacterLimitExceeded = "The input exceeds the allowed character limit!";
            public const string KindRankMustContainNumbers = "kind Rank must contain only numbers!";
        }
    }

    public static class SwitchcaseValues
    {
        public const string Oem = "Oem";
        public const string Family = "Family";
    }

    public static class TaxonomyCharKindOptions
    {
        public const string ProductTaxonomy = "Product Taxonomy";
        public const string ServiceTaxonomy = "Service Taxonomy";

        public static class ProductTaxonomyOptions
        {

            public const string Oem = "3COM";
            //public const string 

        }

        public static class TaxonomySearchBoxTitle
        {
            public const string Oem = "OEM";
            public const string Family = "Family";
            public const string Line = "Line";
            public const string Group = "Group";
            public const string Class = "Class";
        }
    }
}
