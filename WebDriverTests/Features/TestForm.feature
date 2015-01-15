Feature: Test Form
	Example scenario to cover the sample application test form.

@SampleApplication @UI
Scenario: Test Form
	Given I am on the Test Form page
	And I have entered the name "Steve Fenton"
	And I have entered the email "steve@example.com"
	When I press Send
	Then the result should be "You entered Steve Fenton and steve@example.com"