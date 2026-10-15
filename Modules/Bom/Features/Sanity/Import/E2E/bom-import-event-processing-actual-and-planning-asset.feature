Feature: Import and Event Processing of BOM for Actual and Planning Assets

@UI @TCID-112540 @Module-Bom
Scenario: 01 TC-112540 Import Actual And Planning Asset Best BOM Creation Event
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Asset-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Create" Excel file for the "Asset" tab
	Then User verifies valid data is present in the "Create" Excel file 
	And User uploads the "Create" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User retrives the PartitionKey for "Create" Excel SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	Then Verify the BOM Line grid row "0" data of "Create" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed 
	Then User recalls "Asset" header "BOM-Kind" and "Serial-No"
	Then Verify Header fields are not empty in "Asset" tab
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	| Sku       |
	Then Verify BOM UI data "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
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
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify ID and Partition Key generating from PIM match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-112541 @Module-Bom
Scenario: 02 TC-112541 Import Actual And Planning Asset BOM Line Added Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	Then User selects import type as "Asset-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "LineAdded" Excel file for the "Asset" tab
	Then User verifies valid data is present in the "LineAdded" Excel file  
	And User uploads the "LineAdded" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "1" data of "LineAdded" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	Then User recalls "Asset" header "BOM-Kind" and "Serial-No"
	Then Verify Header fields are not empty in "Asset" tab
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	| Sku       |
	Then Verify BOM UI data "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory	      |
	| ActualBOMLineAdded  |
	| PlanningBOMLineAdded|
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify ID and Partition Key generating from PIM match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-112542 @Module-Bom
Scenario: 03 TC-112542 Import Actual And Planning Asset BOM Line Quantity Updated Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Asset-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Update" Excel file for the "Asset" tab
	Then User verifies valid data is present in the "Update" Excel file 
	And User uploads the "Update" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "1" data of "Update" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	Then User recalls "Asset" header "BOM-Kind" and "Serial-No"
	Then Verify Header fields are not empty in "Asset" tab
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	| Sku       |
	Then Verify BOM UI data "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory                   |
	| ActualBOMLineQuantityUpdated   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container


@NoNewBrowser @TCID-112543 @Module-Bom
Scenario: 04 TC-112543 Import Actual And Planning Asset BOM Line Obsolete Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Asset-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Obsolete" Excel file for the "Asset" tab
	Then User verifies valid data is present in the "Obsolete" Excel file 
	And User uploads the "Obsolete" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "1" data of "Obsolete" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	Then User recalls "Asset" header "BOM-Kind" and "Serial-No"
	Then Verify Header fields are not empty in "Asset" tab
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	| Sku       |
	Then Verify BOM UI data "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Asset" tab
	| EventHistory                   |
	| ActualBOMLineQuantityUpdated   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Asset" tab
	Then Verify Actual and Planning BOM documents are saved in "Integration-Data-Hub" database using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Planning Bom" data in "Integration-Data-Hub" using "BOMs" container
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Asset" tab
	Then Verify "Actual Bom" data in "Integration-Data-Hub" using "BOMs" container
	