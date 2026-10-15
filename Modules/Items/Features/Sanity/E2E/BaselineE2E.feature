Feature: BaselineE2E
	
@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_5_1_ItemMaster_122273_CreateNewItemIDBaselineSave
	Given User login to the PIM application using the ppt username and password
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| Description |
	Then Verify "LOB" checkbox is displayed
	Then Verify "Status" is displayed as "Pending"
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldNames |
	| Alias      |
	| Test       |
	Then Verify the below fields are empty
	| FieldNames      |
	| Product-Class   |
	| Product-Category|
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify the created and modified details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_5_2_ItemMaster_122274_EditItemIdInBaselineSave
	When User clicks on "Edit" button in Item Master
	Then User enters text into the "Internal-Notes" field
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify the created and modified details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_5_3_ItemMaster_122285_EditProductTaxonomyBaselineScreenSave
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
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then Verify "Product" Taxonomy details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| Taxonomy Fields |
	| Oem             |
	| Family          |
	| Line            |
	| Group           |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_5_4_ItemMaster_122279_AddLevelValueBaselineScreenSaveYes
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "Billing" and Type as "Billing Type"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames        |
	| Add-Characteristic |
	| Add-Level          |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Value" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	When User clicks on "Edit" button in Characteristic display Panel
	Then Verify below characteristic field values are auto-populated and not editable
	| Field Name       |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames |
	| Undo        |
	| Save        |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonNames |
	| Add-level   |
	| History     |
	| Edit        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_5_5_ItemMaster_122280_EditBaselineDefaultValueLevelManagementBaselineScreenSaveYes
	When User clicks on "Edit" button in Characteristic display Panel
	Then Verify below characteristic field values are auto-populated and not editable
	| Field Name       |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        | 
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames |
	| Undo        |
	| Save        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	Then User enters text into the "Internal-Notes" field in Characteristic display Panel
	Then Verify "Service-Specific" checkbox is not editable
	Then User clicks the below characteristic checkboxes
	| Checkboxes     |
	| Scope-Obsolete |
	| Active         |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete  |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document



