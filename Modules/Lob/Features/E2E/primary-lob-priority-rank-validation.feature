Feature: Priority Rank Validation for Primary LOB

 @UI @Module-Lob @TCID-101361
  Scenario: Scenario_1_1_TC_101361 Assign Priority Rank Unique Primary LOB
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| Description |
	Then User gets "Item-Id" value
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User checks "LOB" deff checkbox
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User gets the value of "LOB" and "OEM" and "Description"
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Service" Taxonomy details
	| Fields |
	| Class  |
	| Domain |
	| Type   |
	| Item   |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then User navigates to LOB Page
	When User clicks on "New" button under "LOVs-LOB-To-GL" tab
	Then User recalls Line of Business "LOB-Name" from baseline screen in "LOB-To-GL" subtab
	Then User verifies the following fields are enabled
	| FieldNames            |
	| Is-Active             |
	| Billed-Account-Code   |
	| Billed-Division-Code  |
	| Unbilled-Account-Code |
	| Priority-Rank         |
	Then User verifies "Save" button is enabled under "Lovs-LOB-To-GL" tab
	Then Verify below service taxonomy details are auto populated and not editable
	| FieldNames            |
	| Class                 |
	| Domain                |
	| Type                  |
	| Item                  |
	Then User enters "Priority-Rank" in "LOB-To-GL" subtab
	Then User enters data for below fields in "LOB-To-GL" subtab
	| FieldNames            |
	| Billed-Account-Code   |
	| Unbilled-Account-Code |
	| Billed-Division-Code  |
	Then User enters text into the Internal "Notes" field
	When User clicks on "Save" button under "LOVs-LOB-To-GL" tab
	Then User clicks on Yes button on the pop up
	Then User waits for status to change to "Failed" in ChangeFeeds container of Pim database
	Then User verifies the Service Taxonomy details in Event-Audit container of Integration-Ingestion database
	Then User verifies the General Ledger code details in Event-Audit container of Integration-Ingestion database
	Then User verifies the following General Ledger code description details in Event-Audit container of Integration-Ingestion database
	| FieldNames                 |
	| Billed-Account-Code-Name   |
	| Unbilled-Account-Code-Name |
	| Billed-Division-Code-Name  |
	Then User verifies the "Item" Id and "description" values in Services container of Integration-Data-Hub database

  @NoNewBrowser @Module-Lob @TCID-101362
  Scenario: Scenario_1_2_TC_101362 Assign Priority Rank With Existing Primary LOB
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| Description |
	Then Verify "LOB" checkbox is displayed
	Then Verify "Status" is displayed as "Pending"
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldNames |
	| Alias      |
	| Test       |
	Then Verify the below fields are empty
	| FieldNames      |
	| Product-Class   |
	| Product-Category|
	Then User gets "Item-Id" value
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User checks "LOB" deff checkbox
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Service" Taxonomy details
	| Fields |
	| Class  |
	| Domain |
	| Type   |
	| Item   |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then User verifies the Item Master "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "Defaults" container in "Item Default" document
	Then User navigates to LOB Page
	When User clicks on "New" button under "LOVs-LOB-To-GL" tab
	Then User recalls Line of Business "LOB-Name" from baseline screen in "LOB-To-GL" subtab
	Then User enters existing "Priority-Rank" in "LOB-To-GL" subtab
	Then User enters data for below fields in "LOB-To-GL" subtab
	| FieldNames            |
	| Billed-Account-Code   |
	| Unbilled-Account-Code |
	| Billed-Division-Code  |
	Then User enters text into the Internal "Notes" field
	When User clicks on "Save" button under "LOVs-LOB-To-GL" tab
	Then User clicks on Yes button on the pop up
	Then User verifies the popup displays the expected message "DuplicatePriorityRank"