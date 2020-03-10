using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using App1_NossoChat.Model;
using App1_NossoChat.Service;
using System.ComponentModel;
using System.Threading.Tasks;

namespace App1_NossoChat.ViewModel {
    public class CadastrarChatViewModel : INotifyPropertyChanged {
        public Command CadastrarCommand { get; set; }

        private bool _carregando;
        public bool Carregando {
            get { return _carregando; }
            set {
                _carregando = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Carregando"));
            }
        }

        public string nome { get; set; }
        public string mensagem {
            get { return _mensagem; }
            set {
                _mensagem = value;
                OnPropertyChanged("mensagem");
            }
        }

        private string _mensagem { get; set; }

        public CadastrarChatViewModel() {
            CadastrarCommand = new Command(CadastrarButton);
        }

        private void CadastrarButton() {
            bool resultado = Task.Run(() => Cadastrar()).GetAwaiter().GetResult();

            if (resultado == true) {
                var PaginaAtual = ((NavigationPage)App.Current.MainPage);
                PaginaAtual.PopAsync();
            }
        }

        private async Task<bool> Cadastrar() {
            Carregando = true;
            try {
                var chat = new Chat() { nome = nome };
                bool ok = await ServiceWS.InsertChat(chat);
                if (ok) {
                    var PaginaAtual = ((NavigationPage)App.Current.MainPage);

                    var Chats = (View.Chats)PaginaAtual.RootPage;
                    var ViewModel = (ChatsViewModel)Chats.BindingContext;
                    if (ViewModel.AtualizarCommand.CanExecute(null)) {
                        ViewModel.AtualizarCommand.Execute(null);
                    }
                    return true;
                } else {
                    mensagem = "Ocorreu um erro no cadastro";
                    Carregando = false;
                    return true;
                }
            } catch (Exception e) {
                mensagem = e.Message;
                Carregando = false;
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
