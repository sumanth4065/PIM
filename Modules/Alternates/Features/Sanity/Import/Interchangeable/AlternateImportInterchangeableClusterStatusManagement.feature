Feature: AlternateImportInterchangeableClusterStatusManagement

@UI @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternatesImport @AlternateParts @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_8_1_Alternate_102962_ImportInterchangeableAddNewItemToClusterAlternatesPartPool
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
	Then User Loads "CreateExplicitItem" excel with row index 6 as "Item6"
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
	When User recalls the Lookup Item from "CreateActiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateActiveCluster" Excel with row index 1
    | FieldNames     | Values           |
    | Item-Id        | Lookup Item,Item |
    | IsActive       | true,Active      |
    | IsMaster       | true,Set Master  |
	Then User Loads data from "CreateActiveCluster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
    | AlternateItemAdded | Item2 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub"
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | false     |

@NoNewBrowser @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternateParts @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_8_2_Alternate_102963_ImportInterchangeableMarkActiveClusterItemAsInactiveAlternatesPartPool
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
	When User recalls the Lookup Item from "CreateInactiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	When User selects Display Inactive toggle button which is in "off" state
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateInactiveCluster" Excel with row index 1
    | FieldNames | Values           |
    | Item-Id    | Lookup Item,Item |
    | IsActive   | true,Active      |
    | IsMaster   | true,Set Master  |
	Then User Loads data from "CreateInactiveCluster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
    | EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateCreated         | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
    | AlternateItemDeactivated | Item2 | Interchangeable  | false    | false       | false    | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub"
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| interchangeableItems | Item2 | false                | false     |


@NoNewBrowser @Alternates @Sanity @UserStory93961 @UserStory127540 @Automatable @AlternatesImport @AlternateParts @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_8_3_Alternate_102965_ImportInterchangeableMarkInactiveClusterItemAsActiveAlternatesPartPool
	When User refreshes the page
	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User recalls the Lookup Item from "CreateActiveCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 2
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateActiveCluster" Excel with row index 2
    | FieldNames | Values           |
    | Item-Id    | Lookup Item,Item |
    | IsActive   | true,Active      |
    | IsMaster   | true,Set Master  |
	Then User Loads data from "CreateActiveCluster" excel with row index 2 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
    | AlternateItemAdded | Item2 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub"
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | false     |


@NoNewBrowser @Alternates @Sanity @UserStory39212 @Automatable @ItemMaster @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_8_4_Alternate_102968_ImportInterchangeableSetMasterChangeAlternatesPartPool
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "SetAsMaster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "SetAsMaster" Excel file 
	And User uploads the "SetAsMaster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User recalls the Lookup Item from "SetAsMaster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "SetAsMaster" Excel with row index 1
    | FieldNames | Values           |
    | Item-Id    | Lookup Item,Item |
    | IsActive   | true,Active      |
    | IsMaster   | false,Set Master |
	Then User Loads data from "SetAsMaster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
    | EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
    | AlternateCreated   | Item1 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
    | AlternateItemAdded | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub"
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |

