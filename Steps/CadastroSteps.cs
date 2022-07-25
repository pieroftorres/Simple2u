using Simple2u.Config;
using Simple2u.Models;
using Simple2u.PageObjects;
using System.Threading;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class CadastroSteps
    {
        private readonly ConfigurationHelper _config;
        private readonly CodigoVerificacao _codigoVerificacao;
        private readonly CadastroPage cadastroPage;

        public CadastroSteps(SeleniumHelper helper, ConfigurationHelper config, CodigoVerificacao codigoVerificacao)
        {
            _codigoVerificacao = codigoVerificacao;
            _config = config;
            cadastroPage = new CadastroPage(helper);
        }

        [Given(@"que eu acesse a url")]
        public void GivenQueEuAcesseAUrl() => cadastroPage.IrUrl(_config.Url("OutSystems"));

        [Given(@"que eu inicie a Simulação")]
        public void GivenQueEuCliqueSimularAgora() 
        {
            cadastroPage.EsperarLoading();
            cadastroPage.ClicarSimular();
        } 

        [Given(@"que eu responda com (.*) se tenho conta")]
        public void GivenQueEuRespondaSeTenhoConta(string resposta)
        {
            cadastroPage.ResponderPossuiConta(resposta);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu selecione o seguro (.*)")]
        public void GivenQueEuCliqueEmAcidentesPessoais(string respostaSeguro) => cadastroPage.EscolherEntrePessoalResidencialCeluar(respostaSeguro);

        [Given(@"que eu insira os Dados Pessoais")]
        public void GivenQueEuInsiraOsDadosPessoais()
        {
            cadastroPage.InserirNome();
            cadastroPage.InserirEmail();
            cadastroPage.InserirProfissao();
            cadastroPage.InserirDataNascimento();
            cadastroPage.ClicarAvancar();
        }
        
        [Given(@"que eu clique no seguro (.*)")]
        public void GivenQueEuCliqueNoSeguro(string respostaSeguro)
        {
            cadastroPage.ClicarTipoSeguro(respostaSeguro);
        }

        [Given(@"que eu clique no dia (.*)")]
        public void GivenQueEuCliqueEm(string respostaDiaDaSemana) => cadastroPage.ClicarDiaDaSemana(respostaDiaDaSemana);

        [Given(@"que eu selecione o período (.*). até (.*).")]
        public void GivenQueEuSelecioneOPeriodo(string dataInicio, string dataFim) => cadastroPage.SelecionarDataInicioEFim(dataInicio, dataFim);

        [Given(@"que eu insira o Celular")]
        public void GivenQueEuInsiraOCelular()
        {
            Thread.Sleep(1000);
            cadastroPage.InserirCelular();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu insira o CPF")]
        public void GivenQueEuInsiraOCpf()
        {
            cadastroPage.InserirCPF();
            cadastroPage.ConcordarComTermos();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu insira os Dados do Cartão")]
        public void GivenQueEuInsiraOsDadosDoCartao()
        {
            Thread.Sleep(2000);
            cadastroPage.EsperarLoading();
            cadastroPage.EntariFrame();
            cadastroPage.InserirNumeroCartao();
            cadastroPage.InserirTitularCartao();
            cadastroPage.InserirMesAno();
            cadastroPage.InserirCvc();
            cadastroPage.ClicarEnviar();
            cadastroPage.SairiFrame();
        }

        [Given(@"que eu selecione horas segurado")]
        public void GivenQueEuSelecioneHorasSegurado()
        {
            cadastroPage.HorasSegurado(); //(.*)
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu configure as Coberturas")]
        public void GivenQueEuConfigureAsCoberturas()
        {
            cadastroPage.PersonalizarSeguro();
            cadastroPage.ScrollarParaAssistencias();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu clique em Avançar")]
        public void GivenQueEuCliqueEmAvancar() => cadastroPage.ClicarAvancar();

        [Given(@"que eu insira o Código de Verificação")]
        public void GivenQueEuInsiraOCodigoDeVerificacao()
        {
            cadastroPage.InserirCodigoVerificacao(_codigoVerificacao.Codigo);
            cadastroPage.ClicarConfirmarCodigoVerificacao();
            /*for (int i = 1; i <= 4; i++)
            {
                //cadastroPage.InserirCodigoVerificacao(i, _codigoVerificacao.Codigo[i-1]);
                cadastroPage.InserirCodigoVerificacao(_codigoVerificacao.Codigo);
            }
            cadastroPage.ClicarConfirmarCodigoVerificacao();*/
        }

        [Given(@"que eu verifique se chegou a última página")]
        public void GivenQueEuVerifiqueSeChegouAUltimaPagina()
        {
            cadastroPage.VerificarSeChegouUltimaPagina();
        }

        [Given(@"que eu aguarde o Loading")]
        public void GivenQueEuAguardeOLoading() => cadastroPage.EsperarLoading();
    }
}