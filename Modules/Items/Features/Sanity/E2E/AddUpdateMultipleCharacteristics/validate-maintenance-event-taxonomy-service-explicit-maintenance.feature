Feature: Validate Maintenance Events for Taxonomy Service Product and Explicit Maintenance

@UI @Module-Items @TCID-147318
Scenario: TC-147318 Activate Item with Service Product(T) and Maintenance(E)
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
	Then User selects characteristic Kind as "Supply Chain" and Type as "Is Service Product"
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
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Maintenance Eligible"
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
	| Characteristic Type     | Expected Value |
	| is Maintenance Eligible | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                    |
	| ItemCreated                     |
	| ItemCatalogCreated              |
	| ItemCatalogMaintenanceActivated |
	Then Verify the characteristic value in ItemPools document in Pim database
	| Characteristic Kind | Characteristic Type     | Expected Value |
	| Supply Chain        | is Maintenance Eligible | True           |
	| Supply Chain        | Is Service Product      | True           |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value  | Expected Value |
	| isServiceProduct      | True           |
	| isMaintenanceEligible | True           |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value  | Expected Value |
	| isServiceProduct      | True           |
	| isMaintenanceEligible | True           |

@NoNewBrowser @Module-Items @TCID-147319
Scenario: TC-147319 Update Item with Service Product(T) and Maintenance(E)
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
	Then User selects characteristic Kind as "Supply Chain" and Type as "Is Service Product"
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
	Then Verify the characteristic value in "Taxonomy Default" document in Pim DB
	| Characteristic Type | Expected Value |
	| Is Service Product  | False          |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                   |
	| ItemUpdated					 |
	| ItemCatalogUpdated             |
	| ItemCatalogMaintenanceUpdated  |
	Then Verify the characteristic value in ItemPools document in Pim database
	| Characteristic Kind | Characteristic Type     | Expected Value |
	| Supply Chain        | Is Service Product      | False          |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value |
	| isServiceProduct     | False          |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| isServiceProduct     | False          |