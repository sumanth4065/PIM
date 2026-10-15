Feature: ItemMaster_38576_ItemExistsForMultipleOEMs
	
@UI @ItemMaster @Sanity @Automatable @Phase2_Sprint11 @Module-Items
Scenario: ItemMaster_38576_ItemExistsForMultipleOEMs
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	Then User enters data for the following header fields
	| FieldName |
	| Item      |
	Then User searches and selects "Oem1" from dropdown
	Then Verify the below fields are not empty
	| FieldName   |
	| LOB         |
	| Oem         |
	| Division    |
	| Description |
	Then Verify that the following fields count displays as more "1"
	| FieldName |
	| OEM       |
	Then Verify that the following fields count displays as "1"
	| FieldName   |
	| LOB         |
	| Division    |
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then Verify that the following radio button fields are displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify that the following text box fields are displayed
	| fieldName |
	| Alias     |
	| Test      |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |
	Then User removes existing "Item"
	Then User enters data for the following header fields
	| FieldName |
	| Item      |
	Then User searches and selects "Oem2" from dropdown
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then Verify the below fields are not empty
	| FieldName          |
	| Item               |
	| LOB                |
	| Oem                |
	| Division           |
	| Description        |
	| Right-Panel-Status |
	Then Verify that the following radio button fields are displayed
	| FieldName      |
	| Product        |
	| Component		 |
	Then Verify that the following text box fields are displayed
	| FieldName |
	| Alias     |
	| Test      |
	Then Verify created and modified username and date in right corner
	| FieldName              |
	| Created-User           |
	| Created-Date           |
	| Modified-User          |
	| Modified-Date          |
	| Document-Modified-User |
	| Document-Modified-Date |



	