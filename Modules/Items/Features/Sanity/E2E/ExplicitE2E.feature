Feature: ExplicitE2E
	
@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_1_ItemMaster_122275_CreateNewItemIDExplicitSave
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
	Then User selects "Active" from the "Status" dropdown
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldNames |
	| Alias      |
	| Test       |
	Then Verify the below fields are empty
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
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
	Then User gets the value of "LOB" and "OEM" and "Description"
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Status         |
	Then Verify the created and modified details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the service created and modified details in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_2_ItemMaster_122287_EditServiceTaxonomyExplicitScreenSave
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Service" Taxonomy details
	| Fields |
	| Class  |
	| Domain |
	| Type   |
	| Item   |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the created and modified details in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then Verify the below "Service" Taxonomy field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames |
	| Class      |
	| Domain     |
	| Type       |
	| Item       |
	Then User verifies the Item Master service "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_3_ItemMaster_122281_AddCharacteristicExplicitScreenItemSaveYes
	When User clicks on "Product" Taxonomy
	Then User enters "Product" Taxonomy details and clicks on "Save" button if empty
	| Fields |
	| Family |
	| Line   |
	| Group  |
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "TESTDATAKIND" and Type as "TESTDATATYPE"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames        |
	| Add-Characteristic |
	| Add-Level          |
	When User clicks on "Add-Characteristic" button in Characteristic display Panel
	Then User searches and selects characteristic "Type" from the dropdown
	Then Verify below characteristic field values are auto-populated and not editable
	| Field Name       |
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
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
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
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_4_ItemMaster_122283_AddLevelValueExplicitScreenSaveYes
	Then User selects characteristic Kind as "Billing" and Type as "Billing Type"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames        |
	| Add-Characteristic |
	| Add-Level          |
	When User clicks on "Add-Level" button in Characteristic display Panel
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
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
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
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document

@NoNewBrowser @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: Scenario_6_5_ItemMaster_122284_EditDefaultValueExplicitLevelManagementExplicitScreenSaveYes
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames |
	| Edit        |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonNames |
	| Add-Level   |
	| Undo        |
	| Save        |
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
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
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
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document

