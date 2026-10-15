Feature: BOM Part Creation Lifecycle and Recall Validation

@UI @Module-Bom @TCID-151172
Scenario: TC-151172 Validate UI Error Messages and Create Part BOM Kind in Part BOM Creation
	Given User login to the PIM application using the ppt username and password
	When User navigates to "BomConfiguration"
	Then Verify User is on BOM Configuration Screen
	When User clicks on the New Kind button
	When User selects "Part" from the popup menu
	When User clicks on the "Save" button
	Then Verify the below fields error messages are displayed
	| FieldName   |
	| KindName    |
	| Description |
	Then Verify character limit error messages are displayed for the below fields with exceeding the data
	| FieldName   |
	| KindName    |
	| Description |
	| Sequencing  |
	| KindRank    |
	When User enters numeric values exceeding the limit in the following fields
    | FieldName  |
    | Sequencing |
    | KindRank   |
	When User enters BOM Configuration details
	When User clicks on the "Save" button
	Then User Clicks on the "No" button
	When User clicks on the "Save" button
	Then User Clicks on the "Yes" button
	Then Verify Created and Modifyed details are displayed
	When User navigates to "BomManagement"
	Then Verify "Asset" tab is displayed
	When User selects "Part" Tab  
	Then Verify "Part" tab "Bom-Kind" field is enabled
	Then Verify "Part" tab "New" button is enabled
	When User clicks on "Part" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Part" tab
	| FieldName     |
	| Part-BOM-Kind |
	| Oem           |
	| Sku           |
	When User clicks on "Part" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then Verify created and modified username and date in right corner of BOM header panel in "Part" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |