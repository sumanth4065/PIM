Feature: Validate error messages without entering mandatory fields while creating New LOV

@UI @Module-Items @TCID-25
Scenario: Validate error messages without entering mandatory fields while creating New LOV
    Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminValueNavigation"
	Then User Validates the Value page header
	When User creates a new Value without mandatory details
    Then User validates mandatory field error messages for "Value"
	When User creates a new "Value" with the fields and validates the error messages
    | FieldName        |
    | LOVDescription   |
    | LOVName          |
    