Feature: Alternate_111424_ValidateSelfReferencingInterchangebleClusterByImportExplicitItem

@UI @Alternates @Sanity @UserStory42743 @Automatable @ItemMaster @Phase2_Sprint15 @Module-Alternates
Scenario: Alternate_111424_ValidateSelfReferencingInterchangebleClusterByImportExplicitItem
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Explicit" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateExplicitItem" Excel file for the "Explicit-Item" tab
	Then User verifies valid data is present in the "CreateExplicitItem" Excel file 
	And User uploads the "CreateExplicitItem" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	Then User recalls the following fields from "CreateExplicitItem" Excel for Explicit Item
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| OEM         |
	| LOB         |
	| Description |
	Then User Loads "CreateExplicitItem" excel with row index 1 as "Item1"
	Then Verify document is created for "Item1" in Cosmos DB using "Pim" database and "ItemPools" container
	Then Verify below documents are created for "Item1" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |