Feature: Sign in to trello

A short summary of the feature

@tag1
Scenario: Sign in to trello
	Given I go to main trello page
	And I login with credentials "szmynczyk.test@interia.pl" "Test1234"
	Then main page is displayed