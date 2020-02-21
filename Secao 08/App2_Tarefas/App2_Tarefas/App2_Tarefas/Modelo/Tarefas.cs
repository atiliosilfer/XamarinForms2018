using System;
using System.Collections.Generic;
using System.Text;

namespace App2_Tarefas.Modelo
{
    public class Tarefas
    {
        public string Nome { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public byte Prioridade { get; set; }
    }
}
