using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1_Vagas.Modelos;
using App1_Vagas.Banco;

namespace App1_Vagas.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroVaga : ContentPage
	{
		public CadastroVaga ()
		{
			InitializeComponent ();

		}

        private void SalvarAction(object sender, EventArgs e)
        {
            //TODO - Validar Dados do Cadastro
            Vaga vaga = new Vaga
            {
                NomeVaga = NomeVaga.Text,
                Quantidade = short.Parse(Quantidade.Text),
                Salario = double.Parse(Salario.Text),
                Empresa = Empresa.Text,
                Cidade = Cidade.Text,
                Descricao = Descricao.Text,
                TipoContratacao = (TipoContratacao.IsToggled) ? "PJ" : "CLT",
                Telefone = Telefone.Text,
                Email = Email.Text
            };

            DataBase database = new DataBase();
            database.Cadastro(vaga);


            App.Current.MainPage = new NavigationPage(new ConsultaVagas());
        }
    }
}