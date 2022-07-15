using Simple2u.Config;
using Simple2u.Models;
using Simple2u.PageObjects;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class CadastroResidSteps
    {
        private readonly ConfigurationHelper _config;
        private readonly CodigoVerificacao _codigoVerificacao;
        private readonly CadastroPage cadastroPage;

        public CadastroResidSteps(SeleniumHelper helper, ConfigurationHelper config, CodigoVerificacao codigoVerificacao)
        {
            _codigoVerificacao = codigoVerificacao;
            _config = config;
            cadastroPage = new CadastroPage(helper);
        }

        
    }
}