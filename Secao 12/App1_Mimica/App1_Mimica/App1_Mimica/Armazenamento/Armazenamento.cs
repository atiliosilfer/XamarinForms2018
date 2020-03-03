using System;
using System.Collections.Generic;
using System.Text;
using App1_Mimica.Model;

namespace App1_Mimica.Armazenamento
{
    public class Armazenamento
    {
        public static Jogo Jogo { get; set; }

        public static short RodadaAtual { get; set; }

        public static string[][] Palavras =
        {
            // Fac. Pontuação 1 
            new string[] { "Olho", "Lingua", "Chinelo", "Milho", "Penalti", "Bola", "Ping-pong" },
            // Med. Pontuação 2
            new string[] { "Carpinteiro", "Amarelo", "Limão", "Abelha"},            
            // Dif. Pontuação 3
            new string[] { "Cisterna", "Lanterna", "Batman vs Superman", "Notebook"},

        };
    }
}
