using Simple2u.Config;
using Simple2u.PageObjects;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class CadastroSteps
    {
        private readonly ConfigurationHelper _config;

        private readonly CadastroPage cadastroPage;

        public CadastroSteps(SeleniumHelper helper, ConfigurationHelper config)
        {
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

        [Given(@"que eu clique em Acidentes Pessoais")]
        public void GivenQueEuCliqueEmAcidentesPessoais() => cadastroPage.ClicarAcidentesPessoais();

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
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu clique no dia (.*)")]
        public void GivenQueEuCliqueEm(string respostaDiaDaSemana) => cadastroPage.ClicarDiaDaSemana(respostaDiaDaSemana);

        [Given(@"que eu insira o Celular")]
        public void GivenQueEuInsiraOCelular()
        {
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
            cadastroPage.ScrollarParaInvalidezTotalAcidente();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu clique em Avançar")]
        public void GivenQueEuCliqueEmAvancar() => cadastroPage.ClicarAvancar();

        [Given(@"que eu fique A Ver Navios")]
        public void GivenQueEuFiqueAVerNavios() => cadastroPage.AVerNavios();

        [Given(@"que eu pegue o Token")]
        public void GivenQueEuPegueOToken()
        {
            cadastroPage.PegarToken();
        }

        [Given(@"que eu Teste")]
        public void GivenQueEuTeste()
        {
            cadastroPage.Teste();
        }

    }
}