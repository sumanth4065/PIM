Feature: Inactivate in Taxonomy and Activate in Explicit then verify Level lineage and Value

@UI @Module-Items @TCID-151186
Scenario: TC-151186 Inactivate In Taxonomy And Activate IsActive In Explicit With Validation
    Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed

	#Explicit item creation
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then User searches and selects "LOB" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
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
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes

	#Recall product taxonomy details in Taxonomy tab
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| FieldName  |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |

	#Uncheck the is Active checkbox for Prod Mgt>Clearview Rank
	Then User selects characteristic Kind as "Prod Mgt" and Type as "Clearview Rank"
    When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox    |
	| Active      |
	Then User sets "Active" checkbox in Item Master to "false"
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then User enters data into the characteristic "Internal-Notes" textarea field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	
    #Recall Explicit item id in Explicit mode
	When User clicks on the "Explicit" tab
	Then User recalls the created "Item" Id
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Explicit |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |

	#Checks the is Active checkbox for Prod Mgt>Clearview Rank
	Then User selects characteristic Kind as "Prod Mgt" and Type as "Clearview Rank"
    When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify the below characteristic checkboxes are checked
	| Checkbox   |
	| Active     |
	Then User sets "Active" checkbox in Item Master to "true"
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then User enters data into the characteristic "Internal-Notes" textarea field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |

	#Validate characteristic value and isActive status in PIM>Defaults
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory       |
	| ItemCreated        |
	| ItemCatalogCreated |
	Then User verifies that the "Primary" service attribute for characteristic kind and name is updated and derived from "Service" with the following updated values in "Pim" database and "ItemPools" container using "ItemPool" document
	| Field          |
	| Value          |
	| Active         |
	| Scope-Obsolete |
	