Feature: Validate Restricted Characteristic Kind and Type and Status Handling for User in Item Management

@UI @Module-Items @TCID-05
Scenario: TC_05 Validate Restricted CharKind & Type with Track History, Explicit Value & LOV in Item
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminStandardFlow"
	Then User validates the Standard Characteristics page header
	Then User validates the default Characteristic Kind details
	Then User validates the selected default Characteristic Type details
	When User creates a new Characteristic Kind
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                         |
	| SuccessToastTitle                 |
	Then User validates the searched Characteristic Kind details
	When User creates a new Characteristic Type
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                         |
	| SuccessToastTitle                 |
	Then User validates the selected default Characteristic Type details
	When User navigates to "AdminValueFlow"
	Then User Validates the Value page header
	Then User Validates the selected default Characteristic Kind and Characteristic LOV details in Value
	When User searches existing Characteristic Kind
	| FieldName                |
	| LOVCharKindSearchTextBox |
	When User creates New LOV value
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                        |
	| SuccessToastTitle                |
	When User navigates to "AdminUserManagementFlow"
	Then User validates the Authorized Users page header
	Then User validates the default Characteristic Kind and User details
	When User searches existing Characteristic Kind
	| FieldName                       |
	| CharacteristicKindSearchTextBox |
	| UserSearchTextBox               |
	When User modifies the following User Details
	| FieldName            |
	| ActiveCheckBox       |
	| InternalNotesTextBox |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName                         |
	| SuccessToastTitle                 |
	When User clicks on the Log Out button
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminItemManagementFlow"
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
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
	| Oem         |
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then User gets the value of "LOB" and "OEM" and "Description"
	Then Verify that the following radio button fields are displayed
	| FieldName |
	| Product   |
	| Component |
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
	When User clicks on "Product" Taxonomy
	Then User enters "Product" Taxonomy details and clicks on "Save" button if empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	When User selects the created Characteristic Kind
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	| Add-Level          |
	| Undo               |
	| Save               |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |


@NoNewBrowser @Module-Items @TCID-24
Scenario: TC_24 Validate Error message when changed user to Inactive for Characteristic kind
	When User navigates to "AdminUserFlow"
	Then User validates the Authorized Users page header
	Then User validates the default Characteristic Kind and User details
	When User searches existing Characteristic Kind
	| FieldName                       |
	| CharacteristicKindSearchTextBox |
	| UserSearchTextBox               |
	When User modifies the following User Details
	| FieldName            |
	| ActiveCheckBox       |
	Then User validates mandatory field error messages for "User Management"