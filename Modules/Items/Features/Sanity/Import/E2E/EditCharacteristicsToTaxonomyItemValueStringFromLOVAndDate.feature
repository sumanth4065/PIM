Feature: EditCharacteristicsToTaxonomyItemValueStringFromLOVAndDate

@UI @Sanity @ItemMasterImport @UserStory64014 @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_1_ItemMaster_64887_EditCharacteristicsToTaxonomyItemDefaultValueStringFromLOVAndDate
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies valid data is present in the "CreateItem" Excel file 
	And User uploads the "CreateItem" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Taxonomy-Data-Baseline-Taxonomy" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies valid data is present in the "ProductTaxonomyCreate" Excel file 
	And User uploads the "ProductTaxonomyCreate" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Characteristics-Data-Taxonomy" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "DefaultStringLov" Excel file for the "Taxonomy" tab
	Then User verifies valid data is present in the "DefaultStringLov" Excel file 
	And User uploads the "DefaultStringLov" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls "item" id from "CreateItem" Excel
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	When User clicks on the "Taxonomy" tab
	Then User recalls the following taxonomy fields from "DefaultStringLov" Excel
	| FieldName |
	| OEM       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic kind and type from "DefaultStringLov" excel and verifies new characteristic "Value" is displayed in "Taxonomy" tab
	Then Verify the "Taxonomy" tab below "DefaultStringLov" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldName   |
	| Value       |	
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Taxonomy Default" document
	Then Verify Explicit Values fields are enabled
	| FieldName          |
	| Edit               |
	| Add-Characteristic |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify the "Taxonomy" tab below "DefaultStringLov" excel characteristic field "Value" in Cosmos DB using "Pim" database and "ItemPools" container in "ItemPool" document

@NoNewBrowser @ItemMasterImport @Sanity @UserStory64014 @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_2_ItemMaster_64888_EditCharacteristicsToTaxonomyItemAdminValueStringFromLOVAndDate	
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Characteristics-Data-Taxonomy" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "AdminStringLov" Excel file for the "Taxonomy" tab
	Then User verifies valid data is present in the "AdminStringLov" Excel file 
	And User uploads the "AdminStringLov" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls "item" id from "CreateItem" Excel
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	When User clicks on the "Taxonomy" tab
	Then User recalls the following taxonomy fields from "AdminStringLov" Excel
	| FieldName |
	| OEM       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic kind and type from "AdminStringLov" excel and verifies new characteristic "Value" is displayed in "Taxonomy" tab
	Then Verify the "Taxonomy" tab below "AdminStringLov" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldName   |
	| Value       |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Taxonomy Default" document
	Then Verify Explicit Values fields are enabled
	| FieldName          |
	| Edit               |
	| Add-Characteristic |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify the "Taxonomy" tab below "AdminStringLov" excel characteristic field "Value" in Cosmos DB using "Pim" database and "ItemPools" container in "ItemPool" document