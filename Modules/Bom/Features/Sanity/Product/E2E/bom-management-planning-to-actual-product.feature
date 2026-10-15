Feature: BOM and Event Processing for Planning To Actual Products

@UI @TCID-113698 @Module-Bom
Scenario: 01 TC-113698 Best BOM Created Event Planning Product
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
	| Fieldvalue |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	When User selects "Product" Tab  
	Then Verify "Product" tab "Bom-Kind" field is enabled
	And Verify "Product" tab "New" button is enabled
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
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113699 @Module-Bom
Scenario: 02 TC-113699 BOM Line Added Event Planning Product
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User clicks and enters BOM Line "Item-To-Bom-Notes" textbox
	Then User gets the values of below fields from BOM line creation panel
	| FieldValue |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Validate BOM line grid row "1" data matches BOM line creation panel
	| FieldValue |
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory         |
	| PlanningBOMLineAdded |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113700 @Module-Bom
Scenario: 03 TC-113700 BOM Line Quantity Update Planning Product
	When User clicks on row "1" Edit button in BOM Line creation panel
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113701 @Module-Bom
Scenario: 04 TC-113701 BOM Line Other Updates Planning Product
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory					 |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-43714 @Module-Bom
Scenario: 05 TC-43714 Mark BOM Line Obsolete Existing Product
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

@NoNewBrowser @TCID-113765 @Module-Bom
Scenario: 06 TC-113765 Planning To Actual Created Event Product
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
	| ActualBOMCreated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113766 @Module-Bom
Scenario: 07 TC-113766 Planning To Actual BOM Line Added Event Product
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory       |
	| ActualBOMLineAdded |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113767 @Module-Bom
Scenario: 08 TC-113767 Planning To Actual BOM Line Quantity Updated Event Product
	When User clicks on row "1" Edit button in BOM Line creation panel
	Then User clicks and enters BOM Line "Quantity" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory                 |
	| ActualBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113768 @Module-Bom
Scenario: 09 TC-113768 Planning To Actual BOM Line Obsolete Updated Event Product
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
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Product" tab
	| EventHistory                 |
	| ActualBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Product" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Product" tab through "GUI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Product" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

