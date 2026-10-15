Feature: Validate Item Creation with Multiple OEMs and Multiple LOBs
	
@UI @Module-Items @TCID-151253
Scenario: TC_151253 Validate Item Creation with Multiple OEMs and Multiple LOBs
	# Create Item with OEM1 and LOB1
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	When User clicks on "New" button in Item Master
	Then Verify Item Master "Explicit" tab is displayed
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	Then User searches and selects "Oem1" from dropdown
	Then User searches and selects "LOB1" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

	#Create item with OEM1 and LOB2
	When User clicks on "New" button in Item Master
	Then User recalls the created "Item-Id" Id
	Then User searches and selects "Oem1" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| Oem         |
	| Division    |
	| Description |
	Then User searches and selects "LOB2" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

	# Add Product Taxonomy details for Item with OEM1 and LOB1
	Then User recalls the created "Item" Id
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
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
	| Fields |
	| Family |
	| Line   |
	| Group  |

	#Create Item with OEM2 and LOB1
	When User clicks on "New" button in Item Master
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the created "Item-Id" Id
	Then User searches and selects "Oem2" from dropdown
	Then User searches and selects "LOB1" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

	#Create Item with OEM2 and LOB2
	When User clicks on "New" button in Item Master
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the created "Item-Id" Id
	Then User searches and selects "Oem2" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| Oem         |
	| Division    |
	| Description |
	Then User searches and selects "LOB2" from dropdown
	Then User enters data for the following header fields
	| FieldName   |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes

	#Add Product Taxonomy details for Item with OEM2 and LOB2
	Then User recalls the created "Item" Id
	Then User searches and selects "Oem2" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
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
	| Fields |
	| Family |
	| Line   |
	| Group  |
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem1"
	| EventHistory       |
	| ItemCreated        |
	| ItemCatalogCreated |
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem2"
	| EventHistory       |
	| ItemCreated        |
	| ItemCatalogCreated |
@NoNewBrowser @Module-Items @TCID-151254
Scenario: TC_151254 Validate Characteristic Addition for Item with Multiple OEMs and Multiple LOBs
	#Add Characteristic value for Item with OEM1 and LOB1 and validate PIM and Item Pool
	Then User recalls the created "Item" Id
	Then User searches and selects "Oem1" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
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
	Then User selects characteristic Kind as "Billing" and Type as "Billing Type"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox       |
	| scope-obsolete |
	| Active         |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkbox         |
	| service-specific |
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify the below characteristic checkboxes are checked
	| Checkbox         |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkbox       |
	| scope-obsolete |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User verifies that the grid displays the added characteristic with From Level "E"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Edit       |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Add-Level  |
	| Undo       |
	| Save       |
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
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem1"
	| EventHistory       |
	| ItemUpdated        |
	| ItemCatalogUpdated |

	# Add Characteristic value for Item with OEM2 and LOB1 and validate PIM and Item Pool
	Then User recalls the created "Item" Id
	Then User searches and selects "Oem2" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then User searches and selects "LOB1" from dropdown
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
	Then User selects characteristic Kind as "Billing" and Type as "Billing Type"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName         |
	| Add-Characteristic |
	| Add-Level          |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	When User clicks on "Add-Level" button in Characteristic display Panel
	Then Verify below checkboxes are enabled in Characteristic display Panel
	| Checkbox       |
	| scope-obsolete |
	| Active         |
	Then Verify below checkboxes are disabled in Characteristic display Panel
	| Checkbox         |
	| service-specific |
	Then Verify below characteristic field values are auto-populated and not editable
	| FieldName        |
	| Type             |
	| Type-Rank        |
	| Type-Description |
	| Scope            |
	| Rank-Memo        |
	Then Verify the below characteristic checkboxes are checked
	| Checkbox         |
	| service-specific |
	| Active           |
	Then Verify the below characteristic checkboxes are unchecked
	| Checkbox       |
	| scope-obsolete |
	Then User enters data into the characteristic "Value" field
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify below fields are editable
	| FieldName      |
	| Internal-Notes |
	Then Verify below fields are not editable
	| FieldName      |
	| Type-Notes     |
	When User clicks on "Save" button in Characteristic display Panel
	Then User confirms the Save changes
	Then User verifies that the grid displays the added characteristic with From Level "E"
	Then Verify below buttons are enabled in Characteristic display Panel
	| ButtonName |
	| Edit       |
	Then Verify below buttons are disabled in Characteristic display Panel
	| ButtonName |
	| Add-Level  |
	| Undo       |
	| Save       |
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
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem2"
	| EventHistory       |
	| ItemUpdated        |
	| ItemCatalogUpdated |