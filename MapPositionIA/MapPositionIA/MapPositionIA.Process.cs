using MapPositionIA.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MapPositionIA.Core
{
    class MapPosition
    {
        public Mapa MapaLocal { get; set; }

        public MapPosition()
        {
            this.MapaLocal = new Mapa();
            this.MapaLocal.Celulas = new List<Celula>();
        }

        public void AddLocal(int linha, int coluna, bool CN, bool CS, bool CL, bool CO, TipoCelula tipo)
        {
            var Posicao = new Celula();
            Posicao.Linha = linha;
            Posicao.Coluna = coluna;
            Posicao.ConectaNorte = CN;
            Posicao.ConectaSul = CS;
            Posicao.ConectaLeste = CL;
            Posicao.ConectaOeste = CO;
            Posicao.Tipo = tipo;

            this.MapaLocal.Celulas.Add(Posicao);
        }
    }
}
