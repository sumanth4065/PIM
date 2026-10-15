Feature: ItemMaster_38577_OEMItemCombinationExistsForMultipleLOBs
	
@UI @Sanity @Automatable @ItemMaster @Phase2_Sprint11 @Module-Items
Scenario: ItemMaster_38577_OEMItemCombinationExistsForMultipleLOBs
	Given User login to the PIM application using the ppt username and password
	When User selects the "Item" in Hamburger menu
	Then User enters data for the following header fields
	| FieldName   |
	| Item        |
	Then Verify the below fields are not empty
	| FieldName   |
	| Oem         |
	Then Verify that the following fields count displays as more "1"
	| FieldName |
	| LOB       |
	Then User searches and selects "LOB1" from dropdown
	Then Verify that the following fields count displays as "1"
	| FieldName |
	| Oem       |
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
	Then User removes existing "Item"
	Then User enters data for the following header fields
	| FieldName |
	| Item      |
	Then User searches and selects "LOB2" from dropdown
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
	Then Verify taxonomy characteristic kind fields are not empty
	| FieldName |
	| Family    |
	| Line      |
	| Group     |



	