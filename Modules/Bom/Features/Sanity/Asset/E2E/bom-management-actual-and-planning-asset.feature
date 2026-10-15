Feature: BOM and Event Processing for Actual and Planning Assets
	
@UI @TCID-43721 @Module-Bom
Scenario: 01 TC-43721 Header Creation New Asset
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
	Then Verify "Asset" tab "Create" button is enabled
	Then Verify "Asset" tab "Cancel" button is enabled
	When User clicks on "Asset" tab "Create" button in BOM header panel 
	Then User verifies success message for BOM creation
	Then User verifies the created data matches "Asset" BOM header page
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	Then Verify created and modified username and date in right corner of BOM header panel in "Asset" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	
@NoNewBrowser @TCID-113678 @Module-Bom
Scenario: 02 TC-113678 Best BOM Created Event Actual and Planning Asset
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
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       | 
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory       |
	| PlanningBOMCreated |
	| ActualBOMCreated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab

@NoNewBrowser @TCID-113679 @Module-Bom
Scenario: 03 TC-113679 BOM Line Added Event Actual and Planning Asset
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
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory         |
	| PlanningBOMLineAdded |
	| ActualBOMLineAdded   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab

@NoNewBrowser @TCID-113680 @Module-Bom
Scenario: 04 TC-113680 BOM Line Quantity Update Actual and Planning Asset
	When User clicks on row "1" Edit button in BOM Line creation panel
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Verify created and modified username and date of BOM Line "1"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-113681 @Module-Bom
Scenario: 05 TC-113681 BOM Line Other Updates Actual and Planning Asset
	When User clicks on row "1" Edit button in BOM Line creation panel
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "1"
	Then Verify created and modified username and date of BOM Line "1"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory					 |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

