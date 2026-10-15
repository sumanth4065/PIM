namespace PIMQAAutomation.Modules.Bom.Constants
{
    public static class AdminConstants
    {
        public const int KindNameCharLimit = 32;
        public const int DescriptionCharLimit = 55;
        public const int SequencingKindRankCharLimit = 5;

        public static class ErrorMessages
        {
            public const string KindNameRequired = "Kind Name is required!";
            public const string DescriptionRequired = "Description is required!";
            public const string SequencingDigitExceeded = "Sequencing must not exceed 4 digits!";
            public const string kindRankDigitExceed = "Kind Rank must not exceed 4 digits!";
            public const string KindNameCharLimitExceeded = "The input exceeds the allowed character limit!";
            public const string DescriptionCharLimitExceeded = "The input exceeds the allowed character limit!";
            public const string SequencingOnlyNumbers = "Sequencing must contain only numbers!";
            public const string KindRankOnlyNumbers = "Kind Rank must contain only numbers!";
        }
    }
}
