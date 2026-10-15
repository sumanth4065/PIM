Feature: ItemMaster_72481_ValidatingItemPoolRecordLoadingRelatedItemFromExplicitMode

@UI @ItemMaster @Sanity @Automatable @UserStory67899 @Phase2_Sprint14 @Module-Items
Scenario: ItemMaster_72481_ValidatingItemPoolRecordLoadingRelatedItemFromExplicitMode
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
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
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Billing" and Type as "Billing Type"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames        |
	| Add-Characteristic |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonNames |
	| Undo        |
	| Save        |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkboxes       |
	| scope-obsolete   |
	| Active           |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkboxes       |
	| service-specific |
	Then Verify below characteristic field values are auto-populated and not editable
	| Field Name       |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify the below characteristic checkboxes are checked
	| Checkboxes       |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkboxes     |
	| scope-obsolete |
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
	Then User verifies that the grid displays the added characteristic with From Level "E"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames |
	| Edit        |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonNames |
	| Add-Level   |
	| Undo        |
	| Save        |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify the below fields in "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldNames     | DBFieldNames     |
	| Item             | ItemId           |
	| OEM              | OEM              |
	| Product-Class    | ProductClass	  |
	| Product-Category | ProductCategory  |
	Then Verify "Product" taxonomy fields in "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldNames | DBFieldNames  |
	| Family       | ProductFamily |
	| Line         | ProductLine   |
	| Group        | ProductGroup  |
	Then User verifies that the general item details using "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldName | DBFieldName |
	| Status    | isActive    |
	| Product   | isProduct   |
	| Component | isComponent |
	| Alias     | isAlias     |
	| Test      | isTest      |
	Then User verifies that the general item details are displayed in "Pim" database and "ItemPools" container using "ItemPool" document
	| FieldName     |
	| isValid       |
	| isArchived    |
	| isRemoved     |
	| isManaged     |
	| isPublishable |
	| isCalculated  |
	Then User verifies the service class details with processing type "Primary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies the relatedItem service class details with processing type "Primary" with itemId as "LOB" , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies that the "Primary" service attribute for characteristic kind and name is updated and derived from "Service" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |