Feature: BOM Line Replacement with Quantity Aggregation

@UI @Module-Bom @TCID-141810
Scenario: TC-141810 Asset Product BOM Line Replace Part BOM Aggregate Quantities
	Given User login to the PIM application using the ppt username and password
	When User selects the "BOM" in Hamburger menu
	Then Verify "Asset" tab is displayed
	Then Verify "Asset" tab "Bom-Kind" field is enabled
	And Verify "Asset" tab "New" button is enabled
	When User clicks on "Asset" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Asset" tab
	| FieldName      |
	| Asset-BOM-Kind |
	| Serial-No      |
	Then User captures the entered values in the "Asset" tab
	| FieldValue |
	| BOM-Kind   |
	| Serial-No  |
	When User clicks on "Asset" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User gets the values of below fields from "Asset" tab
	| Fieldvalue |
	| BOM-Kind   |
	| Serial-No  |
	| Oem        |
	| Family     |
	| Line       |
	| Group      |
	| Sku        |
	Then Verify created and modified username and date in right corner of BOM header panel in "Asset" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then User retrieves the PartitionKey for Asset SerialNumber from Cosmos DB using "Pim" database and "BOMs" container
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku1" from dropdown
	Then User verifies the BOM Line "Quantity" defaults to "1"
	Then User gets the values of below fields from BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Validate BOM line grid row "0" data matches BOM line creation panel
	| FieldValue  |
	| Sku         |
	| Oem         |
	| Description |
	| Quantity    |
	| Obsolete    |
	Then Verify BOM UI data in "Asset" tab with Cosmos DB using "Pim" database and "BOMs" container using "Asset-BOM-Kind" reference
	| FieldName |
	| Oem       |
	| Family    |
	| Line      |
	| Group     |
	Then Verify "UI" BOM line grid row "0" and Cosmos DB using "Pim" database and "BOMs" container for "Asset" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Quantity  |
	| Obsolete  |
	Then User get the value of BOM Line grid row "0" data from Asset tab
	| FieldValue |
	| Sku        |
	| Oem        |
	When User selects "Part" Tab  
	Then Verify "Part" tab "Bom-Kind" field is enabled
	Then Verify "Part" tab "New" button is enabled
	When User clicks on "Part" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Part" tab
	| FieldName     |
	| Part-BOM-Kind | 
	Then User searches and selects data for the below fields from dropdown in "Part" tab using Asset BOM Line data
	| FieldName |
	| Oem       |
	| Sku       |
	Then Verify "Part" tab "Create" button is enabled
	Then Verify "Part" tab "Cancel" button is enabled
	When User clicks on "Part" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User gets the values of below fields from "Part" tab
	| FieldValue |
	| Oem        |
	| Sku        |
	Then Verify created and modified username and date in right corner of BOM header panel in "Part" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku3" from dropdown
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User gets the "Quantity" from the BOM line grid row "0" in the Part tab
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container 
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	When User selects "Asset" Tab  
	Then User recalls below field values in "Asset" tab
	| FieldName |
	| BOM-Kind  |
	| Serial-No |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku2" from dropdown
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then Verify created and modified username and date of BOM Line "1"
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	Then User get the value of BOM Line grid row "1" data from Asset tab
	| FieldValue |
	| Sku        |
	| Oem        |
	When User selects "Part" Tab  
	Then Verify "Part" tab "Bom-Kind" field is enabled
	Then Verify "Part" tab "New" button is enabled
	When User clicks on "Part" tab "New" button in BOM header panel 
	Then User searches and selects data for the below fields from dropdown in "Part" tab
	| FieldName     |
	| Part-BOM-Kind | 
	Then User searches and selects data for the below fields from dropdown in "Part" tab using Asset BOM Line data
	| FieldName |
	| Oem       |
	| Sku       |
	Then Verify "Part" tab "Create" button is enabled
	Then Verify "Part" tab "Cancel" button is enabled
	When User clicks on "Part" tab "Create" button in BOM header panel
	Then User verifies success message for BOM creation
	Then User gets the values of below fields from "Part" tab
	| FieldValue |
	| Oem        |
	| Sku        |
	Then Verify created and modified username and date in right corner of BOM header panel in "Part" tab
	| FieldName     |
	| Created-By    |
	| Last-Modified |
	When User clicks on "New" button in BOM Line creation panel
	Then User searches and selects the "Sku3" from dropdown		
	Then User clicks and enters BOM Line "Quantity1" field
	When User clicks on "Save" button in BOM Line creation panel
	Then User verifies success message for BOM Line addition
	Then User gets the "Quantity" from the BOM line grid row "0" in the Part tab
	Then Verify BOM UI data in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container using "Part-BOM-Kind" reference
	| FieldName |
	| Oem       |
	Then Verify BOM Header "Sku" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container 
	Then Verify BOM Header "Internal-Notes" in "Part" tab with Cosmos DB using "Pim" database and "BOMs" container
	Then Verify Event History should be updated with in the "BestBOMPools" container of the "Pim" database for "Part" tab
	| EventHistory         |
	| PlanningBOMCreated   |
	Then Verify Part BOMLine grid row "0" displayed in "BestBOMPools" container of the "Pim" database for "Part" tab
	| FieldName |
	| Sku       |
	| Oem       |
	| Obsolete  |
	Then User verifies the Aggregated BOMLine grid row "0" quantity for replaced part BOMs in "Pim" database and "BestBOMPools" container
