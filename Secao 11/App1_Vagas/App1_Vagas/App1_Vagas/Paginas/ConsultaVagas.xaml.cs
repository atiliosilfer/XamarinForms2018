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
	public partial class ConsultaVagas : ContentPage
	{
        List<Vaga> Lista { get; set; }

        public ConsultaVagas ()
		{
			InitializeComponent ();

            DataBase database = new DataBase();
            ListaVagas.ItemsSource = database.Consultar();
            Lista = database.Consultar();

            lblCount.Text = Lista.Count.ToString();

		}

        private void GoCadastro(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CadastroVaga());
        }

        private void GoMinhasVagas(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MinhasVagasCadastradas());
        }

        private void MaisDetalhe(object sender, EventArgs e)
        {
            Label lblDetalhe = (Label)sender;
            Vaga vaga = (Vaga)((TapGestureRecognizer)lblDetalhe.GestureRecognizers[0]).CommandParameter;
            Navigation.PushAsync(new DetalheVaga(vaga));

        }

        private void PesquisarAction(object sender, TextChangedEventArgs e)
        {
            ListaVagas.ItemsSource = Lista.Where(a => a.NomeVaga.Contains(e.NewTextValue)).ToList();
        }
    }
}