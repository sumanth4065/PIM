Feature: ItemMaster_64893_AddCharacteristicsExplicitOrBaselineTaxonomyItemDoesnotHaveDefaultAdminValue

@UI @Sanity @HappyPath @UserStory64014 @Automatable @Phase2_Sprint11 @Module-Items
Scenario:ItemMaster_64893_AddCharacteristicsExplicitOrBaselineTaxonomyItemDoesnotHaveDefaultAdminValue
	Given User login to the PIM application using the ppt username and password
	When User selects the "Imports" in Hamburger menu
	Then User is able to view the Import screen 
	Then User clicks on "New-Import" button 
	And User selects import type as "Characteristics-Data-Baseline" from dropdown  
	Then User clicks on "Browse" button
	Then User verifies the Excel sheet headers from the "ItemCreate" Excel file for the "Baseline" tab
	Then User verifies valid data is present in the "ItemCreate" Excel file 
	And User uploads the "ItemCreate" Excel file
	Then User clicks on "Import" button
	And User verifies the error message is displayed after successfully importing the "ItemCreate" Excel file