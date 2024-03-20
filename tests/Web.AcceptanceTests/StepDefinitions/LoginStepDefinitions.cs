namespace CleanArchitecture.Web.AcceptanceTests.StepDefinitions;

[Binding]
public sealed class LoginStepDefinitions
{
    private readonly LoginPage _loginPage;

    public LoginStepDefinitions(LoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    [BeforeFeature("Login")]
    public static async Task BeforeLoginScenario(IObjectContainer container)
    {
        IPlaywright playwright = await Playwright.CreateAsync();

        BrowserTypeLaunchOptions options = new();

#if DEBUG
        options.Headless = false;
        options.SlowMo = 500;
#endif

        IBrowser browser = await playwright.Chromium.LaunchAsync(options);

        IPage page = await browser.NewPageAsync();

        LoginPage loginPage = new(browser, page);

        container.RegisterInstanceAs(playwright);
        container.RegisterInstanceAs(browser);
        container.RegisterInstanceAs(loginPage);
    }

    [Given("a logged out user")]
    public async Task GivenALoggedOutUser()
    {
        await _loginPage.GotoAsync();
    }

    [When("the user logs in with valid credentials")]
    public async Task TheUserLogsInWithValidCredentials()
    {
        await _loginPage.SetEmail("administrator@localhost");
        await _loginPage.SetPassword("Administrator1!");
        await _loginPage.ClickLogin();
    }

    [Then("they log in successfully")]
    public async Task TheyLogInSuccessfully()
    {
        string? profileLinkText = await _loginPage.ProfileLinkText();

        _ = profileLinkText.Should().NotBeNull();
        _ = profileLinkText.Should().Be("Account");
    }

    [When("the user logs in with invalid credentials")]
    public async Task TheUserLogsInWithInvalidCredentials()
    {
        await _loginPage.SetEmail("hacker@localhost");
        await _loginPage.SetPassword("l337hax!");
        await _loginPage.ClickLogin();
    }

    [Then("an error is displayed")]
    public async Task AnErrorIsDisplayed()
    {
        bool errorVisible = await _loginPage.InvalidLoginAttemptMessageVisible();

        _ = errorVisible.Should().BeTrue();
    }

    [AfterFeature]
    public static async Task AfterScenario(IObjectContainer container)
    {
        IBrowser browser = container.Resolve<IBrowser>();
        IPlaywright playright = container.Resolve<IPlaywright>();

        await browser.CloseAsync();
        playright.Dispose();
    }
}