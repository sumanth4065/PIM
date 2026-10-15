Feature: Alternate Import Unidirectional Alias

@UI @Module-Alternates @TC-120107
Scenario: TC_120107 Import Create Single Item Cluster Unidirectional Alias
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
	Then User Loads "ExplicitItemCreate" excel with row index 2 as "Item2"
	Then User Loads "ExplicitItemCreate" excel with row index 3 as "Item3"
	Then Verify document is created for "Item3" in Cosmos DB using "Pim" database and "ItemPools" container
	Then Verify below documents are created for "Item3" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button
	And User selects import type as "Alternate-Part-Data" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateUnidirectionalCluster" Excel file for the "Alternate" tab
	Then User verifies valid data is present in the "CreateUnidirectionalCluster" Excel file 
	And User uploads the "CreateUnidirectionalCluster" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item1 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	| AlternateItemAdded | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | true      |
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
	Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateCreated   | Item1 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	| AlternateItemAdded | Item3 | Unidirectional   | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemAdded | Item3 | Unidirectional   | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item3"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Unidirectional   | true     | false       | true     | false                         | true        | false            |
	| AlternateItemAdded | Item3 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item2 | true                 | true      |
	| unidirectionalItems  | Item1 | true                 | true      |
	| unidirectionalItems  | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| unidirectionalItems  | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item3" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | true                 | true      |
	| interchangeableItems | Item3 | true                 | true      |




	

	


	




	