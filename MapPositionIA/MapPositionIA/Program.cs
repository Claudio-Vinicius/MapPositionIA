using MapPositionIA.Core;
using MapPositionIA.Model;
using System;

namespace MapPositionIA
{
    class Program
    {
        static void Main(string[] args)
        {
            var Mapa = new MapPosition();

            Mapa.AddLocal(0,0,false,true,false,false, TipoCelula.Inicio);
            Mapa.AddLocal(0, 1, false, true, true, true, TipoCelula.Caminho);
            Mapa.AddLocal(0,2,false,false,false,true, TipoCelula.Caminho);
            Mapa.AddLocal(1,0,false, true,true,false,TipoCelula.Caminho);
            Mapa.AddLocal(1,1,true,true,true,true, TipoCelula.Caminho);
            Mapa.AddLocal(1,2,false,false,false,true, TipoCelula.Caminho);
            Mapa.AddLocal(2,0,true, false, false, false, TipoCelula.Caminho);
            Mapa.AddLocal(2,1,true,true,false,false, TipoCelula.Caminho);
            Mapa.AddLocal(2,2,false,false,false,true, TipoCelula.Fim);
        }
    }
}
