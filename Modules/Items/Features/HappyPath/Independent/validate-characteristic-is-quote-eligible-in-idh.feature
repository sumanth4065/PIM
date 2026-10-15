Feature: Validate Characteristic Type Is Quote Eligible in IDH Document

@UI @Module-Items @TCID-121622
Scenario: TC_121622 Validate Characteristic Type Is Quote Eligible In IDH
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
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
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then User gets the value of "LOB" and "OEM" and "Description"
	Then Verify that the following radio button fields are displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify that the following text box fields are displayed
	| FieldName |
	| Alias     |
	| Test      |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	When User clicks on "Product" Taxonomy
	Then User enters "Product" Taxonomy details and clicks on "Save" button if empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Prod Mgt" and Type as "is Quote Eligible"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User verifies that the grid displays the added characteristic with From Level "E"
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify Characteristic type "Boolean" value should be updated with in the "Pim" database and "Defaults" container in "Service Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then User verifies that the Characteristic value is getting updated under "Primary" service attribute in "Pim" database and "ItemPools" container
	Then Verify below documents are created in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| isQuoteEligible      | True           |