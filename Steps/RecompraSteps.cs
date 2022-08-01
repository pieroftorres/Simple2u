using Simple2u.Config;
using Simple2u.Models;
using Simple2u.PageObjects;
using System.Threading;
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
            recompraPage.ConfirmarSenha();
        }

        [Given(@"que eu insira o CPF já cadastrado (.*)")]
        public void GivenQueEuInsiraOCPFJaCadastrado(string cpfJaCadastrado)
        {
            recompraPage.InserirCpfJaCadastrado(cpfJaCadastrado);
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu insira a data de nascimento e a profissão")]
        public void GivenQueEuInsiraADataDeNascimentoEAProfissao()
        {
            recompraPage.RecompraInserirProfissao();
            cadastroPage.InserirDataNascimento();
            cadastroPage.ClicarAvancar();
        }

        [Given(@"que eu solicite redefinição de senha")]
        public void GivenQueEuSoliciteRedefinicaoDeSenha()
        {
            recompraPage.ClicarEsqueciMinhaSenha();
            recompraPage.EnviarChaveDeAcessoParaEmail();
        }

        [Given(@"que eu crie uma senha")]
        public void GivenQueEuCrieUmaSenha()
        {
            recompraPage.InserirSenha();
            recompraPage.SuaSenhaPrecisaTer();
            cadastroPage.ClicarAvancar();
            Thread.Sleep(15000);
            
        }

        [Given(@"que eu confirme a mensagem de senha criada")]
        public void GivenQueEuConfirmeAMensagemDeSenhaCriada()
        {
            Thread.Sleep(2000);
            recompraPage.SenhaCriadaOk();
            recompraPage.SenhaCriadaOk();
        }

        [Given(@"que eu confirme o êxito da recompra")]
        public void GivenQueEuConfirmeOExitoDaRecompra() => recompraPage.ConfirmarExitoDaRecompra();

        [Given(@"que eu avance com o resumo da simulação")]
        public void GivenQueEuAvanceComOResumoDaSimulacao()
        {
            Thread.Sleep(2000);
            recompraPage.AvancarResumoSimulacao();
        }

        [Given(@"que eu tente confirmar a senha")]
        public void GivenQueEuTenteConfirmarASenha() => recompraPage.TentarConfirmarSenha();
    }
}