# SeleniumSuperDriver
A WebDriver that distributes commands to multiple drivers in parallel.


Creating a SuperDriver
===

A SuperDriver can run as many individual drivers as you would like (and your machine can take).

```IWebDriver driver = new SuperWebDriver(new ChromeDriver(), new FirefoxDriver());```

You are not limited to one of each kind of driver:

```IWebDriver driver = new SuperWebDriver(new ChromeDriver(), new FirefoxDriver(), new FirefoxDriver());```

Creating drivers can take some time, so consider creating them in parallel. Simple example:

```
IWebDriver driver = new SuperWebDriver(GetDriverSuite());
```

Here is the ```GetDriverSuite``` method:

```
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
