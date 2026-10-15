Feature: BOM and Event Processing for Actual and Planning Parts

@UI @TCID-43732 @Module-Bom
Scenario: 01 TC-43732 Header Creation New Part
	Given User login to the PIM application using the ppt username and password
    When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	#Create New Asset
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
	| FieldValue |
	| Oem        |
	| Sku        |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	#Create New Part
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
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container 
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container

@NoNewBrowser @TCID-43734 @Module-Bom
Scenario: 02 TC-43734 Header Notes Add Undo Part
	When User clicks on "Part" tab "Edit" button in BOM header panel 
	Then User enters the data in the BOM internal notes textbox under the "Part" tab in the BOM header
	Then Verify "Part" tab "Cancel" button is enabled
	When User clicks on "Part" tab "Cancel" button in BOM header panel 
	Then User clicks on Yes button on the pop up
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container

@NoNewBrowser @TCID-113682 @Module-Bom
Scenario: 03 TC-113682 Best BOM Created Event Actual Planning Part
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
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
	Then Validate BOM line grid row "0" data matches BOM line creation panel
	| FieldValue |
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
	| EventHistory       |
	| PlanningBOMCreated |
	| ActualBOMCreated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113683 @Module-Bom
Scenario: 04 TC-113683 BOM Line Added Event Actual Planning Part
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User clicks and enters BOM Line "Quantity" field
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
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory         |
	| ActualBOMLineAdded   |
	| PlanningBOMLineAdded |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "1" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "1" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113684 @Module-Bom
Scenario: 05 TC-113684 BOM Line Quantity Update Actual Planning Part
	When User clicks on row "1" Edit button in BOM Line creation panel
	Then User clicks and enters BOM Line "Quantity" field
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
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory					 |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-113685 @Module-Bom
Scenario: 06 TC-113685 BOM Line Other Updates Actual Planning Part
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
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify "UI" BOM line grid row "1" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory					 |
	| PlanningBOMLineQuantityUpdated |
	| ActualBOMLineQuantityUpdated   |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "UI"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container
