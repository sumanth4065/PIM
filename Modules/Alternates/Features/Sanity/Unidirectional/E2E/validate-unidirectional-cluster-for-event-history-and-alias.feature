Feature: Validate Unidirectional cluster for Event History and Alias

@UI @TC-93987 @Module-Alternates @Order1
Scenario: 01 TC_93987 validate alternate created Event in AlternatePartPools
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
	Then User selects "Active" from the "Status" dropdown
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
	Then User selects "Active" from the "Status" dropdown
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
	Then User selects "Active" from the "Status" dropdown
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
	When User selects "Unidirectional" tab from cluster mode
	Then Verify the "Unidirectional" tab is displayed
	Then Verify the below text fields are empty in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then Verify the below buttons are disabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	When User enters "Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 0     |
	| Total-Clusters | 0     |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName             |
	| Select-Another-Cluster |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Create-Single-Item-Cluster |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Inactivate-Relationship    |
	| Set-As-Master              |
	| Edit-Item-To-Cluster-Notes |
	| Save                       |
	| Undo                       |
	When User clicks on the "Create-Single-Item-Cluster" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Create New Cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the Display InActive toggle switch is "off"
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify created and modified date and username for "Cluster-Mode-Selected-Cluster-Line" panel in "Item-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	When User from the "Cluster-Mode-Selected-Cluster-Line" panel selects the "Selected-Item" tab
	Then Verify created and modified date and username for "Cluster-Mode-Selected-Cluster-Line" panel in "Selected-Item" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName        | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
@NoNewBrowser @Module-Alternates @TC-93988 @Order2
Scenario:02 TC_93988 validate alternateItem.added Event in AlternatePartPools
	When User clicks on the "Add-Another-Item-To-Cluster" button from the "Cluster-Mode-Cluster-Lookup" panel 
	Then Verify the below text fields are empty in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	When User enters "Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item2" in "Cluster-Mode" interchange master table
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Item1 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	| AlternateItemAdded | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | true                 | true      |
	| interchangeableItems | Item2 | true                 | true      |
@NoNewBrowser @Module-Alternates @TC-93989 @Order3
Scenario: 03 TC_93989 validate alternateItem.primaryStatus.updated Event in AlternatePartPools
	When User clicks on the "set-as-master" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Changing Cluster Master" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item2" in "Cluster-Mode" interchange master table
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName                         | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemPrimaryStatusUpdated | Item1 | Unidirectional   | false    | false       | false    | false                         | false       | false            |
	| AlternateItemDeactivated          | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemAdded | Item2 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| unidirectionalItems  | Item2 | true                 | true      |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | false                | false     |
	| interchangeableItems | Item2 | true                 | true      |
@NoNewBrowser @Module-Alternates @TC-93990 @Order4
Scenario: 04 TC_93990 validate alternateItem.deactivated Event in AlternatePartPools
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
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | false       |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemDeactivated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemDeactivated | Item2 | Unidirectional   | true     | false       | false    | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| unidirectionalItems  | Item2 | false                | true      |
	
@NoNewBrowser @Module-Alternates @TC-110459 @Order5
Scenario:05 TC_110459 validate alternateItem.reactivated Event in AlternatePartPools
	When User selects "Item1" from "Cluster-Mode" interchange master table
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName                | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemReactivated | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemAdded       | Item2 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| unidirectionalItems  | Item2 | true                 | true      |
@NoNewBrowser @Module-Alternates @TC-111494 @Order6
Scenario:06 TC_111494 Adding Alias to unidirectional cluster
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
	| FieldName   | value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item1"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateCreated   | Item2 | Unidirectional   | true     | false       | true     | false                         | false       | false            |
	| AlternateItemAdded | Item3 | Unidirectional   | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item2"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item1 | Unidirectional   | false    | false       | false    | false                         | false       | false            |
	| AlternateCreated   | Item2 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	| AlternateItemAdded | Item3 | Unidirectional   | false    | false       | true     | false                         | false       | true             |
	Then Verify the following details are updated in the "AlternatePartPools" container of the "pim" database for "Item3"
	| EventName          | Item  | RelationshipType | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateCreated   | Item2 | Unidirectional   | true     | false       | true     | false                         | true        | false            |
	| AlternateItemAdded | Item3 | Interchangeable  | true     | true        | true     | false                         | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Item1" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| interchangeableItems | Item1 | true                 | true      |
	| unidirectionalItems  | Item2 | true                 | true      |
	| unidirectionalItems  | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item2" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item1 | false                | false     |
	| interchangeableItems | Item2 | true                 | true      |
	| unidirectionalItems  | Item3 | true                 | false     |
	Then Verify the "Alternates" document is updated with the below details for "Item3" in "Integration-Data-Hub" 
	| AlternateItem        | Item  | IsActiveRelationship | IsPrimary |
	| unidirectionalItems  | Item2 | true                 | true      |
	| interchangeableItems | Item3 | true                 | true      |
	