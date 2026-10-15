Feature: ItemMaster_108753_CreateNewBaselineItemIFISLOBDEFTrueAndLOBToGLISACTIVETRUEInlobTogltab

@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint13 @Module-Items
Scenario: ItemMaster_108753_CreateNewBaselineItemIFISLOBDEFTrueAndLOBToGLISACTIVETRUEInlobTogltab
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
| Billed-Division-Code  |
| Unbilled-Account-Code |
Then User enters text into the Internal "Notes" field
When User clicks on "Save" button under "LOVs-LOB-To-GL" tab
Then User clicks on Yes button on the pop up
When User selects the "Item" in Hamburger menu
Then Verify Item Master "Explicit" tab is displayed
When User clicks on "New" button in Item Master
Then User enters data for the following header fields
| FieldName   |
| Item-Id     |
| Oem         |
Then User recalls "LOB" from baseline screen
	