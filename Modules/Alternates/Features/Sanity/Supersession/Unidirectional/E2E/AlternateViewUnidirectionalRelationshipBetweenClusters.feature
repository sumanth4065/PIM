Feature: AlternateViewUnidirectionalRelationshipBetweenClusters

@UI @Alternates @Sanity @UserStory41607 @Automatable @ItemMaster @Phase2_Sprint13 @Module-Alternates
Scenario: Scenario_16_1_Alternate_42235_ViewUnidirectionalActivateRelationshipBetweenClustersSupersession
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

	
	When User selects the "Alternate" in Hamburger menu
	Then Verify the "Interchangeable" tab is displayed
	#Create 1st Unidirectional Cluster Via Cluster Mode
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
	When User enters "Cluster1Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	When User clicks on the "Create-Single-Item-Cluster" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Create New Cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
	Then Verify the entered "Cluster1Item1" is selected automatically in the grid in the "Cluster-Mode" panel
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |	
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
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Inactivate-Relationship    |
	| Edit-Item-To-Cluster-Notes |
	| View-Cluster-Relationships |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Create-Single-Item-Cluster |
	| Set-As-Master              |
	| Save                       |
	| Undo                       |
	When User clicks on the "Add-Another-Item-To-Cluster" button from the "Cluster-Mode-Cluster-Lookup" panel
	Then Verify the below text fields are empty in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	When User enters "Cluster1Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the entered "Cluster1Item2" is selected automatically in the grid in the "Cluster-Mode" panel
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
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	#Create 2nd Unidirectional Cluster Via Cluster Mode
	When User refreshes the page
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
	When User enters "Cluster2Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	When User clicks on the "Create-Single-Item-Cluster" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Create New Cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
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
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |	
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
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the below buttons are enabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Inactivate-Relationship    |
	| Edit-Item-To-Cluster-Notes |
	| View-Cluster-Relationships |
	Then Verify the below buttons are disabled in "Cluster-Mode-Selected-Cluster-Line" panel
	| ButtonName                 |
	| Activate-Relationship      |
	| Create-Single-Item-Cluster |
	| Set-As-Master              |
	| Save                       |
	| Undo                       |
	When User clicks on the "Add-Another-Item-To-Cluster" button from the "Cluster-Mode-Cluster-Lookup" panel
	Then Verify the below text fields are empty in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName   |
	| Item-Id     |
	| Oem         |
	When User enters "Cluster2Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Activate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Activation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below details of "Cluster2Item2" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | false       |
	Then Verify the entered "Cluster2Item2" is selected automatically in the grid in the "Cluster-Mode" panel
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
	Then Verify created and modified date and username for "Cluster-Mode-Master-Interchange" panel in "Active-Cluster" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	#Create Unidirectional Cluster Via Supersession and Activate 
	When User selects the "Supersession-Mode" tab
	Then Verify the "Supersession-Interchangeable" tab is displayed
	When User selects the "Supersession-Unidirectional" tab
	Then Verify the "Supersession-Unidirectional" tab is displayed
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
	| IsActive    | true,true   |
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
	| IsActive    | true,true   |
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
	Then Verify the below buttons are enabled in "Supersession-Mode-Selected-Suprior-Cluster-Line" panel
	| ButtonName                 |
	| View-Cluster-Relationships |
	Then Verify the "Relationship-Active" checkbox unchecked state from the "supersession-mode-master-interchangeable" panel
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
	When User clicks on the "View-Cluster-Relationships" button from the "Supersession-Mode-Selected-Subordinate-Cluster-Line" panel
	Then Verify the "Alternate Items Management Tool: View Cluster Relationships" pop up is displayed in the "Supersession-Mode-Cluster-Relationship" panel
	Then Verify the Clusters grid is displayed
	Then Verify the following details in pop up for the "Supersession-Mode-Cluster-Relationship" panel
	| FieldName    | Value |
	| Total-Items  | 2     |
	| Active-Items | 2     |
	Then Verify the below buttons are enabled in "Supersession-Mode-Cluster-Relationship-Popup" panel
	| ButtonName  |
	| Close-Popup |
	Then Verify the same "Cluster1Item1" is populated in the "Supersession-Mode-Cluster-Relationship-Popup-Selected" panel
	| FieldName |
	| Item-Id   |
	Then Verify created and modified date and username for "Supersession-Mode-Cluster-Relationship-Popup" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	Then Verify the below cluster details of "Cluster1Item1,Cluster1Item2" in "Interchangeable" table for "Supersession-Mode-Cluster-Relationship" panel
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true,true   |
	| IsMaster    | true,false  |
	Then Verify the below cluster details of "Cluster2Item1,Cluster2Item2" in "Extended-Interchangeable" table for "Supersession-Mode-Cluster-Relationship" panel
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true,true   |
	| IsMaster    | true,false  |
	When User clicks on the "Close-Popup" button from the "Supersession-Mode-Cluster-Relationship-Popup" panel

@NoNewBrowser @Alternates @Sanity @UserStory41607 @Automatable @ItemMaster @Phase2_Sprint13	 @Module-Alternates
Scenario: Scenario_16_2_Alternate_42236_ViewUnidirectionalActivateRelationshipBetweenClustersSupersession
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
	| ButtonName                               |
	| Activate-Relationship-Between-Clusters |
	| Edit-Item-To-Cluster-Notes               |
	Then Verify created and modified date and username for "Supersession-Mode-Master-Interchangeable" panel in "Cluster-To-Cluster-Relationship" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	When User clicks on the "View-Cluster-Relationships" button from the "Supersession-Mode-Selected-Suprior-Cluster-Line" panel
	Then Verify the "Alternate Items Management Tool: View Cluster Relationships" pop up is displayed in the "Supersession-Mode-Cluster-Relationship" panel
	Then Verify the Clusters grid is displayed
	Then Verify the following details in pop up for the "Supersession-Mode-Cluster-Relationship" panel
	| FieldName    | Value |
	| Total-Items  | 2     |
	| Active-Items | 2     |
	Then Verify the same "Cluster2Item1" is populated in the "Supersession-Mode-Cluster-Relationship-Popup-Selected" panel
	| FieldName |
	| Item-Id   |
	Then Verify created and modified date and username for "Supersession-Mode-Cluster-Relationship-Popup" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |
	When User clicks on the "Close-Popup" button from the "Supersession-Mode-Cluster-Relationship-Popup" panel

