Feature: AdminTool_42549_BOMKindExistsViewingScreen
	
@UI @BOM @HappyPath @Sanity @UserStory29174 @Automatable @UAT @Phase2_Sprint10
Scenario: AdminTool_42549_BOMKindExistsViewingScreen
	Given User login to the PIM application using the ppt username and password
    When User selects the "Admin Tool" in Hamburger menu
	Then Verify "Lovs" "Oem" subtab is displayed
	When User selects Admin Tool "BOM-Style-Kind" Tab  
	Then Verify "BOM" "Standard-Style-Kinds" subtab is displayed
	Then Verify row "0" is displayed in "BOM-Style" section
	Then Verify row "0" is displayed in "BOM-Kind" section
	When User clicks "Asset" item in "BOM-Style" Section
	Then User verifies "New" button is enabled under "BOM" tab
	Then User verifies "Undo" button is disabled under "BOM" tab
	Then User verifies "Save" button is disabled under "BOM" tab