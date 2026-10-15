Feature: SendingEventItemCatalogMaintenance

@UI @Sanity @HappyPath @Automatable @P1 @UAT @Redesign @Phase2_Sprint13 @Module-Items
Scenario: Scenario_11_1_ItemMaster_72393_SendingEventItemCatalogMaintenanceActivated
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
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Maintenance Eligible"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User selects characteristic Kind as "Supply Chain" and Type as "Is Service Product"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName     |
	| Created-Date  |
	| Created-User  |
	| Modified-Date |
	| modified-User |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                   |
	| ItemCreated					 |
	| ItemCatalogCreated             |
	| ItemCatalogMaintenanceActivated|
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |

@NoNewBrowser @Sanity @HappyPath @Automatable @P1 @UAT @Redesign @Phase2_Sprint13 @Module-Items 
Scenario: Scenario_11_2_ItemMaster_72394_SendingEventItemCatalogMaintenanceUpdated
	When User clicks on "Edit" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "Supply Chain" and Type as "is Maintenance Eligible"
	Then User selects characteristic Kind as "Supply Chain" and Type as "Is Service Product"
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName     |
	| Created-Date  |
	| Created-User  |
	| Modified-Date |
	| modified-User |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                   |
	| ItemUpdated					 |
	| ItemCatalogUpdated             |
	| ItemCatalogMaintenanceUpdated  |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify below field values are created in "Integration-Data-Hub" database and "Items" container for "Item" type
	| FieldNames            |
	| isMaintenanceEligible |
	| isServiceProduct      |

