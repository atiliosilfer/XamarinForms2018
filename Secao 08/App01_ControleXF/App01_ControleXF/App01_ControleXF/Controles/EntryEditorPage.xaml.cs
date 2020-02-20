using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App01_ControleXF.Controles
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EntryEditorPage : ContentPage
	{
		public EntryEditorPage ()
		{
			InitializeComponent ();

            TxtIdade.TextChanged += delegate (object sender, TextChangedEventArgs args)
            {
                Lbl_Duplicado.Text = args.NewTextValue;
            };

            //Mostra quantidade de caracteres em no campo (Completed)
            TxtComentario.Completed += delegate (object sender, EventArgs args)
            {
                try
                {
                    LblQuantidadeCaracteres.Text = TxtComentario.Text.Length.ToString();
                }catch (Exception e) {
                    LblQuantidadeCaracteres.Text = "0" ;
                }
          
            };
		}
	}
}