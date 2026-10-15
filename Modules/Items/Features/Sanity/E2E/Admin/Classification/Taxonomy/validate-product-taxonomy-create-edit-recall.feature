Feature: Product Taxonomy - Create, Edit, and Recall

@UI @Module-Items @TCID-150871
Scenario: TC_150871 Validate Product Taxonomy create and recall
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminClassificationTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User searches for the value in the below field
	| FieldName    |
	| OemSearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Family          |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Family          |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ProductTaxonomy |
	| Family          |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName       |
	| FamilySearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Line            |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Line            |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ProductTaxonomy |
	| Line            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName     |
	| LineSearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Group           |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ProductTaxonomy |
	| Group           |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ProductTaxonomy |
	| Group           |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |

	When User navigates to "AdminItemFlow"
	Then Verify Item Master "Explicit" tab is displayed
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
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Product" Taxonomy
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-150923
Scenario: TC_150923 Validate Product Taxonomy edit functionality
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User searches for the value in the below field
	| FieldName    |
	| OemSearchBox |
	When User edits the existing Taxonomy
	| ProductTaxonomy |
	| Family          |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName       |
	| FamilySearchBox |
	When User edits the existing Taxonomy
	| ProductTaxonomy |
	| Line            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName     |
	| LineSearchBox |
	When User edits the existing Taxonomy
	| ProductTaxonomy |
	| Group           |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |

@NoNewBrowser @Module-Items @TCID-150924
Scenario: TC_150924 Validate Product Taxonomy with inactive Family
	When User refreshes the page
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User searches for the value in the below field
	| FieldName    |
	| OemSearchBox |
	When User edits the existing Taxonomy
	| ProductTaxonomy |
	| Family          |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User navigates to "AdminItemFlow"
	Then Verify Item Master "Explicit" tab is displayed
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
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Product" Taxonomy
	| FieldName |
	| Family    |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-150925
Scenario: TC_150925 Validate Product Taxonomy with inactive Line
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User searches for the value in the below field
	| FieldName    |
	| OemSearchBox |
	When User searches for the value in the below field
	| FieldName       |
	| FamilySearchBox |
	When User reactivates the existing Taxonomy
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User edits the existing Taxonomy
	| ProductTaxonomy |
	| Line            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |

	When User navigates to "AdminItemFlow"
	Then Verify Item Master "Explicit" tab is displayed
	When User refreshes the page
	Then Verify Item Master "Explicit" tab is displayed
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
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Product" Taxonomy
	| FieldName |
	| Family    |
	| Line      |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-150926
Scenario: TC_150926 Validate Product Taxonomy with inactive Group
  When User navigates to "AdminTaxonomyFlow"
  Then User validates the Taxonomy page header
  Then User validates the selected default Taxonomy type option
  When User searches for the value in the below field
  | FieldName    |
  | OemSearchBox |
  When User searches for the value in the below field
  | FieldName       |
  | FamilySearchBox |
  When User reactivates the existing Taxonomy
  Then User validates the Save Changes pop up and clicks on Yes button
  Then User validates the toast message
  | FieldName         |
  | SuccessToastTitle |
  When User searches for the value in the below field
  | FieldName     |
  | LineSearchBox |
  When User edits the existing Taxonomy
  | ProductTaxonomy |
  | Group           |
  Then User validates the Save Changes pop up and clicks on Yes button
  Then User validates the toast message
  | FieldName                          |
  | SuccessToastTitle                  |
  | GroupSavedSuccessfullyToastMessage |

  When User navigates to "AdminItemFlow"
  Then Verify Item Master "Explicit" tab is displayed
  When User refreshes the page
  Then Verify Item Master "Explicit" tab is displayed
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
  | FieldName        |
  | Product-Class    |
  | Product-Category |
  When User clicks on "Save" button in Item Master
  Then User confirms the Save changes
  Then User recalls the created "Item" Id
  Then Verify "Product" Taxonomy is displayed
  When User clicks on "Edit" button in Taxonomy section
  When User enters details in "Product" Taxonomy
  | FieldName |
  | Family    |
  | Line      |
  | Group     |
  Then verify no data found

	