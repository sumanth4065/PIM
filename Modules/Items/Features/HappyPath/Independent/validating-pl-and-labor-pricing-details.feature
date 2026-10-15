Feature: Validating P&L and Labor pricing details

@UI @Module-Items @TCID-121734
Scenario: TC-121734 Validating P&L and Labor pricing details	
    Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
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
	
	#Baseline and product taxonomy create
	When User clicks on the "Baseline" tab
	Then User recalls the created "Item" Id
	Then Verify that the following characteristic display boxes are displayed
	| PanelName                        |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then User selects characteristic Kind as "List Price Parameters" and Type as "MSLP in Trigger Calc"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Boolean" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User selects characteristic Kind as "List Price Parameters" and Type as "MSLP Percent in Trigger Calc"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Number" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User selects characteristic Kind as "List Price Amounts" and Type as "List Price"
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then User enters data into the characteristic "Number" field
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User selects characteristic Kind as "List Price Parameters" and Type as "MSLP in Trigger Calc"
	Then User selects characteristic Kind as "List Price Amounts" and Type as "List Price"
	Then User calculates P&L values based on the below values
    | List Price | MSLP Percent in Trigger Calc |
    | 20         | 20                           |
	Then User verifies P&L and Labor values with calculated values
	Then Verify the characteristic value in "Item Default" document in Pim DB
    | Characteristic Type          | Expected Value |
    | MSLP in Trigger Calc         | True           |
    | MSLP Percent in Trigger Calc | 20             |
    | List Price                   | 20             |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory           |
	| ItemCreated            |
	| ItemCatalogCreated     |
	Then Verify P&L and Labor values in ItemPools document in Pim database
		

