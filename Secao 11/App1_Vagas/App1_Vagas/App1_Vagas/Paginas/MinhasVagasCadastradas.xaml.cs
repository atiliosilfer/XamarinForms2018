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
	public partial class MinhasVagasCadastradas : ContentPage
	{
        List<Vaga> Lista { get; set; }

        public MinhasVagasCadastradas ()
		{
			InitializeComponent ();
            ConsultarVagas();
        }

        private void ConsultarVagas()
        {
            DataBase database = new DataBase();
            ListaVagas.ItemsSource = database.Consultar();
            Lista = database.Consultar();

            lblCount.Text = Lista.Count.ToString();
        }

        private void EditarAction(object sender, EventArgs e)
        {
            Label lblEditar = (Label)sender;
            Vaga vaga = (Vaga)((TapGestureRecognizer)lblEditar.GestureRecognizers[0]).CommandParameter;
            Navigation.PushAsync(new EditarVaga(vaga));
        }

        private void ExcluirAction(object sender, EventArgs e)
        {
            Label lblExcluir = (Label)sender;
            Vaga vaga = (Vaga)((TapGestureRecognizer)lblExcluir.GestureRecognizers[0]).CommandParameter;

            DataBase database = new DataBase();
            database.Exclusao(vaga);
            ConsultarVagas();
        }

        private void PesquisarAction(object sender, TextChangedEventArgs e)
        {
            ListaVagas.ItemsSource = Lista.Where(a => a.NomeVaga.Contains(e.NewTextValue)).ToList();
        }
    }
}