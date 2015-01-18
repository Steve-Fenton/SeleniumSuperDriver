Feature: FindElements
	So that I can work with multiple browsers
	I want to issue a find element command to a single driver
	and have that distributed to multiple drivers

@SuperDriver @FindElements
Scenario Outline: Find Elements
	Given I am on the Test Form page
	When I select elements "<By>" "<Selector>"
	Then there should be <NumberOfMatches>

Examples:
	| By              | Selector                             | NumberOfMatches |
	| TagName         | a                                    | 3               |
	| TagName         | article                              | 1               |
	| TagName         | div                                  | 4               |
	| Id              | name                                 | 1               |
	| ClassName       | test                                 | 2               |
	| CssSelector     | nav ul li                            | 3               |
	| LinkText        | Link B                               | 1               |
	| Name            | email                                | 1               |
	| PartialLinkText | Link                                 | 3               |
	| XPath           | /html/body/main/article/header/h2[1] | 1               |

@SuperDriver @FindElement
Scenario Outline: Find Element
	Given I am on the Test Form page
	When I select an element "<By>" "<Selector>"
	Then the text should be "<Content>"

Examples:
	| By          | Selector                             | Content      |
	| TagName     | h2                                   | Test Heading |
	| Id          | testheading                          | Test Sub A   |
	| CssSelector | main h2                              | Test Heading |
	| LinkText    | Link B                               | Link B       |
	| XPath       | /html/body/main/article/header/h2[1] | Test Heading |
