Feature: Validate Item Pool Record Creation When Explicit Item Is Removed Status
	
@UI @Module-Items @TCID-141446
Scenario: TC-141446 Validate Item Pool Record for Explicit Item in Removed Status
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
	Then Verify "Status" is displayed as "Pending"
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then User selects "Removed" from the "Status" dropdown
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
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Status         |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then verify the document should not be created in ItemPool