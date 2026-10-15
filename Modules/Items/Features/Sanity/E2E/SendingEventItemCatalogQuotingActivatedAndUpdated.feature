Feature: SendingEventItemCatalogQuotingActivatedAndUpdated

@UI @ItemMaster @HappyPath @Sanity @Automatable @Phase2_Sprint13 @Module-Items
Scenario: Scenario_15_1_ItemMaster_72391_SendingEventItemCatalogQuotingActivated
	Given User login to the PIM application using the ppt username and password
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then Verify "Status" is displayed as "Pending"
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldName |
	| Alias     |
	| Test      |
	Then Verify the below fields are empty
	| FieldName        |
	| Product-Class    |
	| Product-Category |
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
	Then User gets the value of "LOB" and "OEM" and "Description"
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
	| Default-Characteristics-Baseline |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
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
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	| ItemCatalogQuotingActivated |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value                    |
	| PPTSupportability    | PPT New Supported Renew Supported |
	| isQuoteEligible      | True                              |

@NoNewBrowser @ItemMaster @HappyPath @Sanity @Automatable @Phase2_Sprint13 @Module-Items
Scenario: Scenario_15_2_ItemMaster_72392_SendingEventItemCatalogQuotingUpdated
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
	Then Verify below characteristic values displayed in "Pim" database and "Defaults" container in "Service Default" document
	| FieldName      |
	| Value          |
	| Internal-Notes |
	| Active         |
	| Scope-Obsolete |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory              |
	| ItemUpdated               |
	| ItemCatalogUpdated        |
	| ItemCatalogQuotingUpdated |
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value                     |
	| isQuoteEligible      | True                               |
	| PPTSupportability    | PPT New CaseByCase Renew Supported |
