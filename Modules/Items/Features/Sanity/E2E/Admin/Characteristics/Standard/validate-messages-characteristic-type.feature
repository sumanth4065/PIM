Feature: Validation messages for Characteristic Type

@UI @Module-Items @TCID-23
Scenario: TC_23 Validate Mandatory Field Errors and Character Limits for Characteristic Type
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminStandardFlow"
	Then User validates the Standard Characteristics page header
	Then User validates the default Characteristic Kind details
	Then User validates the selected default Characteristic Type details
	When User creates a new Characteristic Type without mandatory details
	Then User validates mandatory field error messages for "Characteristic Type"
	When User creates a new "Characteristic Type" with the fields and validates the error messages
	| FieldName       |
	| TypeName        |
	| TypeRankMemo    |
	| TypeDescription |