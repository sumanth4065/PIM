Feature: Service Taxonomy - Create, Edit, and Recall

@UI @Module-Items @TCID-150928
Scenario: TC_150928 Validate service Taxonomy create and recall
	Given User login to the PIM application using the ppt username and password
	When User navigates to "AdminClassificationTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Class           |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Class           |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ServiceTaxonomy |
	| Class           |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName      |
	| ClassSearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Domain          |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Domain          |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ServiceTaxonomy |
	| Domain          |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName       |
	| DomainSearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Type            |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Type            |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ServiceTaxonomy |
	| Type            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName     |
	| TypeSearchBox |
	When User creates Taxonomy without mandatory details
	Then User validates mandatory field error messages for "Taxonomy"
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Item            |
	Then User validates the Warning pop up and clicks on "No" button
	When User creates Taxonomy to validate warning pop up for below entity
	| ServiceTaxonomy |
	| Item            |
	Then User validates the Warning pop up and clicks on "Yes" button
	When User creates Taxonomy for below entity
	| ServiceTaxonomy |
	| Item            |
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
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Service" Taxonomy
	| FieldName |
	| Class     |
	| Domain    |
	| Type      |
	| Item      |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes

@NoNewBrowser @Module-Items @TCID-150929
Scenario: TC_150929 Validate Service Taxonomy edit functionality
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User searches for the value in the below field
	| FieldName          |
	| EditClassSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Class           |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName      |
	| ClassSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Domain          |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName       |
	| DomainSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Type            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName     |
	| TypeSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Item            |
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |

@NoNewBrowser @Module-Items @TCID-150930
Scenario: TC_150930 Search Class and modify to Inactive and validate Item Management
	When User refreshes the page
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User searches for the value in the below field
	| FieldName          |
	| EditClassSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Class           |
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
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Service" Taxonomy
	| FieldName |
	| Class     |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-150931
Scenario:  TC_150931 Search Domain and modify to Inactive and validate Item Management
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User searches for the value in the below field
	| FieldName          |
	| EditClassSearchBox |
	When User reactivates the existing Taxonomy
	Then User validates the Save Changes pop up and clicks on Yes button
	When User searches for the value in the below field
	| FieldName          |
	| ClassSearchBox	 |
	When User edits the existing Taxonomy
	| ServiceTaxonomy	|
	| Domain			|
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
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Service" Taxonomy
	| FieldName |
	| Class     |
	When User enters below data in "Service" Taxonomy
	| FieldName	|
	| Domain	|
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-150932
Scenario: TC_150932 Search Type and modify to Inactive and validate Item Management
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User searches for the value in the below field
	| FieldName          |
	| ClassSearchBox	 |
	When User reactivates the existing Taxonomy
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName          |
	| DomainSearchBox	 |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Type            |
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
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Service" Taxonomy
	| FieldName |
	| Class     |
	| Domain    |
	When User enters below data in "Service" Taxonomy
	| FieldName |
	| Type      |
	Then verify no data found

@NoNewBrowser @Module-Items @TCID-11
Scenario: TC_150933 Search Item and modify to Inactive and validate Item Management
	When User navigates to "AdminTaxonomyFlow"
	Then User validates the Taxonomy page header
	Then User validates the selected default Taxonomy type option
	When User selects option "service" Taxonomy
	When User searches for the value in the below field
	| FieldName          |
	| ClassSearchBox	 |
	When User searches for the value in the below field
	| FieldName          |
	| DomainSearchBox	 |
	When User reactivates the existing Taxonomy
	Then User validates the Save Changes pop up and clicks on Yes button
	Then User validates the toast message
	| FieldName         |
	| SuccessToastTitle |
	When User searches for the value in the below field
	| FieldName     |
	| TypeSearchBox |
	When User edits the existing Taxonomy
	| ServiceTaxonomy |
	| Item            |
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
	When User clicks on "Service" Taxonomy
	When User clicks on "Edit" button in Taxonomy section
	When User enters details in "Service" Taxonomy
	| FieldName |
	| Class     |
	| Domain    |
	| Type      |
	When User enters below data in "Service" Taxonomy
	| FieldName |
	| Item      |
	Then verify no data found