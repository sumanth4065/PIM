Feature: ItemMaster_38575_ItemRetrievalOneRecordExists
	
@UI @BOM @HappyPath @Sanity @UserStory29174 @Automatable @UAT @Phase2_Sprint11 @Module-Items
Scenario: ItemMaster_38575_ItemRetrievalOneRecordExists
	Given User login to the PIM application using the ppt username and password
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	Then User enters data for the following header fields
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| Oem         |
	| Division    |
	| Description |
	Then Verify that the following fields count displays as "1"
	| FieldName   |
	| LOB         |
	| Oem         |
	| Division    |
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then Verify text is displayed for below fields
	| FieldName      |
	| Internal-Notes |
	Then Verify that the following radio button fields are displayed
	| Product        |
	| Component		 |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |

