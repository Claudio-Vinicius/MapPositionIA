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
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public bool ConectaNorte { get; set; }
        public bool ConectaSul { get; set; }
        public bool ConectaLeste { get; set; }
        public bool ConectaOeste { get; set; }
        public TipoCelula Tipo { get; set; }
    }

    public enum TipoCelula
    {
        Inicio,
        Fim,
        Caminho
    }
}
