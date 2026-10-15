Feature: Manage BOM Header for Assets

@UI @TCID-43720 @Module-Bom
Scenario: 01 TC-43720 Create New BOM Header For Asset With Existing BOM
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
	Then User captures the entered values in the "Asset" tab
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	When User clicks on "Asset" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies the created data matches "Asset" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	When User clicks on "Asset" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Asset" tab
	| FieldName      |
	| Asset-BOM-Kind |
	| Serial-No      |
	Then User verifies duplicate "Asset" BOM creation error message
	When User clicks on "Asset" tab "Cancel" button in BOM header panel 
	Then User clicks on Yes button on the pop up
	Then User recalls below field values in "Asset" tab
	| FieldName |
	| BOM-Kind  |
	| Serial-No |
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |

@NoNewBrowser @TCID-43722 @Module-Bom
Scenario: 02 TC-43722 Edit BOM Header And Save For Asset
	When User clicks on "Asset" tab "Edit" button in BOM header panel
	Then User clicks and enters BOM Internal Notes field in "Asset" tab
	When User clicks on "Asset" tab "Save" button in BOM header panel
	Then Verify created and modified username and date in right corner of BOM header panel in "Asset" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
