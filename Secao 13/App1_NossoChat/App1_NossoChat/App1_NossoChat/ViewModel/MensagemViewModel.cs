using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using App1_NossoChat.Model;
using App1_NossoChat.Util;
using App1_NossoChat.Service;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace App1_NossoChat.ViewModel {
    class MensagemViewModel : INotifyPropertyChanged {

        //private StackLayout SL;
        public Command BtnEnviarCommand { get; set; }
        public Command AtualizarCommand { get; set; }
        private Chat chat;

        private bool _carregando;
        public bool Carregando {
            get { return _carregando; }
            set { _carregando = value; PropertyChanged(this, new PropertyChangedEventArgs("Carregando")); }
        }

        private List<Mensagem> _mensagens;
        public List<Mensagem> Mensagens {
            get { return _mensagens; }
            set {
                _mensagens = value;
                OnPropertyChanged("Mensagens");
                // if (_mensagens != null) {
                //     ShowOnScreen();
                // }
            }
        }

        private string _txtMensagem;
        public string TxtMensagem {
            get { return _txtMensagem; }
            set {
                _txtMensagem = value;
                OnPropertyChanged("TxtMensagem");
            }
        }

        public MensagemViewModel(Chat chat/*, StackLayout SLMensagemContainer*/) {
            this.chat = chat;
            //SL = SLMensagemContainer;
            Task.Run(() => Atualizar());
            BtnEnviarCommand = new Command(BtnEnviar);
            AtualizarCommand = new Command(AtualizarSemAsync);

            //Device.StartTimer(TimeSpan.FromSeconds(1), () => {
            //    Task.Run(() => AtualizarSemTelaCarregando());
            //    return true;
            //});
        }

        private void AtualizarSemAsync() {
            Task.Run(() => Atualizar());
        }

        private async Task Atualizar() {
            try {
                Carregando = true;
                Mensagens = await ServiceWS.GetMensagensChat(chat);
                Carregando = false;
            } catch (Exception e) {
                Carregando = false;
            }
        }

        private async Task AtualizarSemTelaCarregando() {
            Mensagens = await ServiceWS.GetMensagensChat(chat);
        }

        private void BtnEnviar() {
            var msg = new Mensagem() {
                id_usuario = UsuarioUtil.GetUsuarioLogado().id,
                mensagem = TxtMensagem,
                id_chat = chat.id,
            };
            ServiceWS.InsertMensagem(msg);
            Task.Run(() => Atualizar());
            TxtMensagem = string.Empty;
        }


        // public void ShowOnScreen() {
        //
        //     var usuario = UsuarioUtil.GetUsuarioLogado();
        //     SL.Children.Clear();
        //     foreach (var msg in Mensagens) {
        //         if (msg.usuario.id == usuario.id) {
        //             SL.Children.Add(CriarMensagemPropria(msg));
        //         } else {
        //             SL.Children.Add(CriarMensagemOutrosUsuarios(msg));
        //         }
        //     }
        // } 
        //        
        // private Xamarin.Forms.View CriarMensagemPropria(Mensagem mensagem) {
        //     var layout = new StackLayout() { Padding = 5, BackgroundColor = Color.FromHex("#5ED055"), HorizontalOptions = LayoutOptions.End };
        //     var label = new Label() { TextColor = Color.White, Text = mensagem.mensagem };
        //
        //     layout.Children.Add(label);
        //     return layout;
        // }
        //
        // private Xamarin.Forms.View CriarMensagemOutrosUsuarios(Mensagem mensagem) {
        //     var labelNome = new Label() { Text = mensagem.usuario.nome, FontSize = 10, TextColor = Color.FromHex("#5ED055") };
        //     var labelMensagem = new Label { Text = mensagem.mensagem, TextColor = Color.FromHex("#5ED055") };
        //
        //     var SL = new StackLayout();
        //
        //     SL.Children.Add(labelNome);
        //     SL.Children.Add(labelMensagem);
        //
        //     var frame = new Frame() { BorderColor = Color.FromHex("#5ED055"), CornerRadius = 0, HorizontalOptions = LayoutOptions.Start };
        //
        //     frame.Content = SL;
        //
        //     return frame;
        // }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}
