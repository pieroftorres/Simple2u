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

    }
}