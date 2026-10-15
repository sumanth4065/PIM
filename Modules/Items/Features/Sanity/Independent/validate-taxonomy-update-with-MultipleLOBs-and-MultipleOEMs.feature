Feature: Validate Taxonomy Update Event For MultipleOEMs and MultipleLOBs
	
@UI @Module-Items @TCID_151255
Scenario: TC_151255 Validate Taxonomy Update Event Creation For MultipleOEMs and MultipleLOBs
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

	# Create Item with OEM1 and LOB2
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

	# Create Item with OEM2 and LOB1
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
	When User clicks on "New" button in Item Master
	Then Verify Item Master "Explicit" tab is displayed

	# Create Item with OEM2 and LOB2
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

	# Update Product Taxonomy details for Item with OEM1 and LOB1
	Then User recalls the created "Item" Id
	Then User searches and selects "Oem1Edit" from dropdown
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
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes

	# Update Product Taxonomy details for Item with OEM2 and LOB1
	Then User recalls the created "Item" Id
	Then User searches and selects "Oem2Edit" from dropdown
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
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem1"
	| EventHistory        |
	| ItemUpdated         |
	| ItemCatalogUpdated  |
	| ItemTaxonomyChanged |
	Then Verify the characteristic value in ItemPools document in Pim database for "Oem2"
	| EventHistory        |
	| ItemUpdated         |
	| ItemCatalogUpdated  |
	| ItemTaxonomyChanged |
