Feature: Validate Mandatory Field Errors and Character Limits for Characteristic Kind

@UI @Module-Items @TCID-22
Scenario: TC_22 Validate Mandatory Field Errors and Character Limits for Characteristic Kind
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminStandardFlow"
	Then User validates the Standard Characteristics page header
	Then User validates the default Characteristic Kind details
	Then User validates the selected default Characteristic Type details
	When User creates a new Characteristic Kind without mandatory details
	Then User validates mandatory field error messages for "Characteristic Kind"
	When User creates a new "Characteristic Kind" with the fields and validates the error messages
	| FieldName       |
	| KindName        |
	| KindRank        |
	| KindRankMemo    |
	| KindDescription |
	
