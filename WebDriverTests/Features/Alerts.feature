Feature: Alerts
	So that I can work with multiple browsers
	I want to manage alerts with commands
	and have them distributed to multiple drivers

@SuperDriver @Alerts
Scenario: Accept Alert
	Given I am on the Test Form page
	And I have triggered an alert
	When I accept the alert
	Then the alert should close

@SuperDriver @Alerts
Scenario: Dismiss Alert
	Given I am on the Test Form page
	And I have triggered an alert
	When I dismiss the alert
	Then the alert should close