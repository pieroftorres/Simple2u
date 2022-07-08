using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Simple2u.Enums;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using Xunit;
using OpenQA.Selenium.Interactions;
using System.Threading;

namespace Simple2u.Config
{
    public class SeleniumHelper
    {
        private readonly IWebDriver _driver;
        private readonly Actions actions;
        private readonly WebDriverWait wait;
        private readonly ConfigurationHelper _config;

        public SeleniumHelper(IWebDriver driver, ConfigurationHelper config)
        {
            _config = config;
            _driver = driver;
            driver.Manage().Window.Position = new Point(-2000, 0);
            driver.Manage().Window.Maximize();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            actions = new Actions(_driver);
        }

        #region Navegar
        public string ObterUrl() => _driver.Url;

        public void IrParaUrl(string url) => _driver.Navigate().GoToUrl(url);

        public void ValidarUrl(string url)
        {
            var urlAtual = ObterUrl();
            Assert.True(urlAtual.Contains(url, StringComparison.InvariantCultureIgnoreCase), $"Teste foi redirecionado para a página errada. Página atual: {urlAtual}, path esperada: {url}");
        }

        public string NomeAbaAtual() => _driver.CurrentWindowHandle;

        public void MudarAba(string nomeAba) => _driver.SwitchTo().Window(nomeAba);

        public void AbrirMudarNovaAba()
        {
            var javascriptExecutor = (IJavaScriptExecutor)_driver;
            javascriptExecutor.ExecuteScript("window.open();");
            var novaAba = _driver.WindowHandles.Last();
            MudarAba(novaAba);
        }

        public void FechaAbaAtual()
        {
            var javascriptExecutor = (IJavaScriptExecutor)_driver;
            javascriptExecutor.ExecuteScript("window.close();");
        }

        public void EntrarIframe() => _driver.SwitchTo().Frame(0);

        public void SairPaiIframe() => _driver.SwitchTo().ParentFrame();

        public void SairDefaultIframe() => _driver.SwitchTo().DefaultContent();
        #endregion

        #region Procurar
        public IWebElement ProcurarElemento(string xpath, int tempo = 30)
        {
            IWebElement elemento;
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                elemento = _driver.FindElement(By.XPath(xpath));
            }
            catch (WebDriverTimeoutException)
            {
                Print($"ProcurarElementoErro", "Elemento não encontrado");
                throw new WebDriverException($"Não foi encontrado o elemento: {By.XPath(xpath)}");
            }
            return elemento;
        }

