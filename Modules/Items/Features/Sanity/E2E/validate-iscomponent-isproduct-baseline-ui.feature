 Feature: Validate isproduct iscomponent in Baseline item creation From UI

@UI @Module-Items @TCID-150946
Scenario: TC-150946 Create Baseline Item validate isproduct and iscomponent
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
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldName      |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
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

@NoNewBrowser @Module-Items @TCID-150947
Scenario: TC-150947 Edit Baseline Item validate isproduct and iscomponent
	When User clicks on "Edit" button in Item Master
	Then User Selects "Component" radio button
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
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
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
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