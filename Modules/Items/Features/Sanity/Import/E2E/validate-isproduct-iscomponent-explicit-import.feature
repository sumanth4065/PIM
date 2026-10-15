 Feature: Validate isproduct iscomponent in Explicit item creation from Import

@UI @Module-Items @TCID-150940
Scenario: TC-150940 Create Explicit Item Validate Product and Component Flags
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies valid data is present in the "CreateItem" Excel file 
	And User uploads the "CreateItem" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User is able to view the Import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User retrieves the following field values from the "CreateItem" Excel file
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| OEM         |
	| Division    |
	| Description |
	Then Verify that the following radio button fields are displayed
	| FieldName |
	| Product   |
	| Component |
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
	Then Validate that the checkbox states on the UI match the corresponding values from the imported Excel file
	| FieldName |
	| Product   |
	| Component |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the following general flags in the "Item Default" document from "Pim" database and "Defaults" container
	| FieldName |
	| Product   |
	| Component |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory       |
	| ItemCreated        |
	| ItemCatalogCreated |
	Then Verify the following general flags in the ItemPool document from Pim database
	| FieldName |
	| Product   |
	| Component |
	Then Verify the following general flags in the Integration in Item document
	| FieldName   | ExpectedValue |
	| isProduct   | true          |
	| isComponent | false         |
	Then Verify the following general flags in the Integration in ItemCatalog document
	| FieldName   | ExpectedValue |
	| isProduct   | true          |
	| isComponent | false         |


@NoNewBrowser @Module-Items @TCID-150941
Scenario: TC-150941 Edit Create Item Validate Product and Component Flags
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies valid data is present in the "EditItem" Excel file 
	And User uploads the "EditItem" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User is able to view the Import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User retrieves the following field values from the "EditItem" Excel file
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| OEM         |
	| Division    |
	| Description |
	Then Verify that the following radio button fields are displayed
	| FieldName |
	| Product   |
	| Component |
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
	Then Validate that the checkbox states on the UI match the corresponding values from the imported Excel file
	| FieldName |
	| Product   |
	| Component |
	Then Verify the below service field values in Cosmos DB using "Pim" database and "Defaults" container in "Service Default" document
	| FieldNames     |
	| Item           |
	| Oem            |
	| Description    |
	| Internal-Notes |
	| Status         |
	Then Verify the following general flags in the "Item Default" document from "Pim" database and "Defaults" container
	| FieldName |
	| Product   |
	| Component |
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory       |
	| ItemUpdated        |
	| ItemCatalogUpdated |
	Then Verify the following general flags in the ItemPool document from Pim database
	| FieldName |
	| Product   |
	| Component |
	Then Verify the following general flags in the Integration in Item document
	| FieldName   | ExpectedValue |
	| isProduct   | false         |
	| isComponent | true          |
	Then Verify the following general flags in the Integration in ItemCatalog document
	| FieldName   | ExpectedValue |
	| isProduct   | false         |
	| isComponent | true          |
