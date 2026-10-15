using Microsoft.Playwright;

namespace PIMQAAutomation.Common.Pages
{
    public class ImportsPage
    {
        private readonly IPage page;

        private const string SelectImportType = "#imports-file-type-autocomplete , imports-file-type-input";
        private const string Button = "#imports-[BUTTON]-button";
        private const string FileNameOrSize = "#imports-table-body-file[NAME_SIZE]-0";
        private const string ImportScreenHeader = "//h4[normalize-space()='Import Files'] | //h1[@id='imports-recent-imports-title']";
        private const string InputFile = "//input[@id='fileInput']";
        private const string CrossButton = "#imports-file-type-autocomplete_clearBtn , imports-file-type-input-clear-button-icon";
        private const string NewImportButton = "#imports-[TEXT]-button";
        private const string FileNameAndType = "#imports-table-body-fileName-[TEXT] , imports-table-body-entityName-[TEXT]";
        private const string StatusViewLink = "(//td[contains(text(),'[TEXT]')]/following-sibling::td[contains(@id,'imports-table-body-status')])[1]//span";
        private const string TitlePopup = "//h2[@id='imports-status-popup-title']";
        private const string ErrorMessage = "//span[contains(text(),'Error')]";
        private const string ImportCompletionStatus = "//span[contains(text(),'Completion Status')]//following-sibling::span";
        private const string ImportCreatedDate = "//span[contains(text(),'Created On')]//following-sibling::span";
        private const string DetailedErrorMessageOfImportFail = "//span[contains(text(),'Detailed errors')]/parent::div/div//span";
        private const string ImportPopupCloseIcon = "//button[@id='imports-status-popup-close-button']";
        private const string ImportCancelButton = "#imports-status-cancel-button";
        private const string ImportRefreshButton = "#imports-status-refresh-button";
        private const string ImportSuccessBar = "#imports-success-bar";
        private const string ImportErrorList = "#imports-error-list";
        private const string ImportFileViewLink = "(//table[@id='imports-table']//tr[td[contains(., '[FILE-NAME]')]]//span[contains(@class,'view-text-link') and normalize-space(text())='View'])[1]";

        public ImportsPage(IPage page) => this.page = page;

        public ILocator GetImportScreenHeader() => page.Locator(ImportScreenHeader);
        public ILocator GetSelectImportTypeDropdown() => page.Locator(SelectImportType);
        public ILocator GetButtonInImportPage(string buttonType) => page.Locator(Button.Replace("[BUTTON]", buttonType));
        public ILocator GetFileNameOrSize(string nameOrSize) => page.Locator(FileNameOrSize.Replace("[NAME_SIZE]", nameOrSize));
        public ILocator GetInputFile() => page.Locator(InputFile);
        public ILocator GetCrossButton() => page.Locator(CrossButton);
        public ILocator GetNewImportButton(string buttonName) => page.Locator(NewImportButton.Replace("[TEXT]",buttonName));
        public ILocator GetFileNameAndType(string fileNameAndType) => page.Locator(FileNameAndType.Replace("[TEXT]", fileNameAndType));
        public ILocator GetStatusViewLink(string viewLink) => page.Locator(StatusViewLink.Replace("[TEXT]", viewLink));
        public ILocator GetTitlePopup() => page.Locator(TitlePopup);
        public ILocator GetErrorMessage() => page.Locator(ErrorMessage);
        public ILocator GetImportCompletionStatus() => page.Locator(ImportCompletionStatus);
        public ILocator GetImportCreatedDate() => page.Locator(ImportCreatedDate);
        public ILocator GetDetailedErrorMessageOfImportFail() => page.Locator(DetailedErrorMessageOfImportFail);
        public ILocator GetImportPopupCloseIcon() => page.Locator(ImportPopupCloseIcon);
        public ILocator GetImportCancelButton() => page.Locator(ImportCancelButton);
        public ILocator GetImportRefreshButton() => page.Locator(ImportRefreshButton);
        public ILocator GetImportSuccessBar() => page.Locator(ImportSuccessBar);
        public ILocator GetImportErrorList() => page.Locator(ImportErrorList);
        public ILocator GetImportFileViewLink(string fileName) => page.Locator(ImportFileViewLink.Replace("[FILE-NAME]", fileName)).First;
    }
}