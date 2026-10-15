Feature: Validate Quoting Events for Taxonomy PPT Supportability and Baseline Quote Eligible

@UI @Module-Items @TCID-147338
Scenario: TC-147338 Activate Item with PPT Supportability(T) and Quote Eligible(B)
    Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on the "Taxonomy" tab
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then User selects characteristic Kind as "Prod Mgt" and Type as "PPT Supportability"	
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Value" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes

	#Explicit 
	When User clicks on the "Explicit" tab
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

	#Baseline and product taxonomy create
	When User clicks on the "Baseline" tab
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
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "Prod Mgt" and Type as "is Quote Eligible"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify the characteristic value in "Taxonomy Default" document in Pim DB
    | Characteristic Type | Expected Value                    |
    | PPT Supportability  | PPT New Supported Renew Supported |
	Then Verify the characteristic value in "Item Default" document in Pim DB
    | Characteristic Type | Expected Value |
    | is Quote Eligible   | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	| ItemCatalogQuotingActivated |
	Then Verify the characteristic value in ItemPools document in Pim database
    | Characteristic Kind | Characteristic Type | Expected Value                    |
    | Prod Mgt            | is Quote Eligible   | True                              |
    | Prod Mgt            | PPT Supportability  | PPT New Supported Renew Supported |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value |
	| IsQuoteEligible      | True           |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| IsQuoteEligible      | True           |

@NoNewBrowser @Module-Items @TCID-147339
Scenario: TC-147339 Update Item with PPT Supportability(T) and Quote Eligible(B)
	When User clicks on "Edit" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify the characteristic value in "Item Default" document in Pim DB
    | Characteristic Type | Expected Value |
    | is Quote Eligible   | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory              |
	| ItemUpdated               |
	| ItemCatalogUpdated        |
	| ItemCatalogQuotingUpdated |
	Then Verify the characteristic value in ItemPools document in Pim database
    | Characteristic Kind | Characteristic Type | Expected Value |
    | Prod Mgt            | is Quote Eligible   | True           |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value |
	| IsQuoteEligible      | True           |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| IsQuoteEligible      | True           |