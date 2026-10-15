Feature: Validate BOM Flattening with Multiple Obsolete Lines

@UI @Module-Bom @TCID-115825
Scenario: TC-115825 BOM Flattening Multiple Obsolete Lines
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
	| Sku        |
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
	Then Verify "Product" tab "Create" button is enabled
	Then Verify "Product" tab "Cancel" button is enabled
	When User clicks on "Product" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then Verify created and modified username and date in right corner of BOM header panel in "Product" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "0"
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku1" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
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
	Then Verify "Part" tab "Create" button is enabled
	Then Verify "Part" tab "Cancel" button is enabled
	When User clicks on "Part" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Part" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify created and modified username and date in right corner of BOM header panel in "Part" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
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
	Then Verify created and modified username and date of BOM Line "0"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory         |
	| PlanningBOMCreated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	