using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestauranteVotacao.Models;
using System.Collections.Generic;

namespace RestauranteVotacao.Tests.Models
{
    [TestClass]
    public class DiaTest
    {
        #region Estória 1 e 3
        // Estória 1 e Estória 3 cobertas neste teste.
        // 1. Quero votar no meu restaurante favorito 
        // 2. Quero saber antes do meio dia qual foi o restaurante escolhido
        // Critério de Aceitação
	    // 1. Um profissional só pode votar em um restaurante por dia.
        // 2. Mostrar de alguma forma o resultado da votação.

        [TestMethod]
        public void ObterRestauranteMaisVotadoTeste()
        {
            Dia dia = new Dia(new DateTime(2015, 08, 10), null);
            Colaborador colaborador1 = new Colaborador(
                1000, 
                "Colaborador Um",
                "colaborador1@gmail.com", 
                "senha1");

            Colaborador colaborador2 = new Colaborador(
                2000,
                "Colaborador Dois",
                "colaborador2@gmail.com", 
                "senha2");

            Colaborador colaborador3 = new Colaborador(
                3000,
                "Colaborador Três",
                "colaborador3@gmail.com", 
                "senha3");

            Colaborador colaborador4 = new Colaborador(
                4000,
                "Colaborador Quatro",
                "colaborador3@gmail.com",
                "senha4");

            Colaborador colaborador5 = new Colaborador(
                5000,
                "Colaborador Cinco",
                "colaborador5@gmail.com",
                "senha5");

            dia.VerificarRestaurantes();
            Restaurante morteLenta = new Restaurante(
                1000, 
                "Morte Lenta");

            Restaurante bandejao = new Restaurante(
                2000, 
                "Bandejão");

            Restaurante carpano = new Restaurante(
                6000,
                "Carpano Restaurante");

            Restaurante gangueCalcaBranca = new Restaurante(
                4000,
                "Gangue da Calça Branca");

            Restaurante tiaLouca = new Restaurante(
                3000, 
                "Restaurante da Tia Louca");

            // Resultado: Bandejão = 3, Morte Lenta = 2, Carpano = 2, Gangue da Calça Branca = 1, Tia Louca = 2
            dia.Voto(colaborador1, morteLenta);
            dia.Voto(colaborador2, morteLenta);
            dia.Voto(colaborador3, bandejao);
            dia.Voto(colaborador4, carpano);
            dia.Voto(colaborador5, gangueCalcaBranca);

            dia.Voto(colaborador1, bandejao);
            dia.Voto(colaborador3, carpano);
            dia.Voto(colaborador2, bandejao);
            dia.Voto(colaborador5, tiaLouca);
            dia.Voto(colaborador4, tiaLouca);
            Assert.AreEqual(dia.ObterRestauranteMaisVotado().Id, bandejao.Id);
        }
        #endregion

        #region Verificar se o colaborador já votou no dia.
        // Testar se o colaborador já votou no dia. 
        // Verifica no Dicionário de Votos Dictionary<Colaborador, Restaurante>; se já existe uma chave
        // como Votos.Keys.Where(c => c.Id.Equals(colaborador.Id)).ToList().Count != 0. Este método
        // ColaboradorVotou() retorna TRUE se existir algum ID de colaborador na lista de votos.
        [TestMethod]
        public void TestarVotacaoESeOColaboradorJaVotouTeste()
        {
            Dia dia = new Dia(new DateTime(2015, 08, 5), null);
            Colaborador colaborador1 = new Colaborador(
                1000,
                "Colaborador Um",
                "colaborador1@gmail.com",
                "senha1");
            
            // Todos os restaurantes disponíveis no dia.
            dia.VerificarRestaurantes();
            Restaurante morteLenta = new Restaurante(
                1000, 
                "Morte Lenta");

            // Voto(Colaborador colaborador, Restaurante restaurante)
            dia.Voto(colaborador1, morteLenta);
            Assert.IsTrue(dia.ColaboradorVotou(colaborador1));
        }
        #endregion

        #region Estória 3
        // Estória 3
        // Só quer saber quem ganhou a votação para ir almoçar logo.
        // Critério de Aceitação
        // Mostrar de alguma forma o resultado da votação.
        // Método testado é o ObterRestauranteMaisVotado()
        [TestMethod]
        public void ObterContagemRestauranteMaisVotadoTeste()
        {
            // 1. Seta o dia com uma data válida.
            Dia dia = new Dia(new DateTime(2015, 08, 11), null);
            // 2. Cadastra 5 colaboradores 
            Colaborador colaborador1 = new Colaborador(
                1000, 
                "Colaborador Um",
                "colaborador1@gmail.com", 
                "senha1");

            Colaborador colaborador2 = new Colaborador(
                2000,
                "Colaborador Dois",
                "colaborador2@gmail.com", 
                "senha2");

            Colaborador colaborador3 = new Colaborador(
                3000,
                "Colaborador Três",
                "colaborador3@gmail.com", 
                "senha3");

            Colaborador colaborador4 = new Colaborador(
                4000,
                "Colaborador Quatro",
                "colaborador4@gmail.com", 
                "senha4");

            Colaborador colaborador5 = new Colaborador(
                5000,
                "Colaborador Cinco",
                "colaborador5@gmail.com", 
                "senha5");

            dia.VerificarRestaurantes();
            // 3. Cadastra dois restaurantes Morte Lenta e Bandejão.
            Restaurante morteLenta = new Restaurante(
                1000, 
                "Morte Lenta");

            Restaurante bandejao = new Restaurante(
                2000, 
                "Bandejão");

            // 4. Contabiliza os votos de cada colaborador para seu restaurante preferido.
            // Voto(Colaborador, Restaurante)
            dia.Voto(colaborador3, bandejao);
            dia.Voto(colaborador1, morteLenta);
            dia.Voto(colaborador4, morteLenta);
            dia.Voto(colaborador2, morteLenta);
            dia.Voto(colaborador5, bandejao);
                        
            // 5. Pergunta se o Nome do Restaurante mais votados é igual ao Bandejão.
            // O restaurante mais votado do dia é igual ao 3000 (Morte Lenta).
            // 5 votos contabilizados: 3 votos para o Morte lenta e 2 para o Bandejão.
            Assert.AreEqual(dia.ObterRestauranteMaisVotado(), morteLenta.Id);
        }
        #endregion

        #region Estória 2
        // Estória 2
        // Quero que um restaurante não possa ser repetido durante a semana
        // Para que não precise ouvir reclamações infinitas!
        // Critério de Aceitação
        // O mesmo restaurante não pode ser escolhido mais de uma vez durante a semana, ou seja
        // não se pode deixar o restaurante escolhido no dia anteiro ser escolhido novamente. 
        [TestMethod]
        public void RestauranteEscolhidoUmaVezSomenteNaSemanaTeste()
        {
            // 1. Vamos setar o dia 27 de Agosto, quinta-feira. Null, significa que no dia anterior não
            // houve votação.
            Dia dia = new Dia(new DateTime(2015, 8, 27), null);
            // 2. Criamos os 5 colaboradores
            Colaborador colaborador1 = new Colaborador(
                1000, 
                "Colaborador Um",
                "colaborador1@gmail.com", 
                "senha1");

            Colaborador colaborador2 = new Colaborador(
                2000,
                "Colaborador Dois",
                "colaborador2@gmail.com", 
                "senha2");

            Colaborador colaborador3 = new Colaborador(
                3000,
                "Colaborador Três",
                "colaborador3@gmail.com", 
                "senha3");

            Colaborador colaborador4 = new Colaborador(
                4000,
                "Colaborador Quatro",
                "colaborador4@gmail.com",
                "senha4");

            Colaborador colaborador5 = new Colaborador(
                5000,
                "Colaborador Cinco",
                "colaborador5@gmail.com",
                "senha5");

            // 3. Verificamos os restaurantes do dia. Vai trazer os 6 cadastrados, pois o dia
            // foi passado como null. Observe o item 1. (Dia dia = new Dia(new DateTime(2015, 8, 27), null).
            dia.VerificarRestaurantes();
            // 4. Vamos criar dois para a votação.
            Restaurante morteLenta = new Restaurante(
                1000, 
                "Morte Lenta");

            Restaurante bandejao = new Restaurante(
                2000, 
                "Bandejão");
            // 5. Espera-se encontrar 6 restaurantes na lista dia.Restaurantes.
            Assert.AreEqual(dia.Restaurantes.Count, 6);
            // 6. Vamos votar agora duas vezes no "Morte Lenta" e uma vez no "Bandejão".
            dia.Voto(colaborador1, morteLenta);
            dia.Voto(colaborador2, morteLenta);
            dia.Voto(colaborador3, bandejao);

            // 7. O próximo dia será 28 de Agosto, sexta-feira.
            Dia proximoDia = new Dia(new DateTime(2015, 8, 28), dia);
            // 8. Nesta verificação serão passados 3 votos na sexta-feira. O mais votado foi o "Morte Lenta". 
            // A lista de restaurantes retorna com 5, ou seja sem o "Morte Lenta".
            proximoDia.VerificarRestaurantes();
            Assert.AreEqual(proximoDia.Restaurantes.Count, 5);
        }
        #endregion
    }
}
