Feature: Alternate_107462_ValidateSelfReferencingInterchangebleSingleItemClusterBaselineItem

@UI @Alternates @Sanity @UserStory107072 @Automatable @Parts @Pool @Functional @Phase2_Sprint14 @Module-Alternates
Scenario: Alternate_107462_ValidateSelfReferencingInterchangebleSingleItemClusterBaselineItem
	Given User login to the PIM application using the ppt username and password
    When User selects the "Item" in Hamburger menu
	Then Verify Item Master "Explicit" tab is displayed
	When User clicks on the "Baseline" tab
	When User clicks on "New" button in Item Master
	Then User enters data for the following header fields
	| FieldName   |
	| Item-Id     |
	| Oem         |
	| Description |
	Then Verify "LOB" checkbox is displayed
	Then Verify "Status" is displayed as "Pending"
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldNames |
	| Alias      |
	| Test       |
	Then Verify the below fields are empty
	| FieldNames      |
	| Product-Class   |
	| Product-Category|
	Then User enters text into the "Internal-Notes" field
	Then User selects "Active" from the "Status" dropdown
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then User saves the item details as "Item1"
	Then Verify the below field values for "Item1" in Cosmos DB using "Pim" database and "Alternates" container
	| ClusterType             | IsActive | IsMaster |
	| Interchangeable Cluster | True     | True     |
	Then Verify AlternatePartPool document is not created for "Item1" in Pim
	
	     
		