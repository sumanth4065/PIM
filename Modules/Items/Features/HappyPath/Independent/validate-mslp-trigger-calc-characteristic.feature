Feature: Validate MSLP in Trigger Calc Characteristic

@UI @Module-Items @TCID-121599
Scenario: TC-121599 Add MSLP in Trigger Calc
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
	Then User selects characteristic Kind as "List Price Parameters" and Type as "MSLP in Trigger Calc"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName  |
	| Undo        |
	| Save        |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User verifies that the grid displays the added characteristic with From Level "B"
	Then Verify created and modified username and date in Characteristic display Panel
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the characteristic value in "Item Default" document in Pim DB
	| Characteristic Type  | Expected Value |
	| MSLP in Trigger Calc | True           |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify the characteristic field "Boolean" in Cosmos DB using "Pim" database and "ItemPools" container in "ItemPool" document
	Then Verify characteristic value in Item Catalog document Service node in IDH
	| Characteristic Value | Expected Value |
	| isMslpInTriggerCalc  | True           |