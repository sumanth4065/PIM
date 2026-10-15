Feature: Import and Event Processing of Bill of Materials for Planning and Actual Parts

@UI @TCID-112568 @Module-Bom
Scenario: 01 TC-112568 Import Planning Part Best BOM Created Event
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Asset-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "AssetCreate" Excel file for the "Asset" tab
	Then User verifies valid data is present in the "Create" Excel file 
	And User uploads the "AssetCreate" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User retrives the PartitionKey for "AssetCreate" Excel SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	Then Verify the BOM Line grid row "0" data of "AssetCreate" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Create" Excel file for the "Part" tab
	Then User verifies valid data is present in the "Create" Excel file 
	And User uploads the "Create" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "0" data of "Create" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	When User selects "Part" Tab  
	Then User recalls below fields from "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory       |
	| PlanningBOMCreated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "Import"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-112569 @Module-Bom
Scenario: 02 TC-112569 Import Planning Part BOM Line Added Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "LineAdded" Excel file for the "Part" tab
	Then User verifies valid data is present in the "LineAdded" Excel file 
	And User uploads the "LineAdded" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "1" data of "LineAdded" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	When User selects "Part" Tab  
	Then User recalls below fields from "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory         |
	| PlanningBOMLineAdded |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "1" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "Import"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container	

@NoNewBrowser @TCID-112570 @Module-Bom
Scenario: 03 TC-112570 Import Planning Part BOM Line Quantity Updated Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Update" Excel file for the "Part" tab
	Then User verifies valid data is present in the "Update" Excel file 
	And User uploads the "Update" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "0" data of "Update" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	When User selects "Part" Tab  
	Then User recalls below fields from "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory                   |
	| PlanningBOMLineQuantityUpdated |
	Then Verify "NumberOfValidContracts" and Item "Sku" are not empty in the "BestBOMPools" container and verify customer "type" in the "Customers" container of the "Pim" database for "Part" tab
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "Import"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-112571 @Module-Bom
Scenario: 04 TC-112571 Import Planning Part BOM Line Obsolete Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Obsolete" Excel file for the "Part" tab
	Then User verifies valid data is present in the "Obsolete" Excel file 
	And User uploads the "Obsolete" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "0" data of "Obsolete" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	When User selects "Part" Tab  
	Then User recalls below fields from "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory					|
	| PlanningBOMLineQuantityUpdated|
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "Import"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "2",BOMLines grid row "0" of Sku,Oem and Quantity in "Planning Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container

@NoNewBrowser @TCID-114559 @Module-Bom
Scenario: 05 TC-114559 Import Planning To Actual Part BOM Line Quantity Obsolete Event
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Create" Excel file for the "Part" tab
	Then User verifies valid data is present in the "Create" Excel file 
	And User uploads the "Create" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "0" data of "Create" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory     |
	| ActualBOMCreated |
	Then User is able to view the Import screen
	Then User clicks on "New-Import" button
	And User selects import type as "Part-BOM" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "Obsolete" Excel file for the "Part" tab
	Then User verifies valid data is present in the "Obsolete" Excel file 
	And User uploads the "Obsolete" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the BOM Line grid row "0" data of "Obsolete" Excel file matches with Cosmos DB using "Pim" database and "BOMs" container
	When User selects the "BOM" in Hamburger menu
	When User selects "Part" Tab  
	Then User recalls below fields from "Part" tab
	| FieldValue |
	| BOM-Kind   |
	| Oem        |
	| Sku        |
	Then Verify "Import" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory                 |
	| ActualBOMLineQuantityUpdated |
	Then Verify Source Kind and BOMLines grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab through "Import"
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then Verify Asset, SerialNumber,BOM header Sku and Oem,bomtype as "1",BOMLines grid row "0" of Sku,Oem and Quantity in "Actual Bom" record with "Integration-Data-Hub" database of "BOMs" container match with "Pim" database of "BestBOMPools" container for "Part" tab
	Then Verify ID and Partition Key generating from PIM BestBOMPool match with ID and Partition Key in "Integration-Data-Hub" database in "BOMs" container
