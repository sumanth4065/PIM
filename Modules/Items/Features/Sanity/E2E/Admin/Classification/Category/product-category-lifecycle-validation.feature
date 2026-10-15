Feature: Validate Product Category Creation and Modification in Explicit Item Creation

@UI @Module-Items @TCID-32
Scenario: TC-32 Validate UI Error Messages for Product Category
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminCategoryFlow"
	Then Verify User is on Product Category Screen
	When User clicks on the Product Category "Create" button
	When User clicks on the Product Category "Save" button
	Then Verify the below Product Category fields error messages
	| FieldName    |
	| CategoryName |
	| Description  |
	Then Verify the character limit error message for the below product Category fields
	| FieldName   |
	| Description |
	| CategoryName|
	
@NoNewBrowser @Module-Items @TCID-33
Scenario: TC-33 Create Product Category Validate Explicit Item Creation
	When User refreshes the page
	Then Verify User is on Product Category Screen
	When User clicks on the Product Category "Create" button
	When User enters Product Category details
	When User clicks on the Product Category "Save" button
	Then User validates the Save Changes pop up and clicks on Yes button
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters text into the "Internal-Notes" field
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-35
Scenario: TC-35 Search Product Category Modify InActive Validate Explicit Item Creation
	When User navigates to "CategoryFlow"
	When User Searches and selects existing Product Category
	When User clicks on the Product Category "Edit" button
	When User unchecks the product Category active checkbox
	When User enters valid data into the Product Category Internal Notes field
	When User clicks on the Product Category "Update" button
	Then User validates the Save Changes pop up and clicks on Yes button
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters text into the "Internal-Notes" field
	Then User enters data into below product fields
	| FieldName        |
	| Product-Category |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-34
Scenario: TC-34 Search Product Category Modify Active Validate Explicit Item Creation
	When User navigates to "CategoryFlow"
	When User Searches and selects existing Product Category
	When User clicks on the Product Category "Edit" button
	When User checks the product Category active check box
	When User enters valid data into the Product Category Internal Notes field
	When User clicks on the Product Category "Update" button
	Then User validates the Save Changes pop up and clicks on Yes button
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters text into the "Internal-Notes" field
	Then User enters data into below product fields
	| FieldName        |
	| Product-Category |
	Then verify no data found