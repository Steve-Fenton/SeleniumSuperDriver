# SeleniumSuperDriver
A WebDriver that distributes commands to multiple drivers in parallel.

## NuGet

You can install SuperDriver using NuGet

```
PM> Install-Package Fenton.Selenium.SuperDriver 
```

## Creating a SuperWebDriver

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

## Using the SuperWebDriver

The ```SuperWebDriver``` can be used just like any other ```IWebDriver```. It handles all the hard work distributing commands to all of the drivers, web controls, options objects and so on.

Everything with an interface has a partner Super object:

| Interface      | Substitute         |
|----------------|--------------------|
| IWebDriver     | SuperWebDriver     |
| ICookieJar     | SuperCookieJar     |
| INavigation    | SuperNavigation    |
| IOptions       | SuperOptions       |
| ITargetLocator | SuperTargetLocator |
| ITimeouts      | SuperTimeouts      |
| IWebElement    | SuperWebElement    |
| IWindow        | SuperWindow        |

The following classes have also been crowbarred out of the way using the ```new``` keyword to hide base class members.

| Class                 | Substitute                 |
|-----------------------|----------------------------|
| Cookie                | SuperCookie                |
| ReadOnlyCollection<T> | SuperReadOnlyCollection<T> |

Ideally, you shouldn't need to create anything except the ```SuperWebDriver```. The substitutes are used to ensure that commands are distributed to all web browsers and make multiple web browsers look like a single web browser to your code.

## Substitution and Return Value Selection

The current rules for substitution are:

Where any of these interfaces are returned from a method, they are automatically substituted with the appropriate ```SuperX``` class.

The current rules for return value selection are:

Where a simple type (such as ```string``` or ```bool```) is returned, a check is made to ensure that each browser gives the same answer. For example ```page.Title``` will return a single title, after a check has been made to ensure all browsers give the same answer.

Where a return type cannot be substituted, the value from the first browser is returned. (Currently applies to ```Point``` and ```Size```).

I am keen on getting feedback on these rules with a view to refining them to best work with real use cases. For example, if you are obtaining ```Size``` are you asserting that it is correct? (If so, you would only really be checking that the first browser in the collection is correct - does this behaviour need to be more sophisticated?)

## Contributions

Pre-Requisites - theses are the tools you'll need if you want to work with the source code or contribute to the project.

### Software 

 - Visual Studio 2013 Community Edition (or better)
 - TypeScript 1.4 (used in the sample application)

#### Software (Only needed to get code coverage if using Visual Studio Community Edition or Pro Edition)

 - NUnit (the installed version)
 - OpenCover (the installed version)

The installed version of NUnit and OpenCover seems to work best when using OpenCover to track code coverage.

### Visual Studio Extensions

 - SpecFlow
 - NUnit Test Adapted
 - OpenCover UI

### NuGet Packages

Just ensure "Enable NuGet Package Restore" is used on the solution and they'll all download.
