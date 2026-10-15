Feature: Alternate_107465_ValidateSelfReferencingInterchangebleSingleItemClusterExplicit item

@UI @Alternates @Sanity @UserStory107072 @Automatable @Parts @Pool @Functional @Phase2_Sprint14 @Module-Alternates
Scenario: Alternate_107465_ValidateSelfReferencingInterchangebleSingleItemClusterExplicit item
	Given User login to the PIM application using the ppt username and password
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| LOB         |
	| Description |
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User saves the item details as "Item1"
	Then Verify the below field values for "Item1" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | IsMaster |
	| Interchangeable Cluster | True     | True     |
	Then Verify document is created for "Item1" in Cosmos DB using "Pim" database and "ItemPools" container 
	Then Verify below documents are created for "Item1" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