        public ReadOnlyCollection<IWebElement> ProcurarElementos(string xpath, int tempo = 5)
        {
            ReadOnlyCollection<IWebElement> elementos;
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xpath)));
                elementos = _driver.FindElements(By.XPath(xpath));
            }
            catch (WebDriverTimeoutException)
            {
                Print($"ProcurarElementosErro", "Elementos não encontrado");
                throw new WebDriverException($"Não foram encontrados os elementos: {By.XPath(xpath)}");
            }
            return elementos;
        }

        public void ValidarSeElementoNaoExiste(string xpath, int tempo = 5)
        {
            IWebElement elemento;
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            try
            {
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpath)));
                elemento = _driver.FindElement(By.XPath(xpath));
                Print("ValidarSeElementoNaoExisteErro", "Elemento encontrado");
            }
            catch (WebDriverTimeoutException)
            {
                elemento = null;
            }
            Assert.Null(elemento);
        }

        public void ValidarSeElementosNaoExistem(string xpath, int tempo = 5)
        {
            ReadOnlyCollection<IWebElement> elementos;
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            try
            {
                wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(xpath)));
                elementos = _driver.FindElements(By.XPath(xpath));
                Print("ValidarSeElementosNaoExistemErro", "Elementos encontrados");
            }
            catch (WebDriverTimeoutException)
            {
                elementos = null;
            }
            Assert.Null(elementos);
        }
        #endregion

        #region Mouse
        public void Clicar(string xpath, int tempo = 50)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
                    elemento.Click();
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    Print($"ClicarElementoErro", "Elemento não clicavel");
                    throw new WebDriverException($"Não foi possivel clicar no elemento: {elemento}");
                }
                catch (ElementClickInterceptedException ex)
                {
                    Print($"AcaoInterceptada", "Ação interceptada");
                    throw new ElementClickInterceptedException(ex.Message);
                }
                catch (StaleElementReferenceException ex)
                {
                    if (i == 5)
                    {
                        Print($"StaleElementReferenceExceptionErro", "Stale Element erro");
                        throw new StaleElementReferenceException($"{ex.Message}: {elemento}");
                    }
                }
            }
        }

        public void Scrollar(string xpath, int tempo = 5)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            actions.ScrollToElement(elemento).Perform();
        }
        #endregion

        #region Teclado
        public void Escrever(string xpath, string texto, int tempo = 50)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
                    elemento.SendKeys(texto);
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    Print($"EscreverElementoErro", "Elemento não escrevível");
                    throw new WebDriverException($"Não foi possivel escrever no elemento: {elemento}");
                }
                catch (ElementClickInterceptedException ex)
                {
                    Print($"AcaoInterceptada", "Ação interceptada");
                    throw new ElementClickInterceptedException(ex.Message);
                }
                catch (StaleElementReferenceException ex)
                {
                    if (i == 5)
                    {
                        Print($"StaleElementReferenceExceptionErro", "Stale Element erro");
                        throw new StaleElementReferenceException($"{ex.Message} :{elemento}");
                    }
                }
            }
        }

        public void LimparEscrever(string xpath, string texto, int tempo = 5)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
                    elemento.Click();
                    elemento.SendKeys(Keys.Control + "a");
                    elemento.SendKeys("\b");
                    elemento.SendKeys(texto);
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    Print($"EscreverElementoErro", "Elemento não escrevível");
                    throw new WebDriverException($"Não foi possivel escrever no elemento: {elemento}");
                }
                catch (ElementClickInterceptedException ex)
                {
                    Print($"AcaoInterceptada", "Ação interceptada");
                    throw new ElementClickInterceptedException(ex.Message);
                }
                catch (StaleElementReferenceException ex)
                {
                    if (i == 5)
                    {
                        Print($"StaleElementReferenceExceptionErro", "Stale Element erro");
                        throw new StaleElementReferenceException($"{ex.Message} :{elemento}");
                    }
                }
            }
        }

        public void Limpar(string xpath, int tempo = 5)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    wait.Until(ExpectedConditions.ElementToBeClickable(elemento));
                    elemento.Click();
                    elemento.SendKeys(Keys.Control + "a");
                    elemento.SendKeys("\b");
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    Print($"LimparElementoErro", "Elemento não clicavel");
                    throw new WebDriverException($"Não foi possivel limpar o que estava escrito no elemento: {elemento}");
                }
                catch (ElementClickInterceptedException ex)
                {
                    Print($"AcaoInterceptada", "Ação interceptada");
                    throw new ElementClickInterceptedException(ex.Message);
                }
                catch (StaleElementReferenceException ex)
                {
                    if (i == 5)
                    {
                        Print($"StaleElementReferenceExceptionErro", "Stale Element erro");
                        throw new StaleElementReferenceException($"{ex.Message} :{elemento}");
                    }
                }
            }
        }

        public void Enter(string xpath, int tempo = 50)
        {
            var elemento = ProcurarElemento(xpath, tempo);
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            elemento.SendKeys(Keys.Enter);
        }
        #endregion

        #region Loadings
        public void AguardarLoading(string xpath, int tempo = 30)
        {
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath(xpath)));
            }
            catch (WebDriverTimeoutException)
            {
                Print($"AguardarLoadingErro", "Loading não desapareceu");
                throw new WebDriverException($"Loading ultrapassou o limite esperado");
            }
        }
        #endregion

        #region Extras
        public void Print(string nomePrint, string msgConsole)
        {
            if (!RodandoNoBrowserStack())
            {
                string diretorioPrint = string.Format(@$"{Directory.GetCurrentDirectory()}/{nomePrint}_{DateTime.Now:dd-MM-yyyy_HH_mm_ss}.png");

                ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(diretorioPrint);
                Console.WriteLine($"{msgConsole}: {new Uri(diretorioPrint)}");
            }
        }

        public bool RodandoNoBrowserStack()
        {
            var browserStackBrowsers = new List<Browser>
            {
                Browser.BSSafari,
                Browser.BSChrome,
                Browser.BSEdge,
                Browser.BSFirefox
            };

            return browserStackBrowsers.Contains(_config.Browser);
        }

        public void AguardarTotalCarregamento(int tempo = 30)
        {
            wait.Timeout = TimeSpan.FromSeconds(tempo);
            wait.Until(driver => ((IJavaScriptExecutor)_driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        public void ExecutarScript(string cssSelector)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript($"let elemento = document.querySelector('{cssSelector}'); elemento.click();");
        }

        public void HorasSegurado()
        {
            Actions action = new Actions(_driver);
            action.KeyDown(Keys.ArrowUp).Release().Build().Perform();
            action.KeyDown(Keys.ArrowUp).Release().Build().Perform();
            action.KeyDown(Keys.ArrowUp).Release().Build().Perform();
        }

        public void AVerNavios(int milesimos = 50000) => Thread.Sleep(milesimos);

        public static String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        #endregion

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void TesteToken()
        {
            ProcurarElemento("//span[contains(@style, 'font-weight:bold;')]");
            var EspacoDoToken = _driver.FindElement(By.XPath("//span[contains(@style, 'font-weight:bold;')]"));
            var CapturarToken = EspacoDoToken.GetAttribute("span");

            FechaAbaAtual();

            Thread.Sleep(3000);

            Escrever("//input[contains(@id, 'Input_Cod1')]", CapturarToken);
            
            
            
            //var InserirToken = _driver.FindElement(By.XPath("//input[contains(@id, 'Input_Cod1')]"));
            //InserirToken.Clear();
            //InserirToken.SendKeys(CapturarToken);
        }
    }
}