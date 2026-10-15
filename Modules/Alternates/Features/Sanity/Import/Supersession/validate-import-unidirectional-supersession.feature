Feature: Validate Import Unidirectional Supersession

@UI @Alternates @Sanity @UserStory92876 @Automatable @AlternatesImport @AlternatesFlatten @Alternates_E2E @Phase2_Sprint15 @Module-Alternates
Scenario: Scenario_14_1_TC_111560_Import Unidirectional Supersession Create Relationship
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
	Then User Loads "CreateExplicitItem" excel with row index 4 as "Item4"
	Then Verify document is created for "Item4" in Cosmos DB using "Pim" database and "ItemPools" container 
	Then Verify below documents are created for "Item4" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateCluster" Excel file 
	And User uploads the "CreateCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	
	Then User Loads data from "CreateCluster" excel with row index 3 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item1 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	Then Verify below documents are created for "Item2" in "Integration-Data-Hub" database and "Items" container
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | true                 | true      |
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateActiveCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateActiveCluster" Excel file 
	And User uploads the "CreateActiveCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	
	Then User Loads data from "CreateActiveCluster" excel with row index 1 as "Item1" and "Item2"
	#Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType            | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item1 | Unidirectional_Supersession | true     | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                   | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalSupersessionItems | Item1 | true                 | true      |


@NoNewBrowser @Sanity @UserStory92876 @Alternates_import @AlternatesFlatten @Phase2_Sprint15 @Module-Alternates
Scenario: Scenario_14_2_TC_111559_Import Unidirectional Supersession Deactivate The Relationships
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateInactiveCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateInactiveCluster" Excel file 
	And User uploads the "CreateInactiveCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	
	Then User Loads data from "CreateInactiveCluster" excel with row index 1 as "Item1" and "Item2"
	#Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName                | Item  | RelationshipType            | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemDeactivated | Item1 | Unidirectional_Supersession | true     | false       | false    | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                   | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalSupersessionItems | Item1 | false                | true      |


@NoNewBrowser @UserStory92876 @Sanity @Alternates_E2E @AlternatesFlatten @Phase2_Sprint15 @Module-Alternates
Scenario: Scenario_14_3_TC_111597_Import Unidirectional Supersession Reactivate The Relationships
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "ReactivateCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "ReactivateCluster" Excel file 
	And User uploads the "ReactivateCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	
	Then User Loads data from "ReactivateCluster" excel with row index 1 as "Item1" and "Item2"
	#Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName                | Item  | RelationshipType            | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemReactivated | Item1 | Unidirectional_Supersession | true     | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                   | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalSupersessionItems | Item1 | true                 | true      |

