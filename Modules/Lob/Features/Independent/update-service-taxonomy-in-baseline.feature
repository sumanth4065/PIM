Feature: Update Service Taxonomy to Baseline Item

@UI @Module-Lob @TCID-135636
Scenario: TC_135636 Update Service Taxonomy Service Details Added To Baseline Data
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
	Then User gets the value of "LOB" and "OEM" and "Description"
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
	Then Verify the below "Service" Taxonomy field values in Cosmos DB using "Pim" database and "Defaults" container in "Item Default" document
	| FieldNames |
	| Class      |
	| Domain     |
	| Type       |
	| Item       |
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
	| Billed-Division-Code  |
	| Unbilled-Account-Code |
	Then User enters text into the Internal "Notes" field
	When User clicks on "Save" button under "LOVs-LOB-To-GL" tab
	Then User clicks on Yes button on the pop up
	Then User waits for status to change to "Failed" in ChangeFeeds container of Pim database
	Then User verifies the Service Taxonomy details in Event-Audit container of Integration-Ingestion database
	Then User verifies the General Ledger code details in Event-Audit container of Integration-Ingestion database
	Then User verifies the "Item" Id and "description" values in Services container of Integration-Data-Hub database
	Then User verifies the General Ledger code details in Services container of Integration-Data-Hub database