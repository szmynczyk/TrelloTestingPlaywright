Feature: Create new Trello Board

A short summary of the feature

Scenario: Clicking Create new board button opens Create board dialog
	Given I go to main trello page
	And I login with credentials "szmynczyk.test@interia.pl" "Test1234"
	When I click on Create new board element
	Then new board dialog is displayed

Scenario: Create new board
	Given I go to main trello page
	And I login with credentials "szmynczyk.test@interia.pl" "Test1234"
	When I click on Create new board element
	And create new board with title "Some test board"
	Then new board is visible on main page