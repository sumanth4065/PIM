Feature: AlternateImportAlias

@UI @Alternates @Sanity @UserStory42743 @Automatable @ItemMaster @Phase2_Sprint15 @Module-Alternates
Scenario: Alternate_109908_AliasImportCreateSingleItemClusterAddingSkuToIDH
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "ExplicitItemCreate" Excel file for the "Explicit-Item" tab
	Then User verifies valid data is present in the "ExplicitItemCreate" Excel file 
	And User uploads the "ExplicitItemCreate" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the following fields from "ExplicitItemCreate" Excel for Explicit Item
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| OEM         |
	| LOB         |
	| Description |
	Then User Loads "ExplicitItemCreate" excel with row index 1 as "Item1"
	Then Verify document is created for "Item1" in Cosmos DB using "Pim" database and "ItemPools" container 
	Then Verify below documents are created for "Item1" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateAliasCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateAliasCluster" Excel file 
	And User uploads the "CreateAliasCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then User Loads data from "CreateAliasCluster" excel with row index 1 as "lookupItem" and "clusterItem"
	Then Verify AliasSku is added to "Item1" in IDH Item document with the following details
    | IsPrimary | IsValid | Sku        |
    | True      | True    | lookupItem |
@NoNewBrowser @Alternates @Sanity @UserStory42743 @Automatable @ItemMaster @Phase2_Sprint15 @Module-Alternates
Scenario: Alternate_109909_AliasImportAddItemToClusterAddingSkuToIDH
	Given trigger wait is not required
	Then User Loads data from "CreateAliasCluster" excel with row index 2 as "Item1" and "Item2"
	Then Verify AliasSku is added to "Item1" in IDH Item document with the following details
    | IsPrimary | IsValid | Sku   |
    | False     | True    | Item2 |
	

	


	




	