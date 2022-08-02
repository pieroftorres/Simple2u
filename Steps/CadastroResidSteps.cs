using Simple2u.Config;
using Simple2u.Models;
using Simple2u.PageObjects;
using System.Threading;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class CadastroResidSteps
    {
        private readonly ConfigurationHelper _config;
        private readonly CodigoVerificacao _codigoVerificacao;
        private readonly CadastroPage cadastroPage;
        private readonly CadastroResidPage cadastroResidPage;

        public CadastroResidSteps(SeleniumHelper helper, ConfigurationHelper config, CodigoVerificacao codigoVerificacao)
        {
            _codigoVerificacao = codigoVerificacao;
            _config = config;
            cadastroPage = new CadastroPage(helper);
            cadastroResidPage = new CadastroResidPage(helper);
        }

        [Given(@"que eu insira Nome '(.*)' e Email '(.*)'")]
        public void GivenQueEuInsiraNomeEEmail(string nome, string email)
        {
            cadastroResidPage.ResidencialInicio(nome, email);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu proteja o imóvel com (.*)")]
        public void GivenQueEuProtejaOImovelCom(string mensalOuPeriodo) => cadastroResidPage.ResidencialStep2(mensalOuPeriodo);

        [Given(@"que eu informe no endereço o CEP (.*), o Número (.*) e o Complemento (.*)")]
        public void GivenQueEuInformeNoEnderecoCEPNumeroComplemento(string cep, string numero, string complemento)
        {
            Thread.Sleep(2000);
            cadastroResidPage.ResidencialEndereco(cep, numero, complemento);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu selecione o imóvel (.*)")]
        public void GivenQueEuSelecioneOImovel(string apartamentoCasaOuCondominio) => cadastroResidPage.ResidencialLocalizacao(apartamentoCasaOuCondominio);

        [Given(@"que eu informe se o imóvel está localizado em zona rural: (.*)")]
        public void GivenQueEuInformeSeImovelEstaZonaRural(string SimOuNao)
        {
            cadastroResidPage.ResidencialZonaRural(SimOuNao);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu informe que o imóvel segurado é para (.*)")]
        public void GivenQueEuInformeQueOImovelEPara(string principalTemporadaComercial) => cadastroResidPage.ResidencialTipoUso(principalTemporadaComercial);

        [Given(@"que eu informe que o imóvel é (.*)")]
        public void GivenQueEuInformeQueOImovelE(string proprioOuAlugado) => cadastroResidPage.ResidencialProprietario(proprioOuAlugado);

        [Given(@"que eu informe que o Celular é (.*)")]
        public void GivenQueEuInformeOCelular(string celular)
        {
            cadastroResidPage.TelefoneResid(celular);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu escolha a assinatura (.*)")]
        public void GivenQueEuEscolhaAAssinatura(string plano) => cadastroResidPage.EscolherPlano(plano);


    }
}