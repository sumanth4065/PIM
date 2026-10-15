Feature: Validate Import Unidirectional Cluster Status Management

@UI @Alternates @Sanity @UserStory93961 @Automatable @AlternateParts @AlternatesImport @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_9_1_Alternate_102972_ImportUnidirectionalCreateSingleItemClusterAlternatesPartPool
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
	Then User Loads "CreateExplicitItem" excel with row index 7 as "Item6"
	Then Verify document is created for "Item6" in Cosmos DB using "Pim" database and "ItemPools" container 
	Then Verify below documents are created for "Item6" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |


	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateActiveCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateActiveCluster" Excel file 
	And User uploads the "CreateActiveCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen



	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User selects the "Unidirectional" tab
	Then Verify the "Unidirectional" tab is displayed
	When User recalls the Lookup Item from "CreateActiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateActiveCluster" Excel with row index 1
    | FieldNames | Values          |
    | Item-Id    | Lookup Item     |
    | IsActive   | true,Active     |
    | IsMaster   | true,Set Master |

@NoNewBrowser @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternateParts @AlternatesImport @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_9_2_Alternate_102973_ImportUnidirectionalCreateNewClusterAddItemToClusterAlternatesPartPool
	When User refreshes the page
	When User selects the "Unidirectional" tab
	Then Verify the "Unidirectional" tab is displayed
	When User recalls the Lookup Item from "CreateActiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 2
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateActiveCluster" Excel with row index 2
    | FieldNames     | Values           |
    | Item-Id        | Lookup Item,Item |
    | IsActive       | true,Active      |
    | IsMaster       | true,Set Master  |
	Then User Loads data from "CreateActiveCluster" excel with row index 2 as "Item2" and "Item3"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item3"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item2 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	Then Verify below documents are created for "Item3" in "Integration-Data-Hub" database and "Items" container
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item2 | true                 | true      |

#	#Note: User should able to view alternateItem.added event is getting created in Database for Alternate Part pool document of item 2 

@NoNewBrowser @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternateParts @AlternatesImport @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_9_3_Alternate_102974_ImportUnidirectionalMarkActiveClusterItemAsInactiveAlternatesPartPool
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
	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User selects the "Unidirectional" tab
	Then Verify the "Unidirectional" tab is displayed
	When User recalls the Lookup Item from "CreateInactiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	When User selects Display Inactive toggle button which is in "off" state
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateInactiveCluster" Excel with row index 1
    | FieldNames | Values           |
    | Item-Id    | Lookup Item,Item |
    | IsActive   | true,Active      |
    | IsMaster   | true,Set Master  |
	Then User Loads data from "CreateInactiveCluster" excel with row index 1 as "Item4" and "Item5"
	#Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item5"
    | EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateItemDeactivated | Item4 | Unidirectional   | true     | false       | false    | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item5" in "Integration-Data-Hub"
	| AlternateItem       | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems | Item4 | false                | true      |



@NoNewBrowser @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternateParts @AlternatesImport @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_9_4_Alternate_102976_ImportUnidirectionalMarkInactiveClusterItemAsActiveAlternatesPartPool
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
	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User selects the "Unidirectional" tab
	Then Verify the "Unidirectional" tab is displayed
	When User recalls the Lookup Item from "ReactivateCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "ReactivateCluster" Excel with row index 1
    | FieldNames     | Values           |
    | Item-Id        | Lookup Item,Item |
    | IsActive       | true,Active      |
    | IsMaster       | true,Set Master  |
	Then User Loads data from "ReactivateCluster" excel with row index 1 as "Item6" and "Item7"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item7"
    | EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateItemReactivated | Item6 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item7" in "Integration-Data-Hub"
	| AlternateItem       | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems | Item6 | true                 | true      |
