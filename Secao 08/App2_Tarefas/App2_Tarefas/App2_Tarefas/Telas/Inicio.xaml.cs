using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2_Tarefas.Modelo;
using System.Globalization;

namespace App2_Tarefas.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Inicio : ContentPage
	{
		public Inicio ()
		{
			InitializeComponent ();

            CultureInfo culture = new CultureInfo("pt-BR");
            string Data = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy", culture);
            DataHoje.Text = string.Format(Data, "de");

            CarregarTarefas();
        }

        public void ActionGoCadastro(object sender, EventArgs args)
        {
            Navigation.PushAsync(new Cadastros());
        }

        private void CarregarTarefas()
        {
            SLTarefas.Children.Clear();

            List<Tarefas> Lista = new GerenciadorTarefa().Listagem();

            int i = 0;
            foreach (Tarefas tarefa in Lista)
            {
                LinhaStackLayout(tarefa, i);
                i++;
            }
        }

        public void LinhaStackLayout(Tarefas tarefa, int index)
        {
            Image Delete = new Image() { HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("Delete.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Delete.Source = ImageSource.FromFile("Delete.png");
            }
            TapGestureRecognizer DeleteTap = new TapGestureRecognizer();
            DeleteTap.Tapped += delegate
            {
                new GerenciadorTarefa().Deletar(index);
                CarregarTarefas();
            };
            Delete.GestureRecognizers.Add(DeleteTap);


            Image Prioridade = new Image() { HorizontalOptions = LayoutOptions.EndAndExpand, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile(tarefa.Prioridade+".png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Prioridade.Source = ImageSource.FromFile(tarefa.Prioridade+".png");
            }

            View StackCentral = null;
            if (tarefa.DataFinalizacao == null)
            {
                StackCentral = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center, Text = tarefa.Nome };
            }
            else
            {
                StackCentral = new StackLayout() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0 };
                ((StackLayout)StackCentral).Children.Add(new Label() { Text=tarefa.Nome, TextColor = Color.Gray});

                ((StackLayout)StackCentral).Children.Add(new Label() { Text = "Finalizado em "+tarefa.DataFinalizacao.Value.ToString("dd/MM/yyyy - hh:mm") + "h", TextColor = Color.Gray, FontSize = 10 });
            }
            
            Image Check = new Image() { VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromFile("CheckOff.png") };
            if (Device.RuntimePlatform == Device.UWP)
            {
                Check.Source = ImageSource.FromFile("CheckOff.png");
            }
            if (tarefa.DataFinalizacao != null)
            {
                if (Device.RuntimePlatform == Device.UWP)
                {
                    Check.Source = ImageSource.FromFile("CheckOn.png");
                }
                Check.Source = ImageSource.FromFile("CheckOn.png");
            }

            TapGestureRecognizer CheckTap = new TapGestureRecognizer();
            CheckTap.Tapped += delegate
            {
                new GerenciadorTarefa().Finalizar(index, tarefa);
                CarregarTarefas();
            };
            Check.GestureRecognizers.Add(CheckTap);

            StackLayout Linha = new StackLayout(){Orientation = StackOrientation.Horizontal,Spacing = 15};

            Linha.Children.Add(Check);
            Linha.Children.Add(StackCentral);
            Linha.Children.Add(Prioridade);
            Linha.Children.Add(Delete);

            SLTarefas.Children.Add(Linha);
        }
    }
}