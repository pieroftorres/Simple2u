using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
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

        public SeleniumHelper(IWebDriver driver)
        {
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
        public void Clicar(string xpath, int tempo = 30)
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
        public void Escrever(string xpath, string texto, int tempo = 30)
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
            string diretorioPrint = string.Format(@$"{Directory.GetCurrentDirectory()}/{nomePrint}_{DateTime.Now:dd-MM-yyyy_HH_mm_ss}.png");

            ((ITakesScreenshot)_driver).GetScreenshot().SaveAsFile(diretorioPrint);
            Console.WriteLine($"{msgConsole}: {new Uri(diretorioPrint)}");
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
        #endregion

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        public void GerenciarHorasOuVerba()
        {
            Actions action = new Actions(_driver);

            Clicar("//span[contains(text(), 'falecer vítima de algum acidente')]/../..//div[contains(@class, 'noUi-touch-area')]");   //("//div[contains(@id, 'l1-111_5-$b6')]/..//div[contains(@class, 'noUi-touch-area')]");  //("//div[contains(@class, 'noUi-touch-area')]");
            action.KeyDown(Keys.ArrowDown).Release().Build().Perform();
            Thread.Sleep(7000);
            Clicar("//span[contains(text(), 'falecer vítima de algum acidente')]/../..//div[contains(@class, 'noUi-touch-area')]");    //("//div[contains(@id, 'l1-111_5-$b6')]/..//div[contains(@class, 'noUi-touch-area')]");
            action.KeyDown(Keys.ArrowDown).Release().Build().Perform();
            Thread.Sleep(3000);
        }

        public void JSScript(string code)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript(code);
        }

        public void DuploClique(string elemento)
        {
            IWebElement clickable = _driver.FindElement(By.Id(elemento));
            new Actions(_driver)
                .DoubleClick(clickable)
                .Perform();
        }
    }
}