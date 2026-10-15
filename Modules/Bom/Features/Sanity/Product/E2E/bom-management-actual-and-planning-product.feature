Feature: BOM and Event Processing for Actual and Planning Products

@UI @TCID-43707 @Module-Bom
Scenario: 01 TC-43707 Header Creation New Product
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
	Then User captures the entered values in the "Asset" tab
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	When User clicks on "Asset" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Asset" BOM header page
	| FieldValue  |
	| BOM-Kind    |
	| Serial-No   |
	Then User gets the values of below fields from "Asset" tab
	| Fieldvalues |
	| Oem         |
	| Family      |
	| Line        |
	| Group       |
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
	Then Verify "Product" tab "Create" button is enabled
	Then Verify "Product" tab "Cancel" button is enabled
	When User clicks on "Product" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Product" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	Then Verify created and modified username and date in right corner of BOM header panel in "Product" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |

@NoNewBrowser @TCID-43708 @Module-Bom
Scenario: 02 TC-43708 Header Notes Edit Save Product
	When User clicks on "Product" tab "Edit" button in BOM header panel
	Then User clicks and enters BOM Internal Notes field in "Product" tab
	When User clicks on "Product" tab "Save" button in BOM header panel
	Then Verify created and modified username and date in right corner of BOM header panel in "Product" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |

@NoNewBrowser @TCID-113686 @Module-Bom
Scenario: 03 TC-113686 Best BOM Created Event Actual Planning Product
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
	Then User clicks and enters BOM Line "Item-To-Bom-Notes" textbox
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
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Product" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory       |
	| PlanningBOMCreated |
	| ActualBOMCreated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-43712 @Module-Bom
Scenario: 04 TC-43712 BOM Line Add To Grid Product
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
	Then User clicks and enters BOM Line "Item-To-Bom-Notes" textbox
	Then User gets the values of below fields from BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Validate BOM line grid row "1" data matches BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	Then Verify created and modified username and date of BOM Line "1"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Product" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |

@NoNewBrowser @TCID-113687 @Module-Bom
Scenario: 05 TC-113687 BOM Line Added Event Actual Planning Product
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User clicks and enters BOM Line "Item-To-Bom-Notes" textbox
	Then User gets the values of below fields from BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Validate BOM line grid row "2" data matches BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	Then Verify created and modified username and date of BOM Line "2"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "2" and Cosmos DB using "Pim" database and "BOMs" container for "Product" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory         |
	| PlanningBOMLineAdded |
	| ActualBOMLineAdded   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "2" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "2" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113688 @Module-Bom
Scenario: 06 TC-113688 BOM Line Quantity Update Actual Planning Product
	When User clicks on row "2" Edit button in BOM Line creation panel	
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Verify created and modified username and date of BOM Line "2"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "2" and Cosmos DB using "Pim" database and "BOMs" container for "Product" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "2" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "2" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container
	
@NoNewBrowser @TCID-113689 @Module-Bom
Scenario: 07 TC-113689 BOM Line Other Updates Actual Planning Product
	When User clicks on row "2" Edit button in BOM Line creation panel
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "2"
	Then Verify created and modified username and date of BOM Line "2"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Product" tab with Cosmos DB using "Pim" database and "BOMs" container using "Product-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "2" and Cosmos DB using "Pim" database and "BOMs" container for "Product" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "2" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "2" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container
