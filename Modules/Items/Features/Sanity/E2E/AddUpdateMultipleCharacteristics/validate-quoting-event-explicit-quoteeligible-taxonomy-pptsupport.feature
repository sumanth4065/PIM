Feature: Validate Quoting Events for Explicit Quote Eligible and Taxonomy PPT Supportability

@UI @Module-Items @TCID-147330
Scenario: TC-147330 Activate Item with Quote Eligible(E) and PPT Supportability(T)
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Prod Mgt" and Type as "PPT Supportability"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Value" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	When User clicks on the "Explicit" tab
	When User clicks on "New" button in Item Master
	Then Verify "Edit" and "Save" buttons are disabled
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then Verify the below fields are not empty
	| FieldName |
	| Division  |
	Then Verify "Status" is displayed as "Pending"
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldName        |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	Then Verify "Edit" and "New" buttons are disabled
	Then User gets the value of "LOB" and "OEM" and "Description"
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
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify the characteristic value in "Service Default" document in Pim DB
	| Characteristic Type | Expected Value |
	| is Quote Eligible   | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	| ItemCatalogQuotingActivated |
	Then Verify the characteristic value in ItemPools document in Pim database
	| Characteristic Kind | Characteristic Type | Expected Value                      |
	| Prod Mgt            | PPT Supportability  | PPT New CaseByCase Renew CaseByCase |
	| Prod Mgt            | is Quote Eligible   | True                                |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item document in IDH
	| Characteristic Value | Expected Value                      |
	| isQuoteEligible      | True                                |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value                      |
	| PPTSupportability    | PPT New CaseByCase Renew CaseByCase |
	| isQuoteEligible      | True                                |

@NoNewBrowser @Module-Items @TCID-147331
Scenario: TC-147331 Update Item with Quote Eligible(E) and PPT Supportability(T)
	When User clicks on the "Taxonomy" tab
	Then Verify "Product" Taxonomy is displayed
	Then User enters "Product" Taxonomy details
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Prod Mgt" and Type as "PPT Supportability"
	When User clicks on "Edit" button in Characteristic display Panel
	Then User enters data into the characteristic "Value" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	Then Verify the characteristic value in "Taxonomy Default" document in Pim DB
	| Characteristic Type | Expected Value                    |
	| PPT Supportability  | PPT New Supported Renew Supported |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory              |
	| ItemUpdated               |
	| ItemCatalogUpdated        |
	| ItemCatalogQuotingUpdated |
	Then Verify the characteristic value in ItemPools document in Pim database
	| Characteristic Kind | Characteristic Type | Expected Value                    |
	| Prod Mgt            | PPT Supportability  | PPT New Supported Renew Supported |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value                    |
	| PPTSupportability   | PPT New Supported Renew Supported |