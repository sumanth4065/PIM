Feature: Unsaved Product Taxonomy Warning

@UI @Module-Items @TCID-150927
Scenario: TC_150927 Validate Warning Message When Navigating Away Without Saving For Product Taxonomy
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminClassificationTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User searches for the value in the below field
	| FieldName    |
	| OemSearchBox |
	When User attempts to navigate away from the current page by selecting the below tab
	| ProductTaxonomy |
	| Family          |
	When User navigates to "AdminItemFlow"
	Then User validates the toast message
	| FieldName           |
	| WarningToastTitle   |
	| WarningToastMessage |
