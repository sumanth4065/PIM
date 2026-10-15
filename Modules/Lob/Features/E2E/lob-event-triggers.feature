Feature: Trigger and Validate LOB Events

   @UI @Module-Lob @TCID-102202
  Scenario: Scenario_1_1_TC_102202 Trigger Event on LOB Activation
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
	Then Verify the below field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	| LOB            |
	Then User navigates to LOB Page
	When User clicks on "New" button under "LOVs-LOB-To-GL" tab
	Then User recalls Line of Business "LOB-Name" from baseline screen in "LOB-To-GL" subtab
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
	Then User verifies the following Service details in LineOfBusinesses container of Pim database
	Then User verifies "Is-Active" status in LineOfBusinesses container of Pim database
	Then User verifies the following General Ledger code details in LineOfBusinesses container of Pim database
	| FieldNames            |
	| Billed-Account-Code   |
	| Billed-Division-Code  |
	| Unbilled-Account-Code |
	Then User verifies standardEventName "lineOfBusinessActivated" in Event-Audit container of Integration-Ingestion database
	Then User verifies the Service Taxonomy details in Services container of Integration-Data-Hub database
	Then User verifies the General Ledger code details in Services container of Integration-Data-Hub database
	Then User verifies "Is-Active" status in Services container of Integration-Data-Hub database
	Then User verifies the "Item" Id and "description" values in Services container of Integration-Data-Hub database

  @NoNewBrowser @Module-Lob @TCID-102203
  Scenario: Scenario_1_2_TC_102203 Trigger Event on LOB Update
	When User clicks on "Edit" button under "LOVs-LOB-To-GL" tab
	Then User enters "Priority-Rank" in "LOB-To-GL" subtab
	Then User enters text into the Internal "Notes" field
	When User clicks on "Update" button under "LOVs-LOB-To-GL" tab
	Then User clicks on Yes button on the pop up
	Then User waits for status to change to "Failed" in ChangeFeeds container of Pim database
	Then User verifies the following Service details in LineOfBusinesses container of Pim database
	Then User verifies "Is-Active" status in LineOfBusinesses container of Pim database
	Then User verifies the following General Ledger code details in LineOfBusinesses container of Pim database
	| FieldNames            |
	| Billed-Account-Code   |
	| Billed-Division-Code  |
	| Unbilled-Account-Code |
	Then User verifies standardEventName "lineOfBusinessUpdated" in Event-Audit container of Integration-Ingestion database
	Then User verifies the Service Taxonomy details in Services container of Integration-Data-Hub database
	Then User verifies the General Ledger code details in Services container of Integration-Data-Hub database
	Then User verifies "Is-Active" status in Services container of Integration-Data-Hub database
	Then User verifies the "Item" Id and "description" values in Services container of Integration-Data-Hub database
