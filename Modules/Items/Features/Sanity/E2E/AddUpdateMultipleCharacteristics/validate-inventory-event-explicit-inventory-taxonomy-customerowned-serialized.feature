Feature: Validate Item Inventory Events with Explicit and Taxonomy Sourced Characteristics

@UI @Module-Items @TCID-147362
Scenario: TC-147362 Activate Item Inventory Eligible(E) Is Customer Owned(T) Is Serialized(T)
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Customer Owned"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Inventory Eligible"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	When User clicks on the "Explicit" tab
	When User clicks on "New" button in Item Master
	Then Verify "Edit" and "Save" buttons are disabled
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then Verify the below fields are not empty
	| FieldName |
	| Division  |
	Then Verify "Status" is displayed as "Pending"
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	Then Verify "Edit" and "New" buttons are disabled
	Then User gets the value of "LOB" and "OEM" and "Description"
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Serialized"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify the characteristic value in "Service Default" document in Pim DB
	| Characteristic Type | Expected Value |
	| is Serialized       | True           |
	Then Verify the characteristic value in "Taxonomy Default" document in Pim DB
    | Characteristic Type   | Expected Value |
    | is Customer Owned     | True           |
    | is Inventory Eligible | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory           |
	| ItemCreated            |
	| ItemCatalogCreated     |
	| ItemInventoryActivated |
	Then Verify the characteristic value in ItemPools document in Pim database
    | Characteristic Kind | Characteristic Type   | Expected Value |
    | Supply Chain        | is Inventory Eligible | True           |
    | Supply Chain        | is Serialized         | True           |
    | Supply Chain        | is Customer Owned     | True           |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value |
	| isCustomerOwned      | True           |
	| isInventoryEligible  | True           |
	| isSerialized         | True           |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| isInventoryEligible  | True           |
	Then Verify characteristic value in Item Catalog document Item node in IDH
	| Characteristic Value | Expected Value |
	| isCustomerOwned      | True           |
	| isSerialized         | True           |

@NoNewBrowser @Module-Items @TCID-147363
Scenario: TC-147363 Updated Item Inventory Eligible(E) Is Customer Owned(T) Is Serialized(T)
	When User clicks on "Edit" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify the characteristic value in "Service Default" document in Pim DB
	| Characteristic Type | Expected Value |
	| is Serialized       | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory         |
	| ItemUpdated          |
	| ItemCatalogUpdated   |
	| ItemInventoryUpdated |
	Then Verify the characteristic value in ItemPools document in Pim database
    | Characteristic Kind | Characteristic Type   | Expected Value |
    | Supply Chain        | is Inventory Eligible | True           |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value |
	| isInventoryEligible  | True           |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| isInventoryEligible  | True           |
	