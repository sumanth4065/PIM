Feature: AlternateInterchangeableClusterRelationship

@UI @Alternates @Sanity @UserStory34381 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_1_Alternate_41512_RecallInterchangeableClusterActiveRelationshipWithCluster
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
	When User selects the "Alternate" in Hamburger menu
	Then Verify the "Interchangeable" tab is displayed
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
	#Then User verifies and closes the "Alternate Items Management Tool: Cluster Selector" pop-up in "Cluster-Mode-Alternate-Items" panel, then enters "Item1" data in "Cluster-Mode-Cluster-Lookup" panel for the following fields
	#| FieldName |
	#| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
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
	Then Verify the below buttons are disabled in "Cluster-Mode-Master-Interchange" panel
	| ButtonName |
	| Undo       |
	| Save       |
	Then Verify the Display InActive toggle switch is "off"
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the entered "Item1" is selected automatically in the grid in the "Cluster-Mode" panel
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
	When User from the "Cluster-Mode-Selected-Cluster-Line" panel selects the "Selected-Item" tab
	Then Verify created and modified date and username for "Cluster-Mode-Selected-Cluster-Line" panel in "Selected-Item" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |



@NoNewBrowser @Alternates @Sanity @UserStory29162 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_2_Alternate_41520_InterchangeableClusterActiveRelationshipWithUpperCluster
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
	Then User saves the item details as "Item2"
	
	When User selects the "Alternate" in Hamburger menu
	Then Verify the "Interchangeable" tab is displayed
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
	#Then User verifies and closes the "Alternate Items Management Tool: Cluster Selector" pop-up in "Cluster-Mode-Alternate-Items" panel, then enters "Item1" data in "Cluster-Mode-Cluster-Lookup" panel for the following fields
	#| FieldName |
	#| Item-Id   |
	Then Verify the below buttons are enabled in "Cluster-Mode-Cluster-Lookup" panel
	| ButtonName                  |
	| Select-Another-Cluster      |
	| Add-Another-Item-To-Cluster |
	Then Verify the details of below fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName      | Value |
	| Total-Active   | 1     |
	| Total-Clusters | 1     |
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | true        |
	| IsMaster    | true        |
	Then Verify the entered "Item1" is selected automatically in the grid in the "Cluster-Mode" panel
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
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
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
	When User enters "Item2" data for the following fields in "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	Then Verify the "Record is already Active in another cluster" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects the "Item2" option
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
	Then Verify the below field values for "Item2" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | IsMaster |
	| Interchangeable Cluster | True     | False    |

@NoNewBrowser @Alternates @Sanity @UserStory29162 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_3_Alternate_41523_InterchangeableClusterSetAsMaster
	When User clicks on the "Set-As-Master" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Changing Cluster Master" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	Then Verify the below field values for "Item2" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | IsMaster |
	| Interchangeable Cluster | True     | True     |

@NoNewBrowser @Alternates @Sanity @UserStory128217 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_4_Alternate_41526_InterchangeableClusterViewClusterRelationships
	When User clicks on the "View-Cluster-Relationships" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Alternate Items Management Tool: Cluster Selector" pop up is displayed in the "Cluster-Mode-Alternate-Items" panel
	Then Verify the Clusters grid is displayed
	Then Verify the following details in pop up for the "Cluster-Mode-Alternate-Items" panel
	| FieldName    | Value |
	| Total-Items  | 2     |
	| Active-Items | 2     |
	When User clicks on the "Close-Popup" button from the "Cluster-Mode-Alternate-Items-Popup" panel

@NoNewBrowser @Alternates @Sanity @UserStory29162 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_5_Alternate_41521_InterchangeableClusterInactivateRelationshipWithUpperCluster
	When User selects "Item1" from "Cluster-Mode" interchange master table
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Selected-Cluster-Line" panel
	| FieldName |
	| Item-Id   |
	When User clicks on the "Inactivate-Relationship" button from the "Cluster-Mode-Selected-Cluster-Line" panel
	Then Verify the "Warning: Cluster Deactivation" pop up message is displayed
	Then User clicks on Yes button on the pop up
	When User confirms the "Save changes" by clicking on OK button in Alternates
	When User selects Display Inactive toggle button which is in "off" state
	Then Verify the below details of "Item1" in "Cluster-Mode" interchange master table
	| FieldName   | Value       |
	| Item-Id     | Item-Id     |
	| Oem         | Oem         |
	| Description | Description |
	| IsActive    | false       |
	| IsMaster    | false       |
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
	Then Verify the below field values for "Item1" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | IsMaster |
	| Interchangeable Cluster | False    | False    |

@NoNewBrowser @Alternates @Sanity @UserStory34381 @Automatable @ItemMaster @Phase2_Sprint12 @Module-Alternates
Scenario: Scenario_1_6_Alternate_41513_RecallInterchangeableClusterInActiveRelationshipWithCluster
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
	When User enters "Item1" data for the following fields in "Cluster-Mode-Cluster-Lookup" panel
	| FieldName |
	| Item-Id   |
	Then Verify the "Alternate Items Management Tool: Cluster Selector" pop up is displayed in the "Cluster-Mode-Alternate-Items" panel
	Then Verify the Clusters grid is displayed
	Then Verify the following details in pop up for the "Cluster-Mode-Alternate-Items" panel
	| FieldName    | Value |
	| Total-Items  | 2     |
	| Active-Items | 1     |
	Then Verify the below buttons are enabled in "Cluster-Mode-Alternate-Items-Popup" panel
	| ButtonName  |
	| Activate    |
	| Create      |
	| Close-Popup |
	Then Verify the same "Item1" is populated in the "Cluster-Mode-Alternate-Items-Popup-Selected" panel
	| FieldName |
	| Item-Id   |
	Then Verify created and modified date and username for "Cluster-Mode-Alternate-Items-Popup" panel in "Notes" tab
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |

	