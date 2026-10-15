Feature: Manage and Validate Item Processing Type in Item Pool Document

 @UI @Module-Items @TCID-120765
Scenario: TC-120765 Item Pool Processing Type Add New Explicit Item with LOB
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName |
	| Item-Id   |
	| Oem       |
	Then User searches and selects "LOB1" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
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
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
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
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Supply Chain" and Type as "Color"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox         |
	| scope-obsolete   |
	| Active           |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkbox         |
	| service-specific |
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify the below characteristic checkboxes are checked
	| Checkbox         |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkbox       |
	| scope-obsolete |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName  |
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
	| ButtonName  |
	| Edit        |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName  |
	| Add-Level   |
	| Undo        |
	| Save        |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |	
	Then Verify "Product" taxonomy fields in "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldName  | DBFieldName   |
	| Family       | ProductFamily |
	| Line         | ProductLine   |
	| Group        | ProductGroup  |
	Then User verifies that the general item details using "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldName | DBFieldName |
	| Status      | isActive    |
	| Product     | isProduct   |
	| Component   | isComponent |
	| Alias       | isAlias     |
	| Test        | isTest      |
	Then User verifies that the general item details are displayed in "Pim" database and "ItemPools" container using "ItemPool" document
	| FieldName     |
	| isValid       |
	| isArchived    |
	| isRemoved     |
	| isManaged     |
	| isPublishable |
	| isCalculated  |
	Then User verifies the service class details with processing type "Primary" with "Item" Id , "Oem" and "description" values in "Pim" database and "ItemPools" container using "ItemPool" document

@NoNewBrowser @Module-Items @TCID-120766
Scenario:TC-120766 Add Another Item to Item Pool Document
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
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes


	Then User recalls the created "Item" Id
	Then User searches and selects "LOB2" from dropdown
	When User clicks on "Product" Taxonomy
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
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Supply Chain" and Type as "Color"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox         |
	| scope-obsolete   |
	| Active           |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkbox         |
	| service-specific |
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify the below characteristic checkboxes are checked
	| Checkbox         |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkbox       |
	| scope-obsolete |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName  |
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
	| ButtonName  |
	| Edit        |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName  |
	| Add-Level   |
	| Undo        |
	| Save        |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |	
	Then Verify "Product" taxonomy fields in "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldName  | DBFieldName   |
	| Family       | ProductFamily |
	| Line         | ProductLine   |
	| Group        | ProductGroup  |
	Then User verifies that the general item details using "Pim" database and "ItemPools" container in "ItemPool" document
	| UIFieldName | DBFieldName |
	| Status      | isActive    |
	| Product     | isProduct   |
	| Component   | isComponent |
	| Alias       | isAlias     |
	| Test        | isTest      |
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
	
@NoNewBrowser @Module-Items @TCID-120767
Scenario:TC-120767 Change Item Pool Processing Type Inactive Lowest Rank LOB
	When User refreshes the page
	Then User recalls the created "Item" Id
	Then User searches and selects "LOB1" from dropdown
	When User clicks on "Edit" button in Item Master
	Then User selects "Inactive" from the "Status" dropdown
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User searches and selects "LOB1" from dropdown
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document 
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	
@NoNewBrowser @Module-Items @TCID-120768
Scenario:TC-120768 Validate Item Pool Document Processing Type
	Then Item LineOfBusiness array should have LOB2 as Primary and LOB1 as Secondary in the "Pim" database "ItemPools" container