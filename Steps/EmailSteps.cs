using Simple2u.Config;
using Simple2u.PageObjects;
using TechTalk.SpecFlow;

namespace Simple2u.Steps
{
    [Binding]
    public class EmailSteps
    {
        private readonly ConfigurationHelper _config;

        private readonly EmailPage emailPage;

        public EmailSteps(SeleniumHelper helper, ConfigurationHelper config)
        {
            _config = config;
            emailPage = new EmailPage(helper);
        }

        [Given(@"que eu abra o email")]
        public void Ir_Email() => emailPage.IrEmail(_config.Url("Email"));

        [Given(@"acesse o email com o usuario (.*) e com a senha (.*)")]
        public void Login_Email(string usuario, string senha)
        {
            if (usuario == "padrão" && senha == "padrão")
            {
                usuario = _config.UsuarioEmail;
                senha = _config.SenhaEmail;
            }
            emailPage.LoginEmail(usuario, senha);
        }

        [Given(@"pesquise o email (.*?)")]
        public void Pesquisar_Email(string pesquisa) => emailPage.PesquisarEmail(pesquisa);

        [Given(@"responda o email com (.*)")]
        public void Responder_Email(string resposta)
        {
            emailPage.SelecionarEmail();
            emailPage.ResponderEmail(resposta);
        }
    }
}