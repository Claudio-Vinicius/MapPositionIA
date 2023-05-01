using System;
using System.Collections.Generic;

namespace MapPositionIA.Model
{
    public class Mapa
    {
        public List<Celula> Celulas { get; set; }
    }
    public class Celula
    {
        public Coordenada Posicao { get; set; }
        public bool ConectaNorte { get; set; }
        public bool ConectaSul { get; set; }
        public bool ConectaLeste { get; set; }
        public bool ConectaOeste { get; set; }
        public TipoCelula Tipo { get; set; }
    }

    public class  Coordenada 
    {
        public Coordenada()
        {

        }
        public Coordenada(int Linha, int Coluna)
        {
            this.Linha = Linha;
            this.Coluna = Coluna;
        }
        public int Linha { get; set; }
        public int Coluna { get; set; }
    }

    

    public enum TipoCelula
    {
        Inicio,
        Fim,
        Caminho
    }
}
