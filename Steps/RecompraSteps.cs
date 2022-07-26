using Simple2u.Config;
using Simple2u.Models;
using Simple2u.PageObjects;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class RecompraSteps
    {
        private readonly ConfigurationHelper _config;
        private readonly CodigoVerificacao _codigoVerificacao;
        private readonly CadastroPage cadastroPage;
        private readonly RecompraPage recompraPage;

        public RecompraSteps(SeleniumHelper helper, ConfigurationHelper config, CodigoVerificacao codigoVerificacao)
        {
            _config = config;
            _codigoVerificacao = codigoVerificacao;
            cadastroPage = new CadastroPage(helper);
            recompraPage = new RecompraPage(helper);
        }

        [Given(@"que eu confirme que tenho conta")]
        public void GivenQueEuConfirmeQueTenhoConta()
        {
            recompraPage.ConfirmarQueTenhoConta();
            cadastroPage.ClicarAvancar();
        }
        
        [Given(@"que eu selecione o cartão para recorrência")]
        public void GivenQueEuSelecioneOCartaoParaRecorrencia()
        {
            recompraPage.CartaoParaRecorrencia();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu insira a senha")]
        public void GivenQueEuInsiraASenha()
        {
            recompraPage.InserirSenha();
            recompraPage.ConfirmarSenha();
        }

        [Given(@"que eu insira o CPF já cadastrado (.*)")]
        public void GivenQueEuInsiraOCPFJaCadastrado(string cpfJaCadastrado)
        {
            recompraPage.InserirCpfJaCadastrado(cpfJaCadastrado);
            cadastroPage.ClicarAvancar();
        }


    }
}