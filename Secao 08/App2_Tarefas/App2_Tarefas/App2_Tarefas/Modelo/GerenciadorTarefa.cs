using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace App2_Tarefas.Modelo
{
    public class GerenciadorTarefa
    {
        private List<Tarefas> Lista { get; set; }

        public void Salvar (Tarefas tarefa)
        {
            Lista = Listagem();
            Lista.Add(tarefa);

            SalvarNoProperties(Lista);
        }

        public void Deletar(int index)
        {
            Lista = Listagem();

            Lista.RemoveAt(index);

            SalvarNoProperties(Lista);
        }

        public void Finalizar (int index, Tarefas tarefa)
        {
            Lista = Listagem();
            Lista.RemoveAt(index);

            tarefa.DataFinalizacao = DateTime.Now;
            Lista.Add(tarefa);
            SalvarNoProperties(Lista);
        }

        public List<Tarefas> Listagem()
        {
            return ListagemNoProperties();
        }

        private void SalvarNoProperties (List<Tarefas> Lista)
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                App.Current.Properties.Remove("Tarefas");
            }

            String JsonVal = JsonConvert.SerializeObject(Lista);

            App.Current.Properties.Add("Tarefas", JsonVal);
        }

        private List<Tarefas> ListagemNoProperties()
        {
            if (App.Current.Properties.ContainsKey("Tarefas"))
            {
                String JsonVal = (String)App.Current.Properties["Tarefas"];

                List<Tarefas> Lista = JsonConvert.DeserializeObject<List<Tarefas>>(JsonVal);

                return Lista;
            }
            return new List<Tarefas>();
        }
    }
}
