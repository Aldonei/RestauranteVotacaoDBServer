using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestauranteVotacao.Models
{
    public class Votacao
    {
        #region Declarção de variáveis
        protected static DateTime diaVotacao;
        protected static List<Votacao> votacoes = new List<Votacao>();
        public DateTime FimVotacao { get; private set; }
        public DateTime InicioVotacao { get; private set; }
        public List<Dia> Dias { get; private set; }
        #endregion

        #region Construtores
        static Votacao()
        {
            // Data atual do sistema.
            diaVotacao = DateTime.Now.Date;

            // Verifica se é Sábado e acrescenta um dia => Domingo
            if (diaVotacao.DayOfWeek == DayOfWeek.Saturday)
                diaVotacao = diaVotacao.AddDays(1);
            
            // Verifica se é Domingo => Passa para Segunda-Feira.
            if (diaVotacao.DayOfWeek == DayOfWeek.Sunday)
                diaVotacao = diaVotacao.AddDays(1);
        }

        public Votacao()
        {
            Dias = new List<Dia>();
        }
        #endregion

        #region Navegação nos dados
        public static void ProximoDiaVotacao()
        {
            // Verifica se é Sexta-feira. Caso sim, adiciona 3 dias para chegar na Segunda-Feira.
            if (diaVotacao.DayOfWeek == DayOfWeek.Friday)
                diaVotacao = diaVotacao.AddDays(3);
            else
                diaVotacao = diaVotacao.AddDays(1);
        }

        public static void DiaAnteriorVotacao()
        {
            if (diaVotacao.DayOfWeek == DayOfWeek.Monday)
                diaVotacao = diaVotacao.AddDays(-3);
            else
                diaVotacao = diaVotacao.AddDays(-1);
        }
        #endregion

        #region Obter dados
        public static DateTime ObterDiaVotacao()
        {
            return diaVotacao;
        }

        public static Votacao ObterOuCriarVotacao()
        {
            Votacao votacao = ObterVotacao(diaVotacao);

            // Caso não consiga obter uma votação existente, então cria uma nova com o dia da votação.
            if (votacao == null)
            {
                votacao = CriarVotacao(diaVotacao);
                votacoes.Add(votacao);
            }

            return votacao;
        }

        public ResultadoVotacao ObterResultadoDia(DateTime data)
        {
            Dia dia = Dias.Find(d => d.DataVotacao.Equals(data));
            Restaurante restaurante = dia.ObterRestauranteMaisVotado();
            int votos = dia.ObterContagemRestauranteMaisVotado();

            // Mostra na na tela o nome do restaurante selecionado ou a mensagem abaixo, quando não selecionado.
            ResultadoVotacao resultadoVotacao = new ResultadoVotacao
            {
                NomeRestaurante = restaurante != null ? restaurante.Nome : "Restaurante ainda não selecionado.",
                QuantidadeVotos = votos
            };
            return resultadoVotacao;
        }

        // Chamada da Index() para mostrar o nome do restaurante e número de votos na tabela.
        public Dia ObterDiaAtual()
        {
            Dia dia = Dias.Find(d => d.DataVotacao.Equals(diaVotacao));
            dia.VerificarRestaurantes();
            
            return dia;
        }

        protected static Votacao ObterVotacao(DateTime diaVotacao)
        {
            // Verifica se o dia da votação está dentro da semana.
            foreach (Votacao votacaoSemana in votacoes)
                if (diaVotacao.Date >= votacaoSemana.InicioVotacao && diaVotacao.Date <= votacaoSemana.FimVotacao)
                    return votacaoSemana;

            return null;
        }

        // Configura o dia inicial e final da votação
        protected static Votacao ConfigurarDatasVotacaoSemana(Votacao votacaoSemana, DateTime diaSemana)
        {
            int valor = DayOfWeek.Monday - diaSemana.DayOfWeek;
            votacaoSemana.InicioVotacao = diaSemana.Date.AddDays(valor);
            valor = DayOfWeek.Friday - diaSemana.DayOfWeek;
            votacaoSemana.FimVotacao = diaSemana.Date.AddDays(valor);

            return votacaoSemana;
        }
        #endregion

        #region Criar dados
        // Para cada dia útil da semana, cria-se um objeto Dia, que por sua vez fará parte 
        // numa lista de Votacao do respectivo dia.
        protected static Votacao CriarVotacao(DateTime diaSemana)
        {
            Votacao semanaVotacao = new Votacao();
            ConfigurarDatasVotacaoSemana(semanaVotacao, diaSemana);
            // O objeto dia é composto por Dia(DateTime dataVotacao, Dia diaAnterior)
            // Na segunda-feira o segundo parâmetro diaAnterior é null, pois o dia anterior é um domingo.
            Dia segundaFeira = new Dia(semanaVotacao.InicioVotacao, null);
            Dia tercaFeira = new Dia(semanaVotacao.InicioVotacao.AddDays(1), segundaFeira);
            Dia quartaFeira = new Dia(semanaVotacao.InicioVotacao.AddDays(2), tercaFeira);
            Dia quintaFeira = new Dia(semanaVotacao.InicioVotacao.AddDays(3), quartaFeira);
            Dia sextaFeira = new Dia(semanaVotacao.InicioVotacao.AddDays(4), quintaFeira);

            // Adiciona na lista de Dias.
            semanaVotacao.Dias.Add(segundaFeira);
            semanaVotacao.Dias.Add(tercaFeira);
            semanaVotacao.Dias.Add(quartaFeira);
            semanaVotacao.Dias.Add(quintaFeira);
            semanaVotacao.Dias.Add(sextaFeira);

            return semanaVotacao;
        }
        #endregion

        #region Resultados na semana. De segunda à sexta.
        public ResultadoVotacao ObterResultadoSegundaFeira()
        {
            return ObterResultadoDia(InicioVotacao);
        }

        public ResultadoVotacao ObterResultadoTercaFeira()
        {
            return ObterResultadoDia(InicioVotacao.AddDays(1));
        }

        public ResultadoVotacao ObterResultadoQuartaFeira()
        {
            return ObterResultadoDia(InicioVotacao.AddDays(2));
        }

        public ResultadoVotacao ObterResultadoQuintaFeira()
        {
            return ObterResultadoDia(InicioVotacao.AddDays(3));
        }

        public ResultadoVotacao ObterResultadoSextaFeira()
        {
            return ObterResultadoDia(InicioVotacao.AddDays(4));
        }
        #endregion
    }
}
