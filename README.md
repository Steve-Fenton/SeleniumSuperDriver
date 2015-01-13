# SeleniumSuperDriver
A WebDriver that distributes commands to multiple drivers in parallel.


Creating a SuperWebDriver
===

A ```SuperWebDriver``` can run as many individual ```IWebDrivers``` as you would like (and your machine can take).

```csharp
IWebDriver driver = new SuperWebDriver(new ChromeDriver(), new FirefoxDriver());
```

You are not limited to one of each kind of driver:

```csharp
IWebDriver driver = new SuperWebDriver(new ChromeDriver(), new FirefoxDriver(), new FirefoxDriver());
```

Creating drivers can take some time, so consider creating them in parallel. Simple example:

```csharp
IWebDriver driver = new SuperWebDriver(GetDriverSuite());
```

Here is the ```GetDriverSuite``` method:

```csharp
private static IList<IWebDriver> GetDriverSuite()
{
    // Allow some degree of parallelism when creating drivers, which can be slow
    IList<IWebDriver> drivers = new List<Func<IWebDriver>>
    {
        () =>  { return GetDriverFor(Browser.Chrome); },
        () =>  { return GetDriverFor(Browser.Firefox); },
    }.AsParallel().Select(d => d()).ToList();

    return drivers;
}
```

Using the SuperWebDriver
===

The ```SuperWebDriver``` can be used just like any other ```IWebDriver```. It handles all the hard work distributing commands to all of the drivers, web controls, options objects and so on.

Everything with an interface has a partner Super object:

| Interface      | Substitute         |
| IWebDriver     | SuperWebDriver     |
| ICookieJar     | SuperCookieJar     |
| INavigation    | SuperNavigation    |
| IOptions       | SuperOptions       |
| ITargetLocator | SuperTargetLocator |
| ITimeouts      | SuperTimeout       |
| IWebElement    | SuperWebElement    |
| IWindow        | SuperWindow        |

Ideally, you shouldn't need to create anything except the ```SuperWebDriver```. The substitutes are used to ensure that commands are distributed to all web browsers and make multiple web browsers look like a single web browser to your code.

Substitution and Return Value Selection
===

The current rules for substitution are:

Where any of these interfaces are returned from a method, they are automatically substituted with the appropriate ```SuperX``` class.

The current rules for return value selection are:

Where a simple type (such as ```string``` or ```bool```) is returned, a check is made to ensure that each browser gives the same answer. For example ```page.Title``` will return a single title, after a check has been made to ensure all browsers give the same answer.

Where a concrete type is returned (such as ```Point``` or ```Size```) the answer is returned from the first browser.

I am keen on getting feedback on these rules with a view to refining them to best work with real use cases. For example, if you are obtaining ```Size``` are you asserting that it is correct? (If so, you would only really be checking that the first browser in the collection is correct - does this behaviour need to be more sophisticated?)
