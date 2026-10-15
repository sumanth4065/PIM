Feature: Validate Product Class Creation and Modification in Explicit Item Creation

@UI @Module-Items @TCID-36
Scenario: TC-36 Validate UI Error Messages for Product Class
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminClassFlow"
	Then Verify User is on Product Class Screen
	When User clicks on the Product Class "Create" button
	When User clicks on the Product Class "Save" button
	Then Verify the below product class fields error messages
	| FieldName   |
	| ClassName   |
	| Description |
	Then Verify the character limit error message for the below product class fields
	| FieldName   |
	| Description |
	| ClassName   |
	
@NoNewBrowser @Module-Items @TCID-37
Scenario: TC-37 Create Product Class Validate Explicit Item Creation
	When User refreshes the page
	Then Verify User is on Product Class Screen
	When User clicks on the Product Class "Create" button
	When User enters product class details
	When User clicks on the Product Class "Save" button
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

@NoNewBrowser @Module-Items @TCID-39
Scenario: TC-39 Search Product Class Modify InActive Validate Explicit Item Creation
	When User navigates to "ClassFlow"
	When User Searches and selects existing Product Class
	When User clicks on the Product Class "Edit" button
	When User unchecks the product class active checkbox
	When User enters valid data into the product class Internal Notes field
	When User clicks on the Product Class "Update" button
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
	| Product-Class    |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-38
Scenario: TC-38 Search Product Class Modify Active Validate Explicit Item Creation
	When User navigates to "ClassFlow"
	When User Searches and selects existing Product Class
	When User clicks on the Product Class "Edit" button
	When User checks the product class active checkbox
	When User enters valid data into the product class Internal Notes field
	When User clicks on the Product Class "Update" button
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
	| Product-Class    |
	Then verify no data found



