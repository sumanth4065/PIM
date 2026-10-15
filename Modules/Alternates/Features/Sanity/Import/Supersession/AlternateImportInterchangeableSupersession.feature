Feature: AlternateImportInterchangeableSupersession

@UI @Alternates @Phase2_Sprint15 @Module-Alternates @TCID-111558
Scenario: Scenario_13_1_Alternate_111558_ImportInterchangeableSupersessionCreateRelationship
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
	When User selects the "Alternate" in Hamburger menu 
	Then Verify the "Interchangeable" tab is displayed
	When User recalls the Lookup Item from "CreateCluster" Excel for Alternate in the "Cluster-Mode-Cluster-Lookup" panel with row index 1
	| FieldName |
	| Item-Id   |
	Then Validate cluster item rows in "Cluster-Mode" panel from "CreateCluster" Excel with row index 1
    | FieldNames     | Values           |
    | Item-Id        | Lookup Item,Item |
    | IsActive       | true,Active      |
    | IsMaster       | true,Set Master  |
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
	When User selects the "Supersession-Mode" tab
	Then Verify the "Supersession-Interchangeable" tab is displayed
	Then User Loads data from "CreateActiveCluster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType             | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item1 | Interchangeable_Supersession | true     | false       | true     | true                          | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                    | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableSupersessionItems | Item1 | true                 | true      |


@NoNewBrowser @Sanity @Phase2_Sprint15 @Module-Alternates @TCID-111561
Scenario: Scenario_13_2_Alternate_111561_ImportInterchangeableSupersessionDeactivateTheRelationships
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
	When User selects the "Supersession-Mode" tab
	Then Verify the "Supersession-Interchangeable" tab is displayed
	Then User Loads data from "CreateInactiveCluster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName                | Item  | RelationshipType             | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemDeactivated | Item1 | Interchangeable_Supersession | true     | false       | false    | true                          | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                    | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableSupersessionItems | Item1 | false                | true      |


@NoNewBrowser @UserStory92876 @Phase2_Sprint15 @Module-Alternates @TCID-111598
Scenario: Scenario_13_3_Alternate_111598_ImportInterchangeableSupersessionReactivateTheRelationships
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
	When User selects the "Supersession-Mode" tab
	Then Verify the "Supersession-Interchangeable" tab is displayed
	Then User Loads data from "ReactivateCluster" excel with row index 1 as "Item1" and "Item2"
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName                | Item  | RelationshipType             | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemReactivated | Item1 | Interchangeable_Supersession | true     | false       | true     | true                          | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem                    | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableSupersessionItems | Item1 | true                 | true      |
