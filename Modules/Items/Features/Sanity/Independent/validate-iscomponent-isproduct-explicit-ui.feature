 Feature: Valiadate isproduct iscomponent in Explicit item creation From UI

@UI @Module-Items
Scenario: TC-150944 Create Explicit Item validate isproduct and iscomponent
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName |
	| Item-Id   |
	| Oem       |
	Then User searches and selects "LOB" from dropdown
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
	| LOB         |
	| OEM         |
	| Division    |
	| Description |
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
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the following general flags in the "Item Default" document from "Pim" database and "Defaults" container
	| FieldName |
	| Product   |
	| Component |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory       |
	| ItemCreated        |
	| ItemCatalogCreated |
	Then Verify the following general flags in the ItemPool document from Pim database
	| FieldName |
	| Product   |
	| Component |
	Then Verify the following general flags in the Integration in Item document
	| FieldName   | ExpectedValue |
	| isProduct   | true          |
	| isComponent | false         |
	Then Verify the following general flags in the Integration in ItemCatalog document
	| FieldName   | ExpectedValue |
	| isProduct   | true          |
	| isComponent | false         |

@NoNewBrowser @Module-Items
Scenario: TC-150945 Edit Explicit Item validate isproduct and iscomponent
	When User clicks on "Edit" button in Item Master
	Then User Selects "Component" radio button
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| OEM         |
	| Division    |
	| Description |
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
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the following general flags in the "Item Default" document from "Pim" database and "Defaults" container
	| FieldName |
	| Product   |
	| Component |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory       |
	| ItemUpdated        |
	| ItemCatalogUpdated |
	Then Verify the following general flags in the ItemPool document from Pim database
	| FieldName |
	| Product   |
	| Component |
	Then Verify the following general flags in the Integration in Item document
	| FieldName   | ExpectedValue |
	| isProduct   | false         |
	| isComponent | true          |
	Then Verify the following general flags in the Integration in ItemCatalog document
	| FieldName   | ExpectedValue |
	| isProduct   | false         |
	| isComponent | true          |