using App1_NossoChat.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace App1_NossoChat.Service {
    public class ServiceWS {
        private static string EnderecoBase = "http://ws.spacedu.com.br/xf2018/rest/api";

        public static Usuario GetUsuario(Usuario usuario) {
            var URL = EnderecoBase + "/usuario";
            Usuario retorno = null;

            /*
             * QueryString: ?q=Footbal&tipo=imagem
             * StringContent param = new StringContent(string.Format("?nome={0}&password={1}",usuario.nome,usuario.password));
             */
            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string , string>("nome",usuario.nome),
                new KeyValuePair<string , string>("password",usuario.password)
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                var conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                retorno = JsonConvert.DeserializeObject<Usuario>(conteudo);
            }

            return retorno;
        }

        public static List<Chat> GetChats() {
            var URL = EnderecoBase + "/chats";
            List<Chat> lista = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                string conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (conteudo.Length > 2) {
                    lista = JsonConvert.DeserializeObject<List<Chat>>(conteudo);
                } 
            }
            return lista;
        }

        public static bool InsertChat(Chat chat) {
            var URL = EnderecoBase + "/chat";
            bool resp = false;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string , string>("nome",chat.nome)
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                resp = true;
            }
            return resp;
        }

        public static bool RenomearChat(Chat chat) {
            var URL = EnderecoBase + "/chat/" + chat.id;
            bool resp = false;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string , string>("nome",chat.nome)
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PutAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                resp = true;
            }
            return resp;
        }

        public static bool DeleteChat(Chat chat) {
            var URL = EnderecoBase + "/chat/delete/" + chat.id;
            bool resp = false;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.DeleteAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                resp = true;
            }
            return resp;
        }

        public static List<Mensagem> GetMensagensChat(Chat chat) {
            var URL = EnderecoBase + "/chat/" + chat.id + "/msg";
            List<Mensagem> lista = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.GetAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                string conteudo = resposta.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (conteudo.Length > 2) {
                    lista = JsonConvert.DeserializeObject<List<Mensagem>>(conteudo); ;
                }
            }

            return lista;
        }

        public static bool InsertMensagem (Mensagem mensagem) {
            var URL = EnderecoBase + "/chat/" + mensagem.id_chat + "/msg";
            bool resp = false;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string , string>("mensagem",mensagem.mensagem),
                new KeyValuePair<string , string>("id_usuario",mensagem.id_usuario.ToString())
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.PostAsync(URL, param).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                resp = true;
            }
            return resp;
        }

        public static bool DeleteMensagem(Mensagem mensagem) {
            var URL = EnderecoBase + "/chat/" + mensagem.id_chat + "/delete/" + mensagem.id;
            bool resp = false;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = requisicao.DeleteAsync(URL).GetAwaiter().GetResult();

            if (resposta.StatusCode == HttpStatusCode.OK) {
                resp = true;
            }
            return resp;
        }

    }
}