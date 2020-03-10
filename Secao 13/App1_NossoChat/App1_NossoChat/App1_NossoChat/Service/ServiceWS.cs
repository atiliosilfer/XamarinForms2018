using App1_NossoChat.Model;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace App1_NossoChat.Service {
    public class ServiceWS {
        private static string EnderecoBase = "http://ws.spacedu.com.br/xf2018/rest/api";

        public async static Task<Usuario> GetUsuario(Usuario usuario) {
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
            HttpResponseMessage resposta = await requisicao.PostAsync(URL, param);

            if (resposta.StatusCode == HttpStatusCode.OK) {
                var conteudo = await resposta.Content.ReadAsStringAsync();
                retorno = JsonConvert.DeserializeObject<Usuario>(conteudo);
            }

            return retorno;
        }

        public async static Task<List<Chat>> GetChats() {
            var URL = EnderecoBase + "/chats";
            List<Chat> lista = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.GetAsync(URL);

            if (resposta.StatusCode == HttpStatusCode.OK) {
                string conteudo = await resposta.Content.ReadAsStringAsync();
                if (conteudo != null) {
                    if (conteudo.Length > 2) {
                        lista = JsonConvert.DeserializeObject<List<Chat>>(conteudo);
                    }
                }
            }
            return lista;
        }

        public async static Task<bool> InsertChat(Chat chat) {
            var URL = EnderecoBase + "/chat";
            bool resp = false;

            FormUrlEncodedContent param = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string , string>("nome",chat.nome)
            });

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.PostAsync(URL, param);

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

        public async static Task<List<Mensagem>> GetMensagensChat(Chat chat) {
            var URL = EnderecoBase + "/chat/" + chat.id + "/msg";
            List<Mensagem> lista = null;

            HttpClient requisicao = new HttpClient();
            HttpResponseMessage resposta = await requisicao.GetAsync(URL);

            if (resposta.StatusCode == HttpStatusCode.OK) {
                string conteudo = await resposta.Content.ReadAsStringAsync();

                if (conteudo != null) {
                    if (conteudo.Length > 2) {
                        lista = JsonConvert.DeserializeObject<List<Mensagem>>(conteudo); ;
                    }
                }
            }

            return lista;
        }

        public static bool InsertMensagem(Mensagem mensagem) {
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