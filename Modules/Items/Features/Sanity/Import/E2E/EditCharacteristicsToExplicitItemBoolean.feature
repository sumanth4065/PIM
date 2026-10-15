Feature: EditCharacteristicsToExplicitItemBoolean


@UI @Sanity @HappyPath @UserStory64014 @Automatable @P1 @UAT @Phase2_Sprint11 @Module-Items
Scenario: Scenario_2_1_ItemMaster_64891_EditCharacteristicsToExplicitItemEditDefaultValueNumberPercentageDecimal
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
	And User selects import type as "Characteristics-Data-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "DefaultBooleanValue" Excel file for the "Explicit" tab
	Then User verifies valid data is present in the "DefaultBooleanValue" Excel file 
	And User uploads the "DefaultBooleanValue" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the following fields from "DefaultBooleanValue" Excel
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| OEM         |
	| Division    |
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
	Then User selects characteristic kind and type from "DefaultBooleanValue" excel and verifies new characteristic "Value" is displayed in "Explicit" tab
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the "Explicit" tab below "DefaultBooleanValue" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName         |
	| Value             |
	| Notes             |
	| Active-Icon       |
	| Scope-Active-Icon |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |

@NoNewBrowser @Sanity @HappyPath @UserStory64014 @Automatable @P1 @UAT @Phase2_Sprint11 @Module-Items
Scenario: Scenario_2_2_ItemMaster_64892_EditCharacteristicsToExplicitItemEditAdminValueNumberPercentageAndDecimal
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Characteristics-Data-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "AdminBooleanValue" Excel file for the "Explicit" tab
	Then User verifies valid data is present in the "AdminBooleanValue" Excel file 
	And User uploads the "AdminBooleanValue" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the following fields from "AdminBooleanValue" Excel
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| OEM         |
	| Division    |
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
	Then User selects characteristic kind and type from "AdminBooleanValue" excel and verifies new characteristic "Value" is displayed in "Explicit" tab
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the "Explicit" tab below "AdminBooleanValue" excel field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName         |
	| Value             |
	| Notes             |
	| Active-Icon       |
	| Scope-Active-Icon |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "Import" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |