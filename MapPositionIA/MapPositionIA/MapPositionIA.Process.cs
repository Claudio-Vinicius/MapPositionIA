using MapPositionIA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void AddLocal(int linha, int coluna, bool CN, bool CL, bool CS, bool CO, TipoCelula tipo)
        {
            var MapLocation = new Celula();
            MapLocation.Posicao = new Coordenada();
            MapLocation.Posicao.Linha = linha;
            MapLocation.Posicao.Coluna = coluna;
            MapLocation.ConectaNorte = CN;
            MapLocation.ConectaSul = CS;
            MapLocation.ConectaLeste = CL;
            MapLocation.ConectaOeste = CO;
            MapLocation.Tipo = tipo;

            this.MapaLocal.Celulas.Add(MapLocation);
        }

        public List<Coordenada> EncontraCaminho(Mapa mapa, Celula Origem, Celula Destino) 
        {
            
            var ArvoreCaminhos = ControiArvoreCaminhos(mapa);

            var Historico = new List<Coordenada>();
            MontaCaminho(ArvoreCaminhos, Origem, Destino, Historico);

            Historico.Reverse();

            return Historico;
        }

        private void MontaCaminho(TreeNode<Celula> arvoreCaminhos, Celula origem, Celula destino, List<Coordenada> historico)
        {
            var TreeNodeOrigem = EncontraTreeNode(arvoreCaminhos, origem);

            EncontraTreeNodeComHistorico(TreeNodeOrigem, destino, historico);

            historico.Add(origem.Posicao);
        }

        private bool EncontraTreeNodeComHistorico(TreeNode<Celula> treeNodeOrigem, Celula destino, List<Coordenada> historico)
        {
            if (treeNodeOrigem.Data == destino)
            {
                historico.Add(treeNodeOrigem.Data.Posicao);
                return true;
            }
            else
            {
                foreach (var Node in treeNodeOrigem.Children)
                {
                    if (Node.Data == destino)
                    {
                        historico.Add(Node.Data.Posicao);
                        return true;
                    }
                    else
                    {
                        var result = EncontraTreeNodeComHistorico(Node, destino, historico);
                        if (result == true)
                        {
                            historico.Add(Node.Data.Posicao);
                            return true;
                        }
                    }
                }
                return false;
            }
                
            
            
        }

        private TreeNode<Celula> EncontraTreeNode(TreeNode<Celula> arvoreCaminhos, Celula predicado)
        {
            TreeNode<Celula> result = null;

            if (arvoreCaminhos.Data == predicado)
                return arvoreCaminhos;

            foreach (var Node in arvoreCaminhos.Children)
            {
                if (Node.Data == predicado) return Node;
                else
                {
                    result = EncontraTreeNode(Node, predicado);
                }
            }
            return result;
        }

        private TreeNode<Celula> ControiArvoreCaminhos(Mapa mapa)
        {
            var CelulaInicial = mapa.Celulas.FirstOrDefault(x => x.Tipo == TipoCelula.Inicio);

            TreeNode<Celula> Root = new TreeNode<Celula>(CelulaInicial);

            BuscaSubCaminhos(Root, mapa.Celulas);

            return Root;

        }

        private void BuscaSubCaminhos(TreeNode<Celula> PosicalAtual, List<Celula> celulas)
        {

            if (PosicalAtual.Data.ConectaSul)
            {
                var PosicaoSulColuna = PosicalAtual.Data.Posicao.Coluna;
                var PosicaoSulLinha = PosicalAtual.Data.Posicao.Linha + 1;

                var CoordenadaSul = new Coordenada(PosicaoSulLinha, PosicaoSulColuna);
                var CelulaSul = celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaSul.Linha && x.Posicao.Coluna == CoordenadaSul.Coluna);

                if(PosicalAtual.Parent == null)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaSul);
                    BuscaSubCaminhos(chilNode, celulas);
                }
                else if (CelulaSul != PosicalAtual.Parent.Data)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaSul);
                    BuscaSubCaminhos(chilNode, celulas);
                }
            }

            if (PosicalAtual.Data.ConectaNorte)
            {
                var PosicaoNorteColuna = PosicalAtual.Data.Posicao.Coluna;
                var PosicaoNorteLinha = PosicalAtual.Data.Posicao.Linha - 1;

                var CoordenadaNorte = new Coordenada(PosicaoNorteLinha, PosicaoNorteColuna);
                var CelulaNorte = celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaNorte.Linha && x.Posicao.Coluna == CoordenadaNorte.Coluna);

                if (PosicalAtual.Parent == null)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaNorte);
                    BuscaSubCaminhos(chilNode, celulas);
                }
                else if (CelulaNorte != PosicalAtual.Parent.Data)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaNorte);
                    BuscaSubCaminhos(chilNode, celulas);
                }

            }

            if (PosicalAtual.Data.ConectaLeste)
            {
                var PosicaoLesteColuna = PosicalAtual.Data.Posicao.Coluna + 1;
                var PosicaoLesteLinha = PosicalAtual.Data.Posicao.Linha;

                var CoordenadaLeste = new Coordenada(PosicaoLesteLinha, PosicaoLesteColuna);
                var CelulaLeste = celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaLeste.Linha && x.Posicao.Coluna == CoordenadaLeste.Coluna);

                if (PosicalAtual.Parent == null)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaLeste);
                    BuscaSubCaminhos(chilNode, celulas);
                }
                else if (CelulaLeste != PosicalAtual.Parent.Data)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaLeste);
                    BuscaSubCaminhos(chilNode, celulas);
                }

            }

            if (PosicalAtual.Data.ConectaOeste)
            {
                var PosicaoOesteColuna = PosicalAtual.Data.Posicao.Coluna - 1;
                var PosicaoOesteLinha = PosicalAtual.Data.Posicao.Linha;

                var CoordenadaOeste = new Coordenada(PosicaoOesteLinha, PosicaoOesteColuna);
                var CelulaOeste = celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaOeste.Linha && x.Posicao.Coluna == CoordenadaOeste.Coluna);

                if (PosicalAtual.Parent == null)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaOeste);
                    BuscaSubCaminhos(chilNode, celulas);
                }
                else if (CelulaOeste != PosicalAtual.Parent.Data)
                {
                    var chilNode = PosicalAtual.AddChild(CelulaOeste);
                    BuscaSubCaminhos(chilNode, celulas);
                }
            }
            
        }

        private List<Coordenada> BuscaCelulaComHistorico(Mapa mapa, Celula PosicaoAtual, Celula Destino, List<Coordenada> historico)
        {
            if(PosicaoAtual.Posicao.Linha == Destino.Posicao.Linha && PosicaoAtual.Posicao.Coluna == Destino.Posicao.Coluna)
            {
                historico.Add(PosicaoAtual.Posicao);
                return historico;
            }

            if(PosicaoAtual.ConectaSul)
            {
                var PosicaoSulColuna = PosicaoAtual.Posicao.Coluna;
                var PosicaoSulLinha = PosicaoAtual.Posicao.Linha + 1;

                var CoordenadaSul = new Coordenada(PosicaoSulLinha, PosicaoSulColuna);
                var CelulaSul = mapa.Celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaSul.Linha && x.Posicao.Coluna == CoordenadaSul.Coluna);

                var result = BuscaCelulaComHistorico(mapa, CelulaSul, Destino, historico);
                if (result != null)
                    historico.Add(CoordenadaSul);
                return historico;
            }

            if (PosicaoAtual.ConectaNorte)
            {
                var PosicaoNorteColuna = PosicaoAtual.Posicao.Coluna;
                var PosicaoNorteLinha = PosicaoAtual.Posicao.Linha - 1;

                var CoordenadaNorte = new Coordenada(PosicaoNorteLinha, PosicaoNorteColuna);
                var CelulaNorte = mapa.Celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaNorte.Linha && x.Posicao.Coluna == CoordenadaNorte.Coluna);

                var result = BuscaCelulaComHistorico(mapa, CelulaNorte, Destino, historico);
                if (result != null)
                    historico.Add(CoordenadaNorte);
                return historico;
            }

            if (PosicaoAtual.ConectaLeste)
            {
                var PosicaoLesteColuna = PosicaoAtual.Posicao.Coluna;
                var PosicaoLesteLinha = PosicaoAtual.Posicao.Linha + 1;

                var CoordenadaLeste = new Coordenada(PosicaoLesteLinha, PosicaoLesteColuna);
                var CelulaLeste = mapa.Celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaLeste.Linha && x.Posicao.Coluna == CoordenadaLeste.Coluna);

                var result = BuscaCelulaComHistorico(mapa, CelulaLeste, Destino, historico);
                if (result != null)
                    historico.Add(CoordenadaLeste);
                return historico;
            }

            if (PosicaoAtual.ConectaOeste)
            {
                var PosicaoOesteColuna = PosicaoAtual.Posicao.Coluna;
                var PosicaoOesteLinha = PosicaoAtual.Posicao.Linha - 1;

                var CoordenadaOeste = new Coordenada(PosicaoOesteLinha, PosicaoOesteColuna);
                var CelulaOeste = mapa.Celulas.FirstOrDefault(x => x.Posicao.Linha == CoordenadaOeste.Linha && x.Posicao.Coluna == CoordenadaOeste.Coluna);

                var result = BuscaCelulaComHistorico(mapa, CelulaOeste, Destino, historico);
                if (result != null)
                    historico.Add(CoordenadaOeste);
                return historico;
            }
            return null;
        }
    }
}
