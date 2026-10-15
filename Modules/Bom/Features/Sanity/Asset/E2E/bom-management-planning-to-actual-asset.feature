Feature: BOM and Event Processing for Planning To Actual Assets
	
@UI @TCID-113690 @Module-Bom 
Scenario: 01 TC-113690 Best BOM Created Event Planning Asset Create
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
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
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
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Planning BOM document is saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-43723 @Module-Bom 
Scenario: 02 TC-43723 Header Notes Add Undo Asset
	Then Verify "Asset" tab "Edit" button is enabled
	When User clicks on "Asset" tab "Edit" button in BOM header panel 
	Then User enters the data in the BOM internal notes textbox under the "Asset" tab in the BOM header
	Then Verify "Asset" tab "Cancel" button is enabled
	When User clicks on "Asset" tab "Cancel" button in BOM header panel 
	Then User clicks on Yes button on the pop up
	Then Verify Bom internal notes in "Asset" tab and Cosmos DB using "Pim" database and "BOMs" container

@NoNewBrowser @TCID-113692 @Module-Bom
Scenario: 03 TC-113692 BOM Line Quantity Update Planning Asset
	When User clicks on row "0" Edit button in BOM Line creation panel
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
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
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Planning BOM document is saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-113691 @Module-Bom
Scenario: 04 TC-113691 BOM Line Added Event Planning Asset
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
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Planning BOM document is saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-113693 @Module-Bom 
Scenario: 05 TC-113693 BOM Line Other Updates Planning Asset
	When User clicks on row "0" Edit button in BOM Line creation panel
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "0"
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
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Planning BOM document is saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-43726 @Module-Bom 
Scenario: 06 TC-43726 Mark BOM Line Obsolete Existing Asset
	When User clicks on row "0" Edit button in BOM Line creation panel
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "0"
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

@NoNewBrowser @TCID-113757 @Module-Bom 
Scenario: 07 TC-113757 Planning To Actual Created Event Asset
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
	Then Verify created and modified username and date of BOM Line "0"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
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
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
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
	| EventHistory     |
	| ActualBOMCreated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-113758 @Module-Bom 
Scenario: 08 TC-113758 Planning To Actual BOM Line Added Event Asset
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
	| EventHistory       |
	| ActualBOMLineAdded |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab	

@NoNewBrowser @TCID-113759 @Module-Bom 
Scenario: 09 TC-113759 Planning To Actual BOM Line Quantity Updated Event Asset
	When User clicks on row "0" Edit button in BOM Line creation panel
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
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory                 |
	| ActualBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container

@NoNewBrowser @TCID-113760 @Module-Bom 
Scenario: 10 TC-113760 Planning To Actual BOM Line Obsolete Updated Event Asset
	When User clicks on row "0" Edit button in BOM Line creation panel
	Then User checks BOM Line "Obsolete" checkbox
	Then User verifies "Quantity" defaults to "0" in the BOM Line creation section
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User verifies "Quantity" defaults to "0" in the BOM Line Grid row "0"
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
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory                 |
	| ActualBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Asset" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container