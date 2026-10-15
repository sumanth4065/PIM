Feature:  Manage BOM Line Addition Based on Product Category Uniqueness

@UI @TCID-113770 @Module-Bom
Scenario: TC-113770 Product Category Update BOM Line Add No Same Category
	Given User login to the PIM application using the ppt username and password
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	Then Verify "Asset" tab "Bom-Kind" field is enabled
	And Verify "Asset" tab "New" button is enabled
	When User clicks on "Asset" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Asset" tab
	| FieldName      |
	| Asset-BOM-Kind |
	| Serial-No      |
	Then User gets the values of below fields from "Asset" tab
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	When User clicks on "Asset" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then Verify created and modified username and date in right corner of BOM header panel in "Asset" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
	Then User gets the values of below fields from BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Validate BOM line grid row "0" data matches BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	Then User get the value of BOM Line grid row "0" data from Asset tab
	| Fieldvalue |
	| Sku        |
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the "Item" Id from BOM
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	When User clicks on "Edit" button in Item Master
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes


	
	