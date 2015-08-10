using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestauranteVotacao.Models;

namespace RestauranteVotacao.Tests
{
    [TestClass]
    public class VotacaoTest : Votacao
    {
        #region Obter o dia da votação
        [TestMethod]
        public void ObterDiaVotacaoTeste()
        {
            DateTime diaVotacao = new DateTime(2015, 08, 10);
            Votacao.diaVotacao = diaVotacao;
            Assert.AreEqual(Votacao.ObterDiaVotacao(), diaVotacao);
        }
        #endregion

        #region Testar início e fim da votação.
        [TestMethod]
        public void ObterOuCriarVotacaoComDataInicioEFimDaVotacaoTeste() 
        {
            // Votei na segunda-feira. 
            Votacao.diaVotacao = new DateTime(2015, 08, 10);
            // Terminei da Sexta-feira a votação.
            DateTime sextaFeira = new DateTime(2015, 08, 14);
            // Segunda-feira começou a votação.
            DateTime segundaFeira = new DateTime(2015, 08, 10);
            Votacao votacao = Votacao.ObterOuCriarVotacao();
            // Testa se realmente foi na segunda-feira o dia inicial.
            Assert.AreEqual(votacao.InicioVotacao, segundaFeira);
            // Testa se realmente foi na sexta-feira o fim da votação.
            Assert.AreEqual(votacao.FimVotacao, sextaFeira);
        }
        #endregion

        #region Criar Votação
        [TestMethod]
        public void CriarNovaVotacaoTeste()
        {
            // Primeira votação. Deve ser criada neste momento
            Votacao.diaVotacao = new DateTime(2015, 08, 10);
            Assert.AreEqual(Votacao.votacoes.Count, 0);

            // Verifica se existe uma votação criada.
            Votacao.ObterOuCriarVotacao();
            Assert.AreEqual(Votacao.votacoes.Count, 1);
        }
        #endregion 
    }
}
