using Simple2u.Config;
using System;

namespace Simple2u.PageObjects
{
    public class EmailPage : BasePage
    {
        public EmailPage(SeleniumHelper helper) : base(helper) { }

        public void IrEmail(string url) => _helper.IrParaUrl(url);

        public void LoginEmail(string usuario, string senha)
        {
            _helper.Escrever("//input[@id='i0116']", usuario);
            _helper.Clicar("//input[@id='idSIButton9']");
            _helper.Escrever("//input[@id='i0118']", senha);
            _helper.Clicar("//input[@id='idSIButton9']");
            _helper.Clicar("//input[@id='idBtn_Back']", 30);
        }

        public void PesquisarEmail(string pesquisa)
        {
            _helper.AguardarTotalCarregamento();
            _helper.LimparEscrever("//div[@id='searchBoxId-Mail']//input", pesquisa);
            _helper.Enter("//div[@id='searchBoxId-Mail']//input");
        }

        public void SelecionarEmail()
        {
            _helper.AguardarLoading("//span[contains(text(), 'Pesquisando')]");
            _helper.AguardarTotalCarregamento();
            for (int i = 0; i <= 16; i++)
            {
                try
                {
                    _helper.ProcurarElemento("//div[@role='option' and contains(@aria-label, 'Não lidos')]/div[@draggable='true']", 15);
                    break;
                }
                catch (Exception)
                {
                    _helper.Clicar("//button[@aria-label='Pesquisar']");
                }
            }
            _helper.Clicar("//div[@role='option' and contains(@aria-label, 'Não lidos')]/div[@draggable='true']");
            _helper.Clicar("//div[@role='option' and contains(@aria-label, 'Não lidos')]/div[@draggable='true']");
        }

        public void ResponderEmail(string resposta)
        {
            _helper.Clicar("//button[@aria-label='Responder']/span/span");
            _helper.AguardarTotalCarregamento();
            _helper.Escrever("//div[contains(@id, 'virtualEditScroller')]/div[@aria-label='Corpo da mensagem']", resposta);
            _helper.Clicar("//button[@aria-label='Enviar']");
        }
    }
}