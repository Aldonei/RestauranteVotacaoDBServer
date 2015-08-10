using System;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteVotacao.Models
{
    public class Dia
    {
        #region Declaração de variáveis.
        public Dia DiaAnterior { get; private set; }
        public DateTime DataVotacao;
        public DateTime DataSistema;
        public List<Restaurante> Restaurantes = new List<Restaurante>();
        // Cria-se um dicionário para guardar o voto de cada colaborador.
        protected Dictionary<Colaborador, Restaurante> Votos;
        #endregion

        #region Método construtor com dois parâmetros.
        public Dia(DateTime dataVotacao, Dia diaAnterior)
        {
            this.DataVotacao = dataVotacao;
            this.DiaAnterior = diaAnterior;
            this.DataSistema = DateTime.Now.Date;
            Votos = new Dictionary<Colaborador, Restaurante>();
        }
        #endregion

        #region Voto que recebe o Colaborador e o Restaurante.
        public void Voto(Colaborador colaborador, Restaurante restaurante)
        {
            // Verifica se o colaborador já votou. "Um profissional só pode votar em um restaurante por dia."
            if (!ColaboradorVotou(colaborador))
                Votos.Add(colaborador, restaurante);
        }
        #endregion

        #region Restaurante mais votado pelos colaboradores.
        public Restaurante ObterRestauranteMaisVotado()
        {
            // Se a lista estiver vazia não faz nada.
            if (Votos.Keys.Count.Equals(0))
                return null;

            // A variável máximo neste momento é zero.
            int maximo = 0;
            Restaurante restauranteRemovido = null;
            
            // Varre a lista de restaurantes contando os votos que cada restaurante recebeu.
            Restaurantes.ForEach(new Action<Restaurante>(delegate(Restaurante restaurante)
            {
                // Procura na lista de Votos pelo nome do restaurante.
                int contador = Votos.Where(r => r.Value.Nome.Equals(restaurante.Nome)).ToList().Count;
                // Verifica se o contador de votos ainda não é o máximo.
                if (contador > maximo)
                {
                    maximo = contador;
                    restauranteRemovido = restaurante;
                }
            }));

            return restauranteRemovido;
        }
        #endregion

        #region Número de votos do restaurante mais votado.
        public int ObterContagemRestauranteMaisVotado()
        {
            Restaurante restaurante = ObterRestauranteMaisVotado();
            
            if (restaurante == null)
                return 0;

            // Procura na lista de Votos quantas vezes o nome do restaurante aparece.
            return Votos.Where(r => r.Value.Nome.Equals(restaurante.Nome)).ToList().Count;
        }
        #endregion

        // Prepara a lista de restaurantes para um outro método consumir.
        public void VerificarRestaurantes()
        {
            // A lista já está com os restaurantes, não faz nada.
            if (!Restaurantes.Count.Equals(0))
                return;

            // A data de votação é uma segunda-feira, retorna todos os restaurantes cadastrados, pois
            // não houve votação no dia anterior e no mais é o íncio de uma nova semana.
            if (DataVotacao.DayOfWeek.Equals(DayOfWeek.Monday))
            {
                Restaurantes = Restaurante.Restaurantes;
                return;
            }
            
            // Se não houve votação no dia anterior ou não teve definição, retorna a lista de todos os restaurantes cadastrados.
            // Isto acontece num feriado no meio da semana ou na segunda-feira, pois o dia anterior foi um Domingo. 
            if (DiaAnterior == null)
            {
                Restaurantes = Restaurante.Restaurantes;
                return;
            }

            Restaurantes = AjustarRestaurantes(Restaurante.Restaurantes, DiaAnterior.ObterRestauranteMaisVotado());
        }

        private List<Restaurante> AjustarRestaurantes(List<Restaurante> Restaurantes, Restaurante restauranteMaisVotado)
        {
            // Se não teve votação e nenhum restaurante foi escolhido, retorna a lista de todos dos restaurantes cadastrados.
            if (restauranteMaisVotado == null && DiaAnterior.Restaurantes.Count.Equals(0))
                Restaurantes = Restaurante.Restaurantes;
            // Se mesmo assim não teve nenhum voto para o restaurante, vai retornar a lista com os restaurantes do dia anterior.
            else if (restauranteMaisVotado == null)
                Restaurantes = DiaAnterior.Restaurantes;
            else
            {
                // Busca os restaurantes votados no dia anterior.
                List<Restaurante> restaurantesAnteriores = DiaAnterior.Restaurantes;
                // Faz uma cópia dos restaurantes anteriores
                List<Restaurante> copiaRestaurantesAnteriores = new List<Restaurante>(restaurantesAnteriores);
                // Remove da lista dos restaurantes anteriores os mais votados.
                copiaRestaurantesAnteriores.Remove(restauranteMaisVotado);
                // Então retorna a lista de restaurantes sem os mais votados, por consequência não se exibe mais
                // para o colaborador votar.
                Restaurantes = copiaRestaurantesAnteriores;
            }
            return Restaurantes;
        }

        // Um profissional só pode votar em um restaurante por dia.
        public bool ColaboradorVotou(Colaborador colaborador)
        {
            // Retorna TRUE se encontrar algum colaborador na lista que já tenha votado no dia.
            return Votos.Keys.Where(c => c.Id.Equals(colaborador.Id)).ToList().Count != 0;
        }
    }
}