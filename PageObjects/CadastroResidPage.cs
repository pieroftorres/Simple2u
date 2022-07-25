using Simple2u.Config;
using Simple2u.Generator;
using System;
using System.Threading;

namespace Simple2u.PageObjects
{
    public class CadastroResidPage : BasePage
    {
        public CadastroResidPage(SeleniumHelper helper) : base(helper) { }
        
        #region Residencial
        public void ResidencialInicio(string nome, string email)
        {
            _helper.Escrever("//input[contains(@id, 'Input_Nome')]", nome);
            _helper.Escrever("//input[contains(@id, 'Input_Email')]", email);
        }

        public void ResidencialStep2(string mensalOuPeriodo) => _helper.Clicar($"//span[contains(text(), '{mensalOuPeriodo}')]");

        public void ResidencialEndereco(string cep, string numero, string complemento)
        {
            _helper.Escrever("//input[contains(@placeholder, 'CEP')]", cep);
            _helper.Escrever("//input[contains(@placeholder, 'Número')]", numero);
            _helper.Escrever("//input[contains(@placeholder, 'Complemento')]", complemento);
        }

        public void ResidencialLocalizacao(string apartamentoCasaOuCondominio) => _helper.Clicar($"//div[contains(text(), '{apartamentoCasaOuCondominio}')]");

        public void ResidencialZonaRural(string SimOuNao) => _helper.Clicar($"//span[contains(text(), '{SimOuNao}')]");

        public void ResidencialTipoUso(string principalTemporadaComercial) => _helper.Clicar($"//div[contains(text(), '{principalTemporadaComercial}')]");

        public void ResidencialProprietario(string proprioOuAlugado) => _helper.Clicar($"//div[contains(text(), '{proprioOuAlugado}')]");

        public void TelefoneResid(string celular)
        {
            _helper.Escrever("//input[contains(@placeholder, 'Insira seu celular')]", celular);
        }

        public void EscolherPlano(string plano) => _helper.Clicar($"//span[contains(text(), '{plano}')]/../..//span[contains(text(), 'Escolher Plano')]");
        #endregion



    }
}