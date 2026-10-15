Feature: Update Product class and Category for an Explicit Item

@UI @Module-Items @TCID-67889
Scenario: TC_67889 Update Product class and Category for an Explicit Item
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then User searches and selects "LOB" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes	
	Then User recalls the created "Item" Id
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
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
	When User clicks on "Edit" button in Item Master
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes	
	Then User recalls the created "Item" Id
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User verifies product class and category updated in "Item Default" document
	| Class      | Category      |
	| AUTO_CLASS | AUTO_CATEGORY |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemUpdated                 |
	| ItemCatalogUpdated          |
	Then User verifies product class and category updated in "ItemPools" document 
	| Class      | Category      |
	| AUTO_CLASS | AUTO_CATEGORY |
	Then User verifies product class and category updated in "Item" document 
	| Class      | Category      |
	| AUTO_CLASS | AUTO_CATEGORY |
	Then User verifies product class and category updated in "ItemCatalog" document 
	| Class      | Category      |
	| AUTO_CLASS | AUTO_CATEGORY |
	


	