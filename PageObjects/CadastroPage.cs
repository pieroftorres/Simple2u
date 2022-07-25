using Simple2u.Config;
using Simple2u.Generator;
using System;
using System.Threading;

namespace Simple2u.PageObjects
{
    public class CadastroPage : BasePage
    {
        public CadastroPage(SeleniumHelper helper) : base(helper) { }


        #region Carregar Teste
        public void IrUrl(string url) => _helper.IrParaUrl(url);

        public void EsperarLoading() => _helper.AguardarLoading("//div[@data-block='ShowCase.IsLoading']");

        public void ClicarSimular() => _helper.Clicar("//button[contains(@class, 'btn-simular')]");
        #endregion

        #region Já Possui Conta
        public void ResponderPossuiConta(string resposta) => _helper.Clicar($"//span[contains(text(), '{resposta}')]");
        #endregion

        #region Simulação Inicial
        public void EscolherEntrePessoalResidencialCeluar(string respostaSeguro) => _helper.Clicar($"//span[contains(text(), '{respostaSeguro}')]");
        #endregion

        #region Vida Início
        public void ClicarAcidentesPessoais() => _helper.Clicar("//span[contains(text(), 'Acidentes Pessoais')]");

        public void InserirNome() => _helper.Escrever("//input[contains(@id, 'Input_Nome')]", "Paula Duarte");

        public void InserirEmail() => _helper.Escrever("//input[contains(@id, 'Input_Email')]", "paula.duarte@email.com");

        public void InserirDataNascimento() => _helper.Escrever("//input[contains(@id, 'input_dataNascimento')]", "06/05/2000");

        public void InserirProfissao()
        {
            _helper.Clicar("//div[contains(text(), 'Insira sua profissão')]");
            _helper.Clicar("//div[contains(@id, 'choices--b5-DropdownSelect-item-choice-42')]");
        }
        #endregion

        #region Vida Step 2
        public void ClicarTipoSeguro(string respostaSeguro) => _helper.Clicar($"//span[contains(text(), '{respostaSeguro}')]");
        #endregion

        #region Vida Dia a Dia Step 1
        public void ClicarDiaDaSemana(string respostaDiaDaSemana) => _helper.Clicar($"//span[contains(text(), '{respostaDiaDaSemana}')]");
        #endregion

        #region Vida Dia a Dia Step 2
        public void HorasSegurado() //_helper.Clicar($"//div[@aria-valuenow = '{horas}.0']");
        {
            _helper.Clicar("//div[contains(@class, 'noUi-touch-area')]");
            _helper.HorasSegurado();
        }
        #endregion

        #region Vida Período
        public void SelecionarDataInicioEFim(string dataInicio, string dataFim)
        {
            _helper.Clicar($"//button[contains(@aria-label, '{dataInicio}.')]");
            _helper.Clicar($"//button[contains(@aria-label, '{dataFim}.')]");
            _helper.Clicar("//span[contains(text(), 'Confirmar')]");
        }
        #endregion

        #region Telefone Tipo Produto AP
        public void InserirCelular() => _helper.Escrever("//input[contains(@id, 'input_telefone')]", "11987654321");
        #endregion

        #region Coberturas Plano Personalizado
        public void PersonalizarSeguro()
        {
            _helper.GerenciarHorasOuVerba();
            Thread.Sleep(3000);
        }

        public void ScrollarParaAssistencias() => _helper.Scrollar("//span[contains(text(), 'Assistências')]");

        public void CoberturasExtras()
        {
            _helper.Clicar("//input[contains(@class, 'switch')]");
            _helper.Clicar("//input[contains(@id, 'l2-148_5-Switch2')]");
            Thread.Sleep(2000);
        }
        #endregion

        #region Vida Contratação CPF
        public void InserirCPF()
        {
            _helper.AguardarLoading("//input[contains(@placeholder, 'Insira seu CPF')]");
            _helper.Escrever("//input[contains(@placeholder, 'Insira seu CPF')]", RandomGenerator.RandomCpf());
        }

        public void ConcordarComTermos() => _helper.ExecutarScript("input.checkbox");
        #endregion

        #region Cartão Pagamento
        public void EntariFrame()
        {
            Thread.Sleep(2000);
            _helper.EntrarIframe();
        }

        public void SairiFrame() => _helper.SairDefaultIframe();

        public void InserirNumeroCartao() => _helper.Escrever("//input[contains(@placeholder, 'Número do Cartão')]", "5296627327435014");

        public void InserirTitularCartao() => _helper.Escrever("//input[contains(@placeholder, 'Titular do Cartão')]", "CAIO A GAMA");

        public void InserirMesAno() => _helper.Escrever("//input[contains(@placeholder, 'MM/AA')]", "1222");

        public void InserirCvc()
        {
            _helper.Escrever("//input[contains(@placeholder, 'CVC')]", "641");
            Thread.Sleep(3000);
        }
        #endregion

        #region Extras

        public void ClicarEnviar() => _helper.Clicar("//button[contains(text(), 'Enviar')]");

        public void NaoRecebiCodigoConfirmacao() => _helper.Clicar("//span[contains(text(), 'Não recebi o código')]"); 
        
        public void ClicarAvancar()
        {
            _helper.AguardarTotalCarregamento();
            _helper.Clicar("//span[contains(text(), 'Avançar')]");
        }
        #endregion

        public void InserirCodigoVerificacao(string codigoVerificacao) => _helper.Escrever($"//input[contains(@id, 'b3-Input_Cod1')]", codigoVerificacao.ToString());

        public void ClicarConfirmarCodigoVerificacao() => _helper.Clicar("//span[contains(text(), 'Confirmar')]");

        public void VerificarSeChegouUltimaPagina()
        {
            //_helper.AguardarLoading("//div[contains(@class, 'container-ana-text-size-customized')]");
            Thread.Sleep(15000);
            _helper.ValidarUrl("Parabens");
        }
    }
}