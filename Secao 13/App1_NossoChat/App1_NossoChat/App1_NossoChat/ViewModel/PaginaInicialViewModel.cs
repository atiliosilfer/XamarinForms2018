using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using App1_NossoChat.Model;
using App1_NossoChat.Service;
using Newtonsoft.Json;
using App1_NossoChat.Util;

namespace App1_NossoChat.ViewModel {
    public class PaginaInicialViewModel : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _carregando;
        private string _Nome;
        private string _Senha;
        private string _Mensagem;

        public bool Carregando {
            get { return _carregando; }
            set { _carregando = value; PropertyChanged(this, new PropertyChangedEventArgs("Carregando")); }
        }
        public string Nome {
            get { return _Nome; }
            set { _Nome = value; PropertyChanged(this, new PropertyChangedEventArgs("Nome")); }
        }
        public string Senha {
            get { return _Senha; }
            set { _Senha = value; PropertyChanged(this, new PropertyChangedEventArgs("Senha")); }
        }
        public string Mensagem {
            get { return _Mensagem; }
            set { _Mensagem = value; PropertyChanged(this, new PropertyChangedEventArgs("Mensagem")); }
        }

        public Command AcessarCommand { get; set; }

        public PaginaInicialViewModel() {
            AcessarCommand = new Command(Acessar);
        }

        private async void Acessar() {
            Carregando = true;
            var user = new Usuario();
            user.nome = Nome;
            user.password = Senha;

            var usuarioLogado = await ServiceWS.GetUsuario(user);

            if (usuarioLogado == null) {
                Mensagem = "Senha/Usuario incorreto(a)";
                Carregando = false;

            } else {
                UsuarioUtil.SetUsuarioLogado(usuarioLogado);
                //App.Current.Properties["LOGIN"] = JsonConvert.SerializeObject(usuarioLogado);
                App.Current.MainPage = new NavigationPage(new View.Chats()) { BarBackgroundColor = Color.FromHex("#5ED055"), BarTextColor = Color.White };
            }

        }
    }
}
