Feature: OEM Management
  This feature covers creation, validation, and modification of OEM details including groups, active state, and field updates.

@UI @Module-Items @TCID-26
Scenario: TC-26 Validate mandatory and max character limit errors while creating OEM
	Given User login to the PIM application using the ppt username and password
	When User navigates to "ItemOemFlow"
	Then User validates below fields with rules
    | FieldKey           | ValidateEmpty | ValidateMaxLength |
    | OemTextbox         | true          | true              |
    | GroupNameDropdown  | false         | false             |
    | DescriptionTextbox | true          | true              |

@NoNewBrowser @Module-Items @TCID-27
Scenario: TC-27 Create OEM with New group and validate in Explicit Item
	When User creates a new Oem with "New Group"
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	| OemSavedSuccessfullyToastMessage |
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	Then User recalls "Oem" from the admin page which has "Active" status
	Then User enters data for the following header fields
	| FieldName   |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-28
Scenario: TC-28 Create OEM with existing group and validate in Explicit Item
	When User navigates to "ConfigOemFlow"
	When User creates a new Oem with "Existing Group"
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	| OemSavedSuccessfullyToastMessage |
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	Then User recalls "Oem" from the admin page which has "Active" status
	Then User enters data for the following header fields
	| FieldName   |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-29
Scenario: TC-29 Modify OEM Active state to Inactive
	When User navigates to "ConfigOemFlow"
	Then User selects existing Oem and change Active status
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	| OemSavedSuccessfullyToastMessage |
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	Then User recalls "Oem" from the admin page which has "Inactive" status

@NoNewBrowser @Module-Items @TCID-30
Scenario: TC-30 Modify OEM Inactive state to Active
	When User navigates to "ConfigOemFlow"
	Then User selects existing Oem and change Active status
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	| OemSavedSuccessfullyToastMessage |
	When User navigates to "AdminItemFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	Then User recalls "Oem" from the admin page which has "Active" status
	Then User enters data for the following header fields
	| FieldName   |
	| LOB         |
	| Description |
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-31
Scenario: TC-31 Modify all OEM details except Active state and validate update
	When User navigates to "ConfigOemFlow"
	Then User modify existing Oem details
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	| OemSavedSuccessfullyToastMessage |
	Then User verifies modified details

	