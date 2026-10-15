Feature: Validate Item Pool Record Loading and Level Lineage Section in three Modes

@UI @Module-Items @TCID-72475
Scenario: 01 TC-72475 Validate Item Pool Record for Primary Explicit Item
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
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
	When User clicks on "New" button in Item Master
	Then User recalls the created "Item-Id" Id
	Then User enters data for the following header fields
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB2" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
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
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then User gets the value of "LOB" and "OEM" and "Description"
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
	Then User verifies that the "Primary" service attribute for characteristic kind and name is updated and derived from "Service" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |

@NoNewBrowser @Module-Items @TCID-72477
Scenario: 02 TC-72477 Validate Level Lineage Section of Item Pool for Taxonomy Item
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| Fields |
	| Oem    |
	| Family |
	| Line   |
	| Group  |
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Prod Mgt" and Type as "PPT Supportability"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonNames        |
	| Add-Characteristic |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonNames |
	| Undo        |
	| Save        |
	Then User clicks the enabled button from "Add-Level" or "Edit" button 
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
	| Scope            |
	Then Verify the below characteristic checkboxes are checked
	| Checkboxes       |
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
	Then User verifies that the grid displays the added characteristic with From Level "T"
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
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Taxonomy Default" document
	| FieldNames     |	
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	| Value          |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then User verifies the service class details with processing type "Primary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies that the "Primary" service attribute for characteristic kind and name is updated and derived from "Taxonomy" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |

@NoNewBrowser @Module-Items @TCID-72479
Scenario: 03 TC-72479 Validate Level Lineage Section of Item Pool for Baseline Item
	When User refreshes the page
	When User clicks on the "Baseline" tab
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
	Then User selects characteristic Kind as "Prod Mgt" and Type as "PPT Supportability"
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
	Then User verifies that the grid displays the added characteristic with From Level "B"
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
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Item Default" document
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
	Then User verifies the service class details with processing type "Secondary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies that the "Secondary" service attribute for characteristic kind and name is updated and derived from "Item" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |

@NoNewBrowser @Module-Items @TCID-72478
Scenario: 04 TC-72478 Validate Item Pool Record for Secondary Explicit Item
	When User refreshes the page
	When User clicks on the "Explicit" tab
	Then User recalls the created "Item" Id
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB2" from dropdown
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then User gets the value of "LOB" and "OEM" and "Description"
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
	| UIFieldNames     | DBFieldNames    |
	| Item             | ItemId          |
	| OEM              | OEM             |
	| Product-Class    | ProductClass    |
	| Product-Category | ProductCategory |
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
	Then User verifies the service class details with processing type "Secondary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies that the "Secondary" service attribute for characteristic kind and name is updated and derived from "Service" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |

@NoNewBrowser @Module-Items @TCID-72484
Scenario: 05 TC-72484 Validate Level Lineage for Characteristic Type with Three Modes
	Then Verify the below fields in "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldNames     | DBFieldNames    |
	| Item             | ItemId          |
	| OEM              | OEM             |
	| Product-Class    | ProductClass    |
	| Product-Category | ProductCategory |
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
	Then User verifies the service class details with processing type "Secondary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document
	Then User verifies that the "Secondary" service attribute for characteristic kind and name is updated and derived from "Service" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |







