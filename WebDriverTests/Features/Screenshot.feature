Feature: Screenshots
	So that I can work with multiple browsers
	I want to capture a screenshot
	and have a screenshot from each browser saved

@SuperDriver @Screenshot
Scenario: Screenshots
	Given I am on the Test Form page
	And I have an empty screenshot folder
	When I take a screenshot
	Then the screenshot should be saved