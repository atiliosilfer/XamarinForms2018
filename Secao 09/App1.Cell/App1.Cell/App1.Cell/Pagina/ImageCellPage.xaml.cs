using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Cell.Modelo;

namespace App1.Cell.Pagina
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ImageCellPage : ContentPage
	{
		public ImageCellPage ()
		{
			InitializeComponent ();

            List<Funcionario> Lista = new List<Funcionario>();
            Lista.Add(new Funcionario() { Foto = "https://images.vexels.com/media/users/3/147102/isolated/lists/082213cb0f9eabb7e6715f59ef7d322a-icone-do-perfil-do-instagram.png", Nome = "José", Cargo = "Presidente da Empresa" });
            Lista.Add(new Funcionario() { Foto = "https://2.bp.blogspot.com/-_Cpv8suMlPw/UFiqaAVggjI/AAAAAAAAAGA/XtKB6fsMwmQ/s1600/foto-de-perfil-jerry.png", Nome = "Maria", Cargo = "Gerente de Vendas" });
            Lista.Add(new Funcionario() { Foto = "https://lh3.googleusercontent.com/proxy/3goH18LO-ochle0xE2YQRlu4V9xSIr5nE_nN7hUa8euwRJKutEOEWbwDyPcZOq9sIirRttOum29yPmwaOCvKFNDyGVw8xhO0H7X_SUcKO1AiPnb5PqyzpHf7_ZHJ1rOgDXxjUkjkYKGQaJ_xnw8W6yEY", Nome = "Elaine", Cargo = "Gerente de Marketing" });
            Lista.Add(new Funcionario() { Foto = "https://pt.seaicons.com/wp-content/uploads/2015/06/profile-icon.png", Nome = "Felipe", Cargo = "Entregador" });
            Lista.Add(new Funcionario() { Foto = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcTDIfCyrQYwEzEScQWKwvkJ_x8kSoPR5yipjrmUJG_P-JCDx3Qc", Nome = "João", Cargo = "Vendedor" });

            ListaFuncionario.ItemsSource = Lista;
        }
	}
}