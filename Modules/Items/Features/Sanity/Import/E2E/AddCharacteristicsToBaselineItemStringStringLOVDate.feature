Feature: AddCharacteristicsToBaselineItemStringStringLOVDate

@UI @Sanity @UserStory64014 @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_3_1_ItemMaster_64885_AddCharacteristicsToBaselineItemCreateDefaultValueStringFromLOVAndDate
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Baseline" from dropdown  
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
	And User selects import type as "Characteristics-Data-Baseline" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "DefaultString" Excel file for the "Baseline" tab
	Then User verifies valid data is present in the "DefaultString" Excel file 
	And User uploads the "DefaultString" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	Then User recalls the following fields from "DefaultString" Excel
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| OEM         |
	| Description |
	Then Verify that the following radio button fields are displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify that the following text box fields are displayed
	| FieldName |
	| Alias     |
	| Test      |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then Verify taxonomy characteristic kind fields are not empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic kind and type from "DefaultString" excel and verifies new characteristic "Value" is displayed in "Baseline" tab
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify the "Baseline" tab below "DefaultString" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName         |
	| Value             |
	| Notes             |
	| Active-Icon       |
	| Scope-Active-Icon |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document

@NoNewBrowser @Sanity @UserStory64014 @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_3_2_ItemMaster_64886_AddCharacteristicsToBaselineItemCreateAdminValueStringFromLOVDate
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Characteristics-Data-Baseline" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "AdminString" Excel file for the "Baseline" tab
	Then User verifies valid data is present in the "AdminString" Excel file 
	And User uploads the "AdminString" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	Then User recalls the following fields from "AdminString" Excel
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| OEM         |
	| Description |
	Then Verify that the following radio button fields are displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify that the following text box fields are displayed
	| FieldName |
	| Alias     |
	| Test      |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then Verify taxonomy characteristic kind fields are not empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify Explicit Values fields are enabled
	| FieldName          |
	| Edit               |
	| Add-Characteristic |
	| Add-Scope          |
	Then User selects characteristic kind and type from "AdminString" excel and verifies new characteristic "Value" is displayed in "Baseline" tab
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify the "Baseline" tab below "AdminString" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName         |
	| Value             |
	| Notes             |
	| Active-Icon       |
	| Scope-Active-Icon |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document
