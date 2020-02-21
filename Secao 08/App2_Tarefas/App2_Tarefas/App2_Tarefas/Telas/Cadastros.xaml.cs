using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App2_Tarefas.Modelo;

namespace App2_Tarefas.Telas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cadastros : ContentPage
	{
        private byte Prioridade { get; set; }
		public Cadastros ()
		{
			InitializeComponent ();
		}
        public void PrioridadeSelectAction (object sender, EventArgs args)
        {
            var Stacks = SLPrioridades.Children;
            foreach (var Stack in Stacks)
            {
                Label LblPrioridade = ((StackLayout)Stack).Children[1] as Label;
                LblPrioridade.TextColor = Color.Gray;
            }
            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            FileImageSource Source = ((Image)((StackLayout)sender).Children[0]).Source as FileImageSource;

            String Prioridade = Source.File.ToString().Replace(".png", "");

            this.Prioridade = byte.Parse(Prioridade);
        }

        public void SalvarAction(object sender, EventArgs args)
        {
            bool ErroExiste = false;
            //Verificando se os dois campos estão preenchidos. Para strings, verificando se o tamanho é maior que 0:
            if (TxtNome.Text == null || TxtNome.Text.Trim().Length <= 0)
            {
                ErroExiste = true;
                DisplayAlert("Erro", "Tarefa não digitada", "Okay");
            }

            //Já, para a prioridade, 
            if (Prioridade <= 0)
            {
                ErroExiste = true;
                DisplayAlert("Erro", "Prioridade não escolhida", "Okay");
            }

            if (ErroExiste == false)
            { 
                //Salvar esses dados
                Tarefas tarefa = new Tarefas();
                tarefa.Nome = TxtNome.Text.Trim();
                tarefa.Prioridade = this.Prioridade;

                new GerenciadorTarefa().Salvar(tarefa);

                App.Current.MainPage = new NavigationPage(new Inicio());
            }
        }
    }
}