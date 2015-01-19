Feature: Navigation
	So that I can work with multiple browsers
	I want to send navigation commands
	and have them distributed to multiple drivers

@SuperDriver @Navigation
Scenario: Got To Url
	Given I am on the Test Form page
	When I navigate to the Test Navigation page
	Then I should be on the Test Navigation page

@SuperDriver @Navigation
Scenario: Back
	Given I am on the Test Navigation page
	And I navigate to the Test Form page
	When I navigate back
	Then I should be on the Test Navigation page

@SuperDriver @Navigation
Scenario: Foreward
	Given I am on the Test Form page
	And I navigate to the Test Navigation page
	And I navigate back
	When I navigate forward
	Then I should be on the Test Navigation page

@SuperDriver @Navigation
Scenario: Refresh
	Given I am on the Test Navigation page
	When I refresh the page
	Then I should be on the Test Navigation page
