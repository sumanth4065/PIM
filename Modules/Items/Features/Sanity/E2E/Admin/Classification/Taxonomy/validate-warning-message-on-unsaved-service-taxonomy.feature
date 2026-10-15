Feature: Unsaved Service Taxonomy Warning
 
@UI @Module-Items @TCID-150934
Scenario: TC_150934 Validate Warning Message When Navigating Away Without Saving For Service Taxonomy
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminClassificationTaxonomyFlow"
	Then User validates the Taxonomy page header
	When User selects option "service" Taxonomy
	When User creates Taxonomy for below entity
	| ServiceTaxonomy |
	| Class           |
	Then User validates the Save Changes pop up and clicks on Yes button
	When User searches for the value in the below field
	| FieldName      |
	| ClassSearchBox |
	When User attempts to navigate away from the current page by selecting the below tab
	| ServiceTaxonomy |
	| Domain          |
	When User navigates to "AdminItemFlow"
	Then User validates the toast message
	| FieldName           |
	| WarningToastTitle   |
	| WarningToastMessage |