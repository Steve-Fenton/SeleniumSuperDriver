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

@SuperDriver @Cookies
Scenario: Delete Cookie
	Given I am on the Test Form page
	And I have sent a test cookie to be saved
	When I delete the test cookie
	Then the test cookie should be gone

@SuperDriver @Cookies
Scenario: Delete Cookie By Name
	Given I am on the Test Form page
	And I have sent a test cookie to be saved
	When I delete the test cookie by name
	Then the test cookie should be gone
