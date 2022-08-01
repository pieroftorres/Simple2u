using Simple2u.Config;
using Simple2u.Generator;
using System;
using System.Threading;

namespace Simple2u.PageObjects
{
    public class RecompraPage : BasePage
    {
        public RecompraPage(SeleniumHelper helper) : base(helper) { }

        public void ConfirmarQueTenhoConta() => _helper.Clicar("//label[contains(@for, 'RadioButton2-input')]");
        
        public void CartaoParaRecorrencia() => _helper.Clicar("//div[contains(@class, 'medCircle')]");

        public void InserirSenha() => _helper.Escrever("//input[contains(@type, 'password')]", "Simple2u");

        public void ConfirmarSenha() => _helper.Clicar("//button[contains(@type, 'button')]");

        public void InserirCpfJaCadastrado(string cpfJaCadastrado) => _helper.Escrever("//input[contains(@id, 'input_CPF')]", cpfJaCadastrado);

        public void ClicarEsqueciMinhaSenha() => _helper.Clicar("//span[contains(text(), 'Esqueci minha senha')]");

        public void EnviarChaveDeAcessoParaEmail() => _helper.Clicar("//img[contains(@src, '/img/site.mailicon.svg?l14ex4JdpAHsrf8V+Yp6lQ')]");

        public void ConfirmarExitoDaRecompra() => _helper.ProcurarElemento("//span[contains(text(), 'Agora você já tem créditos para ativar')]");

        public void SenhaCriadaOk() => _helper.Clicar("//span[contains(text(), 'OK')]");

        public void AvancarResumoSimulacao() => _helper.Clicar("//button[contains(@type, 'button')]");

        public void SuaSenhaPrecisaTer() => _helper.Clicar("//div[contains(text(), 'Sua senha precisa ter:')]");

        public void TentarConfirmarSenha() => _helper.DuploClique("//button[contains(@type, 'button')]");

        public void RecompraInserirProfissao()
        {
            _helper.Clicar("//div[contains(text(), 'Insira sua profissão')]");
            _helper.Clicar("//div[contains(@id, 'choices--b3-DropdownSelect-item-choice-43')]");
        }

    }
}