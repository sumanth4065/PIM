Feature: AlternateEventHistoryInterchangeable

@UI @Alternates @Sanity @UserStory91437 @UserStory94168 @Automatable @Parts @Pool @Functional @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_6_1_Alternate_93983_AlternateCreatedEventInterchangeable
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
	Then User saves the item details as "Item2"

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
	Then User saves the item details as "Item3"

	Then Verify document is created for "Item3" in Cosmos DB using "Pim" database and "ItemPools" container 
	Then Verify below documents are created for "Item1" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify below documents are created for "Item2" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	Then Verify below documents are created for "Item3" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |

	When User selects the "Alternate" in Hamburger menu
	Then Verify the "Interchangeable" tab is displayed
	When User enters "Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the below buttons are enabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName         |
	| Edit-Cluster-Notes |
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Create-Single-Item-Cluster |
	| Save                       |
	| Undo                       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
@NoNewBrowser @Alternates @Sanity @UserStory91437 @UserStory94168 @Automatable @Parts @Pool @Functional @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_6_2_Alternate_93984_AlternateItemAddedEventInterchangeable
	When User clicks on the "Add-Another-Item-To-Cluster" button from the "Cluster-Mode-Cluster-Lookup" panel 
	Then Verify the below text fields are empty in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	When User enters "Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the "Record is already Active in another cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects the "Item2" option
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item2" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Interchangeable  | true     | false       | true     | false                         | false       | false            |
	| AlternateItemAdded | Item2 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemAdded | Item2 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | false     |
@NoNewBrowser @Alternates @Sanity @UserStory91437 @UserStory94168 @Automatable @Parts @Pool @Functional @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_6_3_Alternate_93985_AlternateItemPrimaryStatusUpdatedEventInterchangeable
	When User clicks on the "set-as-master" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Changing Cluster Master" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item2" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item2"
	| EventName                         | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated                  | Item1 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	| AlternateItemPrimaryStatusUpdated | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName                         | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated                  | Item1 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	| AlternateItemPrimaryStatusUpdated | Item2 | Interchangeable  | true     | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
@NoNewBrowser @Alternates @Sanity @UserStory91437 @UserStory94168 @Automatable @Parts @Pool @Functional @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_6_4_Alternate_93986_AlternateItemDeactivatedEventInterchangeable
	When User selects "Item1" from "Cluster-Mode" interchange master table
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Inactivate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Deactivation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then User verifies "Item1" is not visible on "Cluster_Mode" interchange master table
	When User selects Display Inactive toggle button which is in "off" state
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | false       |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item2"
	| EventName                         | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemPrimaryStatusUpdated | Item1 | Interchangeable  | false    | false       | false    | false                         | false       | false            |
	| AlternateItemDeactivated          | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName                         | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemDeactivated          | Item1 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	| AlternateItemPrimaryStatusUpdated | Item2 | Interchangeable  | true     | false       | false    | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | false                | true      |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | false                | false     |
	| interchangeableItems | Item2 | true                 | true      |

@NoNewBrowser @Alternates @Sanity @UserStory91437 @UserStory94168 @Automatable @Parts @Pool @Functional @Phase2_Sprint14 @Module-Alternates
Scenario: Scenario_6_5_Alternate_110458_AlternateItemReactivatedEventInterchangeable
	When User selects "Item1" from "Cluster-Mode" interchange master table
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemReactivated | Item1 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	| AlternateItemReactivated | Item2 | Interchangeable  | true     | false       | true     | false                         | false       | false            |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item2"
	| EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemReactivated | Item1 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	| AlternateItemReactivated | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	 Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |


@NoNewBrowser @Alternates @Sanity @Flatten @Automatable @Parts @Pool @Functional @Phase2_Sprint15 @Module-Alternates
Scenario: Scenario_6_6_Alternate_111492_AddingAliasToInterchangeableClusterAlternatesPartPool
	When User selects the "Alias" tab
	Then Verify the "Alias" tab is displayed
	Then Verify the below text fields are empty in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then Verify the below buttons are disabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	When User enters "Item2" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Create-Single-Item-Cluster |
	When User clicks on the "Create-Single-Item-Cluster" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Create New Cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below field values for "Item2" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType   | IsActive | IsMaster |
	| Alias Cluster | True     | True     |
	When User clicks on the "Add-Another-Item-To-Cluster" button from the "Cluster-Mode-Cluster-Lookup" panel 
	Then Verify the below text fields are empty in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Inactivate-Relationship    |
	| Set-As-Master              |
	| Edit-Item-To-Cluster-Notes |
	| View-Cluster-Relationships |
	| Undo                       |
	| Save                       |
	When User enters "Item3" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the details of below fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName      | Value |
	| Total-Active   | 0     |
	| Total-Clusters | 1     |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item3" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item1"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	| AlternateCreated | Item2 | Interchangeable  | true     | false       | true     | false                         | false       | false            |
	| AlternateCreated | Item3 | Interchangeable  | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item2"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | false    | false       | true     | false                         | false       | false            |
	| AlternateCreated | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateCreated | Item3 | Interchangeable  | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Item3"
	| EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | false    | false       | true     | false                         | true        | false            |
	| AlternateCreated | Item2 | Interchangeable  | true     | false       | true     | false                         | true        | false            |
	| AlternateCreated | Item3 | Interchangeable  | false    | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
	| interchangeableItems | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
	| interchangeableItems | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item3" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | false     |
	| interchangeableItems | Item2 | true                 | true      |
	| interchangeableItems | Item3 | true                 | false     |
