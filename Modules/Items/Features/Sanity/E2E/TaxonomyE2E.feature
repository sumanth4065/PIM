Feature: TaxonomyE2E

@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_10_1_ItemMaster_122289_AddCharacteristicExplicitScreenItemSaveYes
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then Verify "Status" is displayed as "Pending"
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details and clicks on "Save" button if empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User selects characteristic Kind as "TESTDATAKIND" and Type as "TESTING TYPE"	
	When User clicks on "Add-Characteristic" button in Characteristic display Panel
	Then User enters data into the characteristic "Type" field
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName       |	
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo		   |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User clicks on OK popup button
	When User clicks on "Edit" button in Characteristic display Panel
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |	
	Then Verify characteristic type is editable
	| FieldName        |
	| Value           |
	Then Verify below checkbox fields are not editable
	| FieldName            |
	| service-specific     |
	Then Verify below checkbox fields are editable and edit the flag
	| FieldName      |
	| scope-obsolete |
	Then User enters data into the characteristic "Internal-Notes" textarea field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below buttons are disabled in Characteristic display Panel
	| FieldName   |
	| Save        |
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldName      |
	| Active         |
	| Scope-Obsolete |
	| Value          |
	| Internal-Notes |
	When User clicks on the "Baseline" tab
	When User clicks on the "Taxonomy" tab
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Taxonomy Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_10_2_ItemMaster_122291_AddLevelValueTaxonomyScreenSaveYes	
	Then User selects characteristic Kind as "TESTDATAKIND" and Type as "TESTING TYPE"	
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	Then User clicks the enabled button from "Add-Level" or "Edit" button	
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then User enters data into the characteristic "Boolean" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldName      |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	| Boolean        |
	When User clicks on the "Baseline" tab
	When User clicks on the "Taxonomy" tab
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Taxonomy Default" document


@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario:Scenario_10_3_ItemMaster_122292_EditTaxonomyDefaultValueLevelManagementSKU_TaxonomyScreenSaveYES
	Then User selects characteristic Kind as "TESTDATAKIND" and Type as "TESTING TYPE"
	When User clicks on "Edit" button in Characteristic display Panel
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |	
	Then Verify characteristic type is editable
	| FieldName |
	| Boolean   |
	Then Verify below checkbox fields are not editable
	| FieldName            |
	| service-specific     |
	Then Verify below checkbox fields are editable and edit the flag
	| FieldName      |
	| scope-obsolete |
	Then User enters data into the characteristic "Internal-Notes" textarea field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below buttons are disabled in Characteristic display Panel
	| FieldName   |
	| Save        |
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldName      |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	| Boolean        |
	When User clicks on the "Baseline" tab
	When User clicks on the "Taxonomy" tab
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Taxonomy Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_10_4_ItemMaster_122293_EditUserDefinedTaxonomyBaselineScreen
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Description        |
	| Right-Panel-Status |
	Then Verify text is displayed for below fields
	| FieldName      |
	| Internal-Notes |
	Then Verify radio button is displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then Verify "Undo" and "Save" buttons are disabled
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "User Taxonomy" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "User" Taxonomy details
	| FieldName |
	| Service   |
	| Domain    |
	| Class     |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify the below "User" Taxonomy details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName |
	| Service   |
	| Domain    |
	| Class     |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document