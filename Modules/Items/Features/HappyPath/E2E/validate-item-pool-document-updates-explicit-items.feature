Feature: Validate Item Pool Document Updates for Explicit Items

@UI @Module-Items @TCID-72485
Scenario: TC-72485 Validate Item Pool Document Update for Primary Explicit Item
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

	When User clicks on "New" button in Item Master
	Then User recalls the created "Item-Id" Id
	Then User enters data for the following header fields
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB2" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
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
	| FieldName |
	| Product   |
	| Component |
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
	When User clicks on "Edit" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Inactive" from the "Status" dropdown
	Then User selects below explicit tab checkboxes
	| Checkbox |
	| Alias    |
	| Test     |
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
	Then Verify below checkboxes status in "Service Default" document
	| Checkbox |
	| Alias    |
	| Test     |
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
	| Product     | isProduct   |
	| Component   | isComponent |
	Then User verifies that the general item details are displayed in "Pim" database and "ItemPools" container using "ItemPool" document
	| FieldName     |
	| isValid       |
	| isArchived    |
	| isRemoved     |
	| isManaged     |
	| isPublishable |
	| isCalculated  |
	Then User verifies that the general item details are getting updated under "Secondary" service attribute in ItemPool
	| FieldName      |
	| Description    |
	| Internal-Notes |
	| Status         |
	| Test           |
	| Alias          |

@NoNewBrowser @Module-Items @Order2 @TCID-72486
Scenario: TC-72486 Validate Item Pool Document Update for Secondary Explicit Item
	When User refreshes the page
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
	| FieldName |
	| Product   |
	| Component |
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
	When User clicks on "Edit" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Inactive" from the "Status" dropdown
	Then User selects below explicit tab checkboxes
	| Checkbox |
	| Alias    |
	| Test     |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User searches and selects "LOB2" from dropdown
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify below checkboxes status in "Service Default" document
	| Checkbox |
	| Alias    |
	| Test     |
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
	| Product     | isProduct   |
	| Component   | isComponent |
	Then User verifies that the general item details are displayed in "Pim" database and "ItemPools" container using "ItemPool" document
	| FieldName     |
	| isValid       |
	| isArchived    |
	| isRemoved     |
	| isManaged     |
	| isPublishable |
	| isCalculated  |
	Then User verifies that the general item details are getting updated under "Secondary" service attribute in ItemPool
	| FieldName      |
	| Description    |
	| Internal-Notes |
	| Status         |
	| Test           |
	| Alias          |
