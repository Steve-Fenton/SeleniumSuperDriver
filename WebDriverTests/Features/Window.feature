Feature: Window
	So that I can work with multiple browsers
	I want to send window commands
	and have them distributed to multiple drivers

@SuperDriver @Navigation
Scenario: Maximise And Check
	Given I am on the Test Form page
	When I maximise the browser
	Then all browsers should be maximised