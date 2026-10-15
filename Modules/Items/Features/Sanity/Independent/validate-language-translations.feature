Feature: Validate Language Translations for New Explicit Item

@UI @Module-Items @TCID-100849
Scenario: TC-100849 Validate Language Translation Values for Explicit Item
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
	Then User selects "Active" from the "Status" dropdown
	Then Verify "Product" normally radio button is selected
	Then Verify "Component" normally radio button is unselected
	Then Verify that the following checkboxes are unchecked
	| FieldNames |
	| Alias      |
	| Test       |
	Then Verify the below fields are empty
	| FieldNames       |
	| Product-Class    |
	| Product-Category |
	Then User enters text into the "Internal-Notes" field
	Then User enters data for the following product fields
	| FieldNames       |
	| Product-Class    |
	| Product-Category |	
	Then Verify "Undo" and "Save" buttons are enabled
	When User clicks on "Save" button in Item Master
	Then User confirms the Save changes
	Then User recalls the created "Item" Id
	Then Verify "Product" Taxonomy is displayed
	When User clicks on "Edit" button in Taxonomy section
	Then User enters "Product" Taxonomy details
	| Fields |
	| Family |
	| Line   |
	| Group  |
	When User clicks on "Save" button in Taxonomy section
	Then User confirms the Save changes
	Then Verify that the following characteristic display boxes are displayed
	| PanelNames                       |
	| Default-Characteristics-Baseline |
	| Default-Characteristics-Taxonomy |
	| Characteristics-Display          |
	Then Verify row "0" is displayed in characteristic "Kind" section
	Then User selects characteristic Kind as "Language Translations" and Type as "German Description"
	Then User gets the value of the "German Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Event History should be updated with in the "ItemPools" container of the "Pim" database in "ItemPool" document
	| EventHistory                |
	| ItemCreated                 |
	| ItemCatalogCreated          |
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType |
	| Language Translations | German Description |
	Then User selects characteristic Kind as "Language Translations" and Type as "Japanese Description"
	Then User gets the value of the "Japanese Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType   |
	| Language Translations | Japanese Description |
	Then User selects characteristic Kind as "Language Translations" and Type as "Portuguese Description"
	Then User gets the value of the "Portuguese Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType     |
	| Language Translations | Portuguese Description |
	Then User selects characteristic Kind as "Language Translations" and Type as "Italian Description"
	Then User gets the value of the "Italian Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType  |
	| Language Translations | Italian Description |
	Then User selects characteristic Kind as "Language Translations" and Type as "Mandarin Description"
	Then User gets the value of the "Mandarin Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType   |
	| Language Translations | Mandarin Description |
	Then User selects characteristic Kind as "Language Translations" and Type as "Spanish Description"
	Then User gets the value of the "Spanish Description" characteristic
	Then Verify Characteristic type "String" value should be updated with in the "Pim" database and "Defaults" container in "Item Default" document
	Then Verify Language Translation value should be updated with in the "Pim" database and "ItemPools" container in "ItemPool" document
	| CharacteristicKind    | CharacteristicType  |
	| Language Translations | Spanish Description |