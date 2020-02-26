using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Cell.Modelo;

namespace App1.Cell.Pagina.Detalhe
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetailPage : ContentPage
	{
		public DetailPage (Funcionario funcionario)
		{
			InitializeComponent ();

            txtNome.Text = funcionario.Nome;
		}
	}
}