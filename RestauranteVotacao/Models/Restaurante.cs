using System.Collections.Generic;

namespace RestauranteVotacao.Models
{
    public class Restaurante
    {
        public static List<Restaurante> Restaurantes;

        #region Criar e Inserir Restaurantes numa lista.
        // Cria-se 6 restaurante fictícios e insere numa lista para carregar na tela.
        static Restaurante()
        {
            // 1. Cria-se os objetos.
            Restaurante morteLenta = new Restaurante(
                1000, 
                "Morte Lenta");

            Restaurante bandejao = new Restaurante(
                2000, 
                "Bandejão");

            Restaurante tiaLouca = new Restaurante(
                3000, 
                "Restaurante da Tia Louca");

            Restaurante gangueCalcaBranca = new Restaurante(
                4000, 
                "Gangue da Calça Branca");

            Restaurante voceNaoTemCoragem = new Restaurante(
                5000, 
                "Você Não Tem Coragem");

            Restaurante carpano = new Restaurante(
                6000, 
                "Carpano Restaurante");

            // 2. Insere-os na lista de Restaurantes a serem votados.
            Restaurantes = new List<Restaurante>();
            Restaurantes.Add(gangueCalcaBranca);
            Restaurantes.Add(voceNaoTemCoragem);
            Restaurantes.Add(tiaLouca);
            Restaurantes.Add(bandejao);
            Restaurantes.Add(morteLenta);
            Restaurantes.Add(carpano);
        }
        #endregion

        // O restaurante terá apenas um Nome eu um Identificador.
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Restaurante(int id, string nome)
        {
            this.Id = id;
            this.Nome = nome;
        }
    }
}