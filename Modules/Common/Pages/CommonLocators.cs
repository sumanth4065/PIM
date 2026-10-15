using Google.Protobuf.WellKnownTypes;
using Microsoft.Playwright;

namespace PIMQAAutomation.Common.Pages
{
    public class CommonLocators
    {
        private const string CreatedModifiedUserDate = "//span[@id='common-date-[TEXT]' or @id='common-date-doc-[TEXT]']";
        private const string PopupSaveMessage = "//span[text()='Do you want to save your changes?']";
        private const string PopupYesButton = "#modal-yes-button";
        private const string SuccessTitlePopup = "#modal-title";
        private const string PopupOkButton = "#modal-ok-button";
        private const string ToastMessage = "//span[contains(@id,'toast-message')]";
        private const string SpinnerSelector = "//div[text()='Loading...']";
        private const string DropdownListBox = "//div[@role='listbox']";
        private const string SelectDropdownOption = "//mat-option/span[normalize-space(text())='[TEXT]']";
        private const string CreatedPopupYesButton = "#modal-yes-button";
        private const string DocumentModifiedUserDate = "//span[@id='common-date-document-[TEXT]']";
        private const string TermsAndConditionsPopUp = "//div[text()='Accept Terms of Service']";
        private const string CheckboxTermsAndCondition = "//span[contains(text(), 'you are accepting the terms')]";
        private const string AcceptButton = "//span[normalize-space(text())='Accept']";
        private const string EmptyDropdown = "//span[text()='NO DATA FOUND']";
        private const string ErrorTitlePopupMessage = "//h4[normalize-space(text())='Error: Key Fields that duplicate an Existing Record']";
        private const string PopupMessage = "//span[contains(@id,'toast-message-0')] | //div[@id='popup-content' or @id='modal-body']/span";
        private const string DropdownOption = "//mat-option[@role='option' and @id='[TEXT]']";
        private readonly IPage page;
        public CommonLocators(IPage page) => this.page = page;
        public ILocator GetCreatedModifiedUserDate(string createdModifiedUserDate) => page.Locator(CreatedModifiedUserDate.Replace("[TEXT]", createdModifiedUserDate.ToLower()));
        public ILocator GetSavePopupMessage() => page.Locator(PopupSaveMessage);
        public ILocator GetPopupYesButton() => page.Locator(PopupYesButton);
        public ILocator GetSuccessTitlePopup() => page.Locator(SuccessTitlePopup).First;
        public ILocator GetPopupOkButton() => page.Locator(PopupOkButton).First;
        public ILocator GetLoader() => page.Locator(SpinnerSelector);
        public ILocator GetDropdownListBox() => page.Locator(DropdownListBox);
        public ILocator GetSelectDropdownOption(string option) => page.Locator(SelectDropdownOption.Replace("[TEXT]", option));
        public ILocator GetCreatedPopupYesButton() => page.Locator(CreatedPopupYesButton);
        public ILocator GetDocumentModifiedUserDate(string modifiedUserDate) => page.Locator(DocumentModifiedUserDate.Replace("[TEXT]", modifiedUserDate.ToLower()));
        public ILocator GetTermsAndConditionsPopUp() => page.Locator(TermsAndConditionsPopUp);
        public ILocator GetCheckboxTermsAndCondition() => page.Locator(CheckboxTermsAndCondition);
        public ILocator GetAcceptButton() => page.Locator(AcceptButton);
        public ILocator GetEmptyDropdown() => page.Locator(EmptyDropdown);
        public ILocator GetErrorTitlePopupMessage() => page.Locator(ErrorTitlePopupMessage);
        public ILocator GetpopupMessage() => page.Locator(PopupMessage);
        public ILocator GetToastMessage() => page.Locator(ToastMessage);
        public ILocator GetDropDownOption(string option) => page.Locator(DropdownOption.Replace("[TEXT]", option));
    } 
}