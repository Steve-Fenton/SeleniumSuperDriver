Feature: Cookies
	So that I can work with multiple browsers
	I want to save and retrieve cookies
	and have them distributed to multiple drivers

@SuperDriver @Cookies
Scenario: Cookies
	Given I am on the Test Form page
	And I have sent a test cookie to be saved
	When I retrieve the test cookie
	Then the test cookie should be supplied
