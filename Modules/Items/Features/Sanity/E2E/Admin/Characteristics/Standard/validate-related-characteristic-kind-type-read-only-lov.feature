Feature: Validate Related Characteristic Kind And Type except Read-Only properties and LOV Value

@UI @Module-Items @TCID-03
Scenario: TC_03 Validate Related CharKind and Type with All Properties except Read-Only and LOV in Item Management
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
	When User navigates to "AdminItemFlow"
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
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	When User clicks on "Add-Characteristic" button in Characteristic display Panel
	When User selects the Characteristic Type option from Characteristic Type drop down
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox       |
	| scope-obsolete |
	| Active         |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkbox         |
	| service-specific |
	Then Verify the below characteristic checkboxes are checked
	| Checkbox         |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkbox       |
	| scope-obsolete |
	Then User enters data into the characteristic value field
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User verifies that the grid displays the added characteristic with From Level "E"
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Add-Level  |
	| Undo       |
	| Save       |
	| Edit       |
	| History    |
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |