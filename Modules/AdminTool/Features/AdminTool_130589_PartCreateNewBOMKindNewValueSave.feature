Feature: AdminTool_130589_PartCreateNewBOMKindNewValueSave
	
@UI @BOM @HappyPath @Sanity @UserStory29174 @Automatable @UAT @Phase2_Sprint10
Scenario: AdminTool_130589_PartCreateNewBOMKindNewValueSave
	Given User login to the PIM application using the ppt username and password
    When User selects the "Admin Tool" in Hamburger menu
	Then Verify "Lovs" "Oem" subtab is displayed
	When User selects Admin Tool "BOM-Style-Kind" Tab  
	Then Verify "BOM" "Standard-Style-Kinds" subtab is displayed
	Then Verify row "0" is displayed in "BOM-Style" section
	Then Verify row "0" is displayed in "BOM-Kind" section
	When User clicks "Product" item in "BOM-Style" Section
	Then User verifies "New" button is enabled under "BOM" tab
	Then User verifies "Undo" button is disabled under "BOM" tab
	Then User verifies "Save" button is disabled under "BOM" tab
	When User clicks on "New" button under "BOM" tab
	Then Verify Is "Active" checkbox is checked
	Then Verify Not "Editable" checkbox is unchecked
	Then User enters data for the below fields
	| FieldName   |
	| Name        |
	| Sequencing  |
	| Description |
	| Rank        |
	Then User enters text into the Internal "Notes" field
	When User clicks on "Save" button under "BOM" tab
	Then User confirms the Save changes
	Then User verifies "save" button is disabled under "BOM" tab
	Then Verify created and modified username and date in right corner
	| FieldName     |
	| Created-User  |
	| Created-Date  |
	| Modified-User |
	| Modified-Date |  
	Then Verify below field values with Cosmos DB using "Pim" database and "AdminDefinitions" container using "Name" reference
	| FieldName   |
	| Name        |
	| Sequencing  |
	| Description |
	| Rank        |
	| Notes       |
	| IsActive    |
	Then User verifies the UI "Modified-Date" and Source Type as "GUI" with Cosmos DB using the "Pim" database and "AdminDefinitions" container