Feature: Validate Is Service Specific Characteristic Update from Baseline for a Primary Item	

 @UI @Module-Items @TCID-72489
Scenario: TC-72489 Update Characteristic Primary Item from Baseline Is Service Specific
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
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
	| FieldName |
	| Oem       |
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
	Then User searches and selects "LOB1" from dropdown
	When User clicks on "Product" Taxonomy
	Then User enters "Product" Taxonomy details and clicks on "Save" button if empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on the "Baseline" tab
	Then User recalls the created "Item" Id
	Then User selects characteristic Kind as "Compliance" and Type as "ECCN"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "String" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify the characteristic value in "Item Default" document in Pim DB
    | Characteristic Type | Expected Value |
    | ECCN                | Main           |
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
	Then Verify the characteristic and levelLineage value under skuAttributes in ItemPools document in Pim database
	| Kind       | Type | Value | DerivedFrom | ItemValue |
	| Compliance | ECCN | Main  | Item        | Main      |