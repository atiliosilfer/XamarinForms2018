using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using App1_Vagas.Banco;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(App1_Vagas.iOS.Banco.Caminho))]
namespace App1_Vagas.iOS.Banco
{
    public class Caminho : ICaminho
    {
        public string ObterCaminho(string NomeArquivoBanco)
        {
            string caminhoDaPasta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string caminhoDaBiblioteca = Path.Combine(caminhoDaPasta, "..", "Library");
            string caminhoBanco = Path.Combine(caminhoDaBiblioteca, NomeArquivoBanco);
            return caminhoBanco;
        }
    }
}