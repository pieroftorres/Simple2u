using Simple2u.Config;
using System;
using System.Threading;

namespace Simple2u.PageObjects
{
    public class CadastroPage : BasePage
    {
        public CadastroPage(SeleniumHelper helper) : base(helper) { }

        public void IrUrl(string url) => _helper.IrParaUrl(url);

        public void EsperarLoading() => _helper.AguardarLoading("//div[@data-block='ShowCase.IsLoading']");

        public void ClicarSimular() => _helper.Clicar("//button[contains(@class, 'btn-simular')]");

        public void ResponderPossuiConta(string resposta) => _helper.Clicar($"//span[contains(text(), '{resposta}')]");

        public void ClicarAvancar()
        {
            _helper.AguardarTotalCarregamento();
            _helper.Clicar("//span[contains(text(), 'Avançar')]");
        }

        public void ClicarAcidentesPessoais() => _helper.Clicar("//span[contains(text(), 'Acidentes Pessoais')]");

        public void InserirNome() => _helper.Escrever("//input[contains(@id, 'Input_Nome')]", "Lyllian Souza");

        public void InserirEmail() => _helper.Escrever("//input[contains(@id, 'Input_Email')]", "lyllian.souza@email.com");

        public void InserirDataNascimento() => _helper.Escrever("//input[contains(@id, 'input_dataNascimento')]", "05/05/2000");

        public void InserirProfissao()
        {
            _helper.Clicar("//div[contains(text(), 'Insira sua profissão')]");
            _helper.Clicar("//div[contains(@id, 'choices--b5-DropdownSelect-item-choice-42')]");
        }

        public void ClicarTipoSeguro(string respostaSeguro) => _helper.Clicar($"//span[contains(text(), '{respostaSeguro}')]");

        public void ClicarDiaDaSemana(string respostaDiaDaSemana) => _helper.Clicar($"//span[contains(text(), '{respostaDiaDaSemana}')]");

        public void InserirCelular() => _helper.Escrever("//input[contains(@placeholder, 'Insira seu celular')]", "11987654321");

        public void InserirCPF()
        {
            _helper.AguardarLoading("//input[contains(@placeholder, 'Insira seu CPF')]");
            _helper.Escrever("//input[contains(@placeholder, 'Insira seu CPF')]", GerarCpf());
        }

        public void ConcordarComTermos() => _helper.ExecutarScript("input.checkbox");

        public void EntariFrame() => _helper.EntrarIframe();

        public void SairiFrame() => _helper.SairDefaultIframe();

        public void InserirNumeroCartao() => _helper.Escrever("//input[contains(@placeholder, 'Número do Cartão')]", "5296627327435014");

        public void InserirTitularCartao() => _helper.Escrever("//input[contains(@placeholder, 'Titular do Cartão')]", "CAIO A GAMA");

        public void InserirMesAno() => _helper.Escrever("//input[contains(@placeholder, 'MM/AA')]", "1222");

        public void InserirCvc() => _helper.Escrever("//input[contains(@placeholder, 'CVC')]", "641");

        public void HorasSegurado() //_helper.Clicar($"//div[@aria-valuenow = '{horas}.0']");
        {
            _helper.Clicar("//div[contains(@class, 'noUi-touch-area')]");
            _helper.HorasSegurado();
        }
        public void ScrollarParaInvalidezTotalAcidente()
        {
            _helper.Scrollar("//span[contains(text(), 'Invalidez Permanente Total por Acidente')]");;
        }

        public void AVerNavios() => _helper.AVerNavios(3000);

        public void ClicarEnviar() => _helper.Clicar("//button[contains(text(), 'Enviar')]");

        public void NaoRecebiCodigoConfirmacao() => _helper.Clicar("//span[contains(text(), 'Não recebi o código')]");
        
        public static String GerarCpf()
        {
            int soma = 0, resto = 0;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            Random rnd = new Random();
            string semente = rnd.Next(100000000, 999999999).ToString();

            for (int i = 0; i < 9; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(semente[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            semente = semente + resto;
            return semente;
        }

        public void PegarToken()
        {
            _helper.AbrirMudarNovaAba();
            _helper.IrParaUrl("https://outlook.live.com/owa/");
            _helper.AguardarTotalCarregamento();
            _helper.Clicar("//a[contains(text(), 'Entrar')]");
            _helper.Escrever("//input[contains(@id, 'i0116')]", "t2m.ptorres@terceiros.mag.com.br");
            _helper.Clicar("//input[contains(@id, 'idSIButton9')]");
            _helper.Escrever("//input[contains(@id, 'i0118')]", "Mongeralaegon2022");
            _helper.Clicar("//input[contains(@id, 'idSIButton9')]");
            _helper.Clicar("//input[contains(@id, 'KmsiCheckboxField')]");
            _helper.Clicar("//input[contains(@id, 'idSIButton9')]");
            _helper.Clicar("//div[contains(@class, 'xfaRf')]");
            _helper.TesteToken();
            _helper.AVerNavios();
        }

        public void Teste()
        {
            _helper.AbrirMudarNovaAba();
            _helper.MudarAba("F0B575D04F4CB628906D3300697AA8C1");
            Thread.Sleep(5000);
        }

    }
}