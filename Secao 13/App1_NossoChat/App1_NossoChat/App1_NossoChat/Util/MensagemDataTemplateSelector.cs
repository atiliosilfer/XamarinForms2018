using App1_NossoChat.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App1_NossoChat.Util
{
    public class MensagemDataTemplateSelector : DataTemplateSelector {
        public DataTemplate MinhasMensagensTemplate { get; set; }
        public DataTemplate MensagensOutrasTemplates { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
            var Usuario = UsuarioUtil.GetUsuarioLogado();
            try {
                return ((Mensagem)item).id_usuario == Usuario.id ? MinhasMensagensTemplate : MensagensOutrasTemplates;
            } catch { return null; }
        }
    }
}
