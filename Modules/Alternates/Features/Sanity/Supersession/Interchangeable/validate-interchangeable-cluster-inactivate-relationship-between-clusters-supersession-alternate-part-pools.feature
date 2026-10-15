Feature: Validate Interchangeable Cluster Inactivate Relationship Between Clusters Supersession Alternate Part Pools

@UI @UserStory121592 @Alternates_E2E @Automatable_Alternates @Module-Alternates
Scenario: Alternate_122586_InterchangeableInactivateRelationshipSupersession
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	#Item1 Creation
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
	Then User saves the item details as "Cluster1Item1"
	#Item2 Creation
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
	Then User saves the item details as "Cluster1Item2"
	#Item3 Creation
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
	Then User saves the item details as "Cluster2Item1"
	#Item4 Creation
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
	Then User saves the item details as "Cluster2Item2"
	Then Verify document is created for "Cluster2Item2" in Cosmos DB using "Pim" database and "ItemPools" container
	Then Verify below documents are created for "Cluster2Item2" in "Integration-Data-Hub" database and "Items" container
	| DocumentType |
	| Item         |
	| ItemCatalog  |
	
	
	When User selects the "Alternate" in Hamburger menu
	Then Verify the "Interchangeable" tab is displayed
	#Create 1st Interchangeable Cluster
	Then Verify the below text fields are empty in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then Verify the below buttons are disabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	When User enters "Cluster1Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
	Then Verify the below details of "Cluster1Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the entered "Cluster1Item1" is selected automatically in the grid in the "Cluster-Mode" panel
	Then Verify created and modified username and date in "Cluster-Mode-Master-Interchange" panel
	| FieldName    |
	| Cluster-Date |
	Then Verify the below buttons are enabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName         |
	| Edit-Cluster-Notes |
	Then Verify the below buttons are disabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify the Display InActive toggle switch is "off"
	Then Verify the same "Cluster1Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Inactivate-Relationship    |
	| Edit-Item-To-Cluster-Notes |
	| View-Cluster-Relationships |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Create-Single-Item-Cluster |
	| Save                       |
	| Undo                       |
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
	When User enters "Cluster1Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the "Record is already Active in another cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects the "Cluster1Item2" option
	Then Verify the details of below fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName      | Value |
	| Total-Active   | 0     |
	| Total-Clusters | 1     |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the details of below fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
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
	When User clicks on the "Inactivate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Deactivation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects Display Inactive toggle button which is in "off" state
	Then Verify the below details of "Cluster1Item2" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | false       |
	| IsMaster    | false       |
	#Create 2nd Interchangeable Cluster
	When User refreshes the page
	Then Verify the "Interchangeable" tab is displayed
	Then Verify the below text fields are empty in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	Then Verify the below buttons are disabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	When User enters "Cluster2Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
	Then Verify the below details of "Cluster2Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the entered "Cluster2Item1" is selected automatically in the grid in the "Cluster-Mode" panel
	Then Verify created and modified username and date in "Cluster-Mode-Master-Interchange" panel
	| FieldName    |
	| Cluster-Date |
	Then Verify the below buttons are enabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName         |
	| Edit-Cluster-Notes |
	Then Verify the below buttons are disabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify the Display InActive toggle switch is "off"
	Then Verify the same "Cluster2Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Inactivate-Relationship    |
	| Edit-Item-To-Cluster-Notes |
	| View-Cluster-Relationships |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Create-Single-Item-Cluster |
	| Save                       |
	| Undo                       |
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
	When User enters "Cluster2Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the "Record is already Active in another cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects the "Cluster2Item2" option
	Then Verify the details of below fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName      | Value |
	| Total-Active   | 0     |
	| Total-Clusters | 1     |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the details of below fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
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
	When User clicks on the "Inactivate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Deactivation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects Display Inactive toggle button which is in "off" state
	Then Verify the below details of "Cluster2Item2" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | false       |
	| IsMaster    | false       |
	#Create Supersession Interchangeable Cluster And Activate
	When User selects the "Supersession-Mode" tab
	Then Verify the "Supersession-Interchangeable" tab is displayed
	Then Verify the below text fields are empty in "Supersession-Mode-Subordinate-Cluster" panel
	| FieldName |
	| Item-Id   |
	| Oem       |
	Then Verify the below buttons are disabled in "Supersession-Mode-Subordinate-Cluster" panel
	| ButtonName             |
	| Select-Another-Cluster |
	When User enters "Cluster1Item1" data for the following fields in "Supersession-Mode-Subordinate-Cluster" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Supersession-Mode-Subordinate-Cluster" panel
	| ButtonName             |
	| Select-Another-Cluster |
	Then Verify the below cluster details of "Cluster1Item1, Cluster1Item2" in "Supersession-Mode-Master-Interchangeable" table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true,false  |
	| IsMaster    | true,false  |
	Then Verify the entered cluster item "Cluster1Item1" is selected automatically in the grid in the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify created and modified date and username for "Supersession-Mode-Master-Interchangeable" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the Display InActive toggle switch is "On" in Supersession Mode
	Then Verify the same "Cluster1Item1" is populated in the "Supersession-Mode-selected-Subordinate-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify created and modified date and username for "Supersession-Mode-Selected-Subordinate-Cluster-Line" panel in "Item-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the below buttons are enabled in "Supersession-Mode-Selected-Subordinate-Cluster-Line" panel
	| ButtonName                 |
	| View-Cluster-Relationships |
	Then Verify the below text fields are enabled in the "Supersession-Mode-Superior-Cluster" panel
	| FieldName |
	| Item-Id   |
	When User enters "Cluster2Item1" data for the following fields in "Supersession-Mode-Superior-Cluster" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Supersession-Mode-Superior-Cluster" panel
	| ButtonName             |
	| Select-Another-Cluster |
	Then Verify the below cluster details of "Cluster2Item1, Cluster2Item2" in "Supersession-Mode-Suprior-Interchangeable" table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true,false  |
	| IsMaster    | true,false  |
	Then Verify the entered "Cluster2Item1" from the cluster is selected automatically in the grid in the "Supersession-Mode-Suprior-Interchangeable" panel
	Then Verify created and modified date and username for "Supersession-Mode-Suprior-Interchangeable" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the Display InActive toggle switch is "On" in Supersession Mode
	Then Verify the same "Cluster2Item1" is populated in the "Supersession-Mode-Selected-Suprior-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify created and modified date and username for "Supersession-Mode-Master-Interchangeable" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify created and modified date and username for "Supersession-Mode-Selected-Suprior-Cluster-Line" panel in "Item-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the "Relationship-Active" checkbox unchecked state from the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify the below buttons are enabled in "Supersession-Mode-Selected-Suprior-Cluster-Line" panel
	| ButtonName                 |
	| View-Cluster-Relationships |
	Then Verify the below buttons are disabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                               |
	| Inactivate-Relationship-Between-Clusters |
	| Undo                                     |
	| Save                                     |
	| Edit-Item-To-Cluster-Notes               |
	Then Verify the below buttons are enabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                             |
	| Activate-Relationship-Between-Clusters |
	When User clicks on the "Activate-Relationship-Between-Clusters" button from the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below buttons are enabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                               |
	| Inactivate-Relationship-Between-Clusters |
	| Edit-Item-To-Cluster-Notes               |
	Then Verify the below buttons are disabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                             |
	| Activate-Relationship-Between-Clusters |
	| Undo                                   |
	| Save                                   |
	Then Verify the "Relationship-Active" checkbox checked state from the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify created and modified date and username for "Supersession-Mode-Master-Interchangeable" panel in "Cluster-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Cluster1Item1"
	| EventName          | Item          | RelationshipType             | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemAdded | Cluster2Item1 | Interchangeable_Supersession | true     | false       | true     | true                          | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Cluster1Item1" in "Integration-Data-Hub" 
	| AlternateItem                    | Item          | IsActiveRelationship | IsPrimary |
	| interchangeablesupersessionitems | Cluster2Item1 | true                 | true      |
	When User clicks on the "Inactivate-Relationship-Between-Clusters" button from the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify the "Warning: Cluster Deactivation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below buttons are disabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                               |
	| Inactivate-Relationship-Between-Clusters |
	| Undo                                     |
	| Save                                     |
	Then Verify the below buttons are enabled in "Supersession-Mode-Master-Interchangeable" panel
	| ButtonName                             |
	| Activate-Relationship-Between-Clusters |
	| Edit-Item-To-Cluster-Notes             |
	Then Verify the "Relationship-Active" checkbox unchecked state from the "Supersession-Mode-Master-Interchangeable" panel
	Then Verify created and modified date and username for "Supersession-Mode-Master-Interchangeable" panel in "Cluster-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the cluster replacement field values for "Cluster1Item1" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | DocumentType        |
	| Interchangeable Cluster | false    | Cluster Replacement |
	Given trigger wait is required
	Then Verify the following details are updated in the "AlternatePartPools" container of the "Pim" database for "Cluster1Item1"
	| EventName                | Item          | RelationshipType             | IsMaster | IsGenerated | IsActive | IsImmediateSupersessionMaster | IsAliasPart | IsAliasAlternate |
	| AlternateItemDeactivated | Cluster2Item1 | Interchangeable_Supersession | true     | false       | false    | true                          | false       | false            |
	Then Verify the "Alternates" document is updated with the below details for "Cluster1Item1" in "Integration-Data-Hub" 
	| AlternateItem                    | Item          | IsActiveRelationship | IsPrimary |
	| interchangeablesupersessionitems | Cluster2Item1 | false                | true      |
