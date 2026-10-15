Feature: Manage Bill of Materials Headers for Products
 
@UI @Module-Bom @TCID-43706
Scenario: 01 TC-43706 Create New BOM Header For Product With Existing BOM
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
	| Family     |
	| Line       |
	| Group      |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	When User selects "Product" Tab  
	Then Verify "Product" tab "Bom-Kind" field is enabled
	Then Verify "Product" tab "New" button is enabled
	When User clicks on "Product" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Product" tab
	| FieldName        |
	| Product-BOM-Kind |
	Then User searches and selects data for the below fields from dropdown in "Product" tab using Asset data
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User captures the entered values in the "Product" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	When User clicks on "Product" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Product" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	When User clicks on "Product" tab "New" button in BOM header panel
	Then User searches and selects data for the below fields from dropdown in "Product" tab
	| FieldName        |
	| Product-BOM-Kind |
	Then User searches and selects data for the below fields from dropdown in "Product" tab using Asset data
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User verifies duplicate "Product" BOM creation error message
	When User clicks on "Product" tab "Cancel" button in BOM header panel
	Then User clicks on Yes button on the pop up
	Then User recalls below field values in "Product" tab
	| FieldName |
	| BOM-Kind  |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |

@NoNewBrowser @TCID-153279 @Module-Bom
Scenario: 02 TC-153279 Create New BOM Header For Product With Existing BOM Line
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies duplicate "BOMLine" BOM creation error message