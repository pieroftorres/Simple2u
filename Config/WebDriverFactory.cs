using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using Simple2u.Enums;
using System;
using System.Collections.Generic;
using System.IO;

namespace Simple2u.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(ConfigurationHelper configuration)
        {
            switch (configuration.Browser)
            {
                case Browser.Local:
                    ChromeOptions chromeOptions = new ChromeOptions();
                    //chromeOptions.AddArguments("--incognito"); --> Abre Guia Anônima
                    chromeOptions.AddUserProfilePreference("download.default_directory", Path.GetTempPath());
                    chromeOptions.BrowserVersion = "103.0.5060.5300";
                    chromeOptions.AddArgument("ignore-certificate-errors");
                    return new ChromeDriver(chromeOptions);

                case Browser.BSChrome:
                    ChromeOptions capabilities = new ChromeOptions();
                    capabilities.BrowserVersion = "103.0.5060.5300";

                    Dictionary<string, object> browserstackOptions = new Dictionary<string, object>();
                    browserstackOptions.Add("os", configuration.OS);
                    browserstackOptions.Add("osVersion", configuration.OSVersion);
                    browserstackOptions.Add("local", "false");
                    browserstackOptions.Add("seleniumVersion", "4.1.0");
                    browserstackOptions.Add("userName", configuration.UserName);
                    browserstackOptions.Add("accessKey", configuration.AccessKey);
                    browserstackOptions.Add("projectName", configuration.NomeProjeto);
                    browserstackOptions.Add("buildName", configuration.NomeSquad);
                    browserstackOptions.Add("debug", "true");
                    browserstackOptions.Add("networkLogs", "true");
                    browserstackOptions.Add("video", "true");
                    browserstackOptions.Add("seleniumLogs", "true");
                    browserstackOptions.Add("networkProfile", "4g-lte-advanced-good");
                    capabilities.AddAdditionalOption("bstack:options", browserstackOptions);

                    return new RemoteWebDriver(new Uri("https://hub-cloud.browserstack.com/wd/hub/"), capabilities);

                case Browser.BSSafari:
                    throw new NotSupportedException("Ainda não foi implementado a execução com esse navegador");

                case Browser.BSEdge:
                    throw new NotSupportedException("Ainda não foi implementado a execução com esse navegador");

                case Browser.BSFirefox:
                    throw new NotSupportedException("Ainda não foi implementado a execução com esse navegador");

                default:
                    throw new NotSupportedException("Sem browser para executar");
            }
        }
    }
}