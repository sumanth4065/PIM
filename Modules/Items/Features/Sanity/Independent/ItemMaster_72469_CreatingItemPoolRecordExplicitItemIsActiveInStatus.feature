Feature: ItemMaster_72469_CreatingItemPoolRecordExplicitItemIsActiveInStatus
	
@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint13 @Module-Items
Scenario: ItemMaster_72469_CreatingItemPoolRecordExplicitItemIsActiveInStatus
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
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |	
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User gets the value of "LOB" and "OEM" and "Description"
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
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