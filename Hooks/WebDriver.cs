using BoDi;
using OpenQA.Selenium;
using Simple2u.Config;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace Simple2u.Hooks
{
    [Binding]
    public sealed class WebDriver
    {
        public ConfigurationHelper config;
        private readonly IObjectContainer _objectContainer;

        public WebDriver(IObjectContainer objectContainer)
        {
            config = new ConfigurationHelper();
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            var webdriver = WebDriverFactory.CreateWebDriver(config);
            var selenium = new SeleniumHelper(webdriver, config);

            _objectContainer.RegisterInstanceAs(config);
            _objectContainer.RegisterInstanceAs(selenium);
            _objectContainer.RegisterInstanceAs(webdriver);

            if (selenium.RodandoNoBrowserStack())
            {
                ((IJavaScriptExecutor)webdriver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionName\", \"arguments\": {\"name\":\"" + FormatarTextoParaBrowserStack(featureContext.FeatureInfo.Title + ": " + scenarioContext.ScenarioInfo.Title) + "\"}}");
            }
        }

        [BeforeStep]
        public void BeforeStep(ScenarioContext scenarioContext, SeleniumHelper selenium, IWebDriver webdriver)
        {
            if (selenium.RodandoNoBrowserStack())
            {
                var stepContext = scenarioContext.StepContext;
                var text = "browserstack_executor: {\"action\": \"annotate\", \"arguments\": {\"data\":\"" + FormatarTextoParaBrowserStack(TextoStepDefinitionType(stepContext.StepInfo.StepDefinitionType)) + " " + FormatarTextoParaBrowserStack(stepContext.StepInfo.Text) + "\", \"level\": \"info" + "\"}}";
                ((IJavaScriptExecutor)webdriver).ExecuteScript(text);
            }
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scenarioContext, SeleniumHelper selenium, IWebDriver webdriver)
        {
            if (null != scenarioContext.TestError)
            {
                if (selenium.RodandoNoBrowserStack())
                    ((IJavaScriptExecutor)webdriver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\": \" Erro no cenario - " + FormatarTextoParaBrowserStack(scenarioContext.ScenarioInfo.Title) + " | " + FormatarTextoParaBrowserStack(scenarioContext.TestError.Message) + "\"}}");
            }
            else
            {
                if (selenium.RodandoNoBrowserStack())
                    ((IJavaScriptExecutor)webdriver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \" Sucesso no cenario - " + FormatarTextoParaBrowserStack(scenarioContext.ScenarioInfo.Title) + "\"}}");
            }

            selenium.Dispose();
        }

        //[AfterFeature]
        //public static void AfterFeature(SeleniumHelper selenium)
        //{
        //    selenium.Dispose();
        //}

        private string TextoStepDefinitionType(StepDefinitionType stepDefinitionType)
        {
            return stepDefinitionType switch
            {
                StepDefinitionType.Given => "Dado",
                StepDefinitionType.Then => "Então",
                StepDefinitionType.When => "Quando",
                _ => "",
            };
        }

        private string FormatarTextoParaBrowserStack(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder(capacity: normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var finalText = stringBuilder
                .ToString()
                .Normalize(NormalizationForm.FormC);

            return HttpUtility.JavaScriptStringEncode(finalText, false);
        }
    }
}