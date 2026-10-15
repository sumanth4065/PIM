Feature: Alternate_111425_ValidateSelfReferencingInterchangebleClusterByImportBaselineItem

@UI @Alternates @Sanity @UserStory42743 @Automatable @ItemMaster @Phase2_Sprint15 @Pool @Module-Alternates
Scenario: Alternate_111425_ValidateSelfReferencingInterchangebleClusterByImportBaselineItem
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Item-Data-Items-Baseline" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "CreateExplicitItem" Excel file for the "Baseline-Item" tab
	Then User verifies valid data is present in the "CreateExplicitItem" Excel file 
	And User uploads the "CreateExplicitItem" Excel file
	Then User clicks on "Import" button
	And Verify the imported file is processed successfully on the import screen
	When User selects the "Item" in Hamburger menu 
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	Then User recalls the following fields from "CreateExplicitItem" Excel for Explicit Item
	| FieldName |
	| Item      |
	Then Verify the below fields are not empty
	| FieldName   |
	| OEM         |
	| Description |
	Then User Loads "CreateExplicitItem" excel with row index 1 as "Item1"
	Then Verify AlternatePartPool document is not created for "Item1" in Pim