Feature: Manage Bill of Materials Headers for Parts

@UI @TCID-43731 @Module-Bom
Scenario: 01 TC-43731 Create New BOM Header For Part With Existing BOM
	Given User login to the PIM application using the ppt username and password
    When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed 
	Then Verify "Asset" tab "Bom-Kind" field is enabled
	Then Verify "Asset" tab "New" button is enabled
	When User clicks on "Asset" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Asset" tab
	| FieldName      |
	| Asset-BOM-Kind |
	| Serial-No      |
	When User clicks on "Asset" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User gets the values of below fields from "Asset" tab
	| FieldValue |
	| Oem        |
	| Sku        |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	When User selects "Part" Tab  
	Then Verify "Part" tab "Bom-Kind" field is enabled
	Then Verify "Part" tab "New" button is enabled
	When User clicks on "Part" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Part" tab
	| FieldName     |
	| Part-BOM-Kind |
	Then User searches and selects data for the below fields from dropdown in "Part" tab using Asset data
	| FieldName |
	| Oem       |
	| Sku       |
	Then User captures the entered values in the "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	When User clicks on "Part" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Part" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	When User clicks on "Part" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Part" tab
	| FieldName     |
	| Part-BOM-Kind |
	Then User searches and selects data for the below fields from dropdown in "Part" tab using Asset data
	| FieldName |
	| Oem       |
	| Sku       |
	Then User verifies duplicate "Part" BOM creation error message
	When User clicks on "Part" tab "Cancel" button in BOM header panel
	Then User clicks on Yes button on the pop up
	Then User recalls below field values in "Part" tab
	| FieldName |
	| BOM-Kind  |
	| Oem       |
	| Sku       |
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container 
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container

@NoNewBrowser @TCID-43733 @Module-Bom
Scenario: TC-43733 Header Edit Part Save
	When User clicks on "Part" tab "Edit" button in BOM header panel
	Then User clicks and enters BOM Internal Notes field in "Part" tab
	When User clicks on "Part" tab "Save" button in BOM header panel
	Then Verify created and modified username and date in right corner of BOM header panel in "Part" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container 
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container

