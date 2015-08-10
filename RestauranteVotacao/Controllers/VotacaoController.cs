using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestauranteVotacao.Models;

namespace RestauranteVotacao.Controllers
{
    public class VotacaoController : Controller
    {
        // A cada execução verifica se o usuário continua logado. (OnActionExecuting)
        [RestauranteVotacao.Filters.Autorizar]
        public ActionResult Index()
        {
            // Dependendo do dia da votação, cria-se ou retorna os dados já existentes.
            return View(Votacao.ObterOuCriarVotacao());
        }

        #region Votar
        [RestauranteVotacao.Filters.Autorizar]
        [HttpPost]
        public ActionResult Votar(int restauranteId = -1)
        {
            // Busca ou cria uma votação.
            Dia dia = Votacao.ObterOuCriarVotacao().ObterDiaAtual();
            if (!restauranteId.Equals(-1))
            {
                // Procura se existe um restaurante na lista de restaurantes com o ID vindo da tela.
                Restaurante restaurante = Restaurante.Restaurantes.Where(r => r.Id.Equals(restauranteId)).First();
                // Adiciona um voto na lista de votos, passando o colaborador na sessão e o restaurante escolhido por ele.
                dia.Voto((Colaborador)Session["colaborador"], restaurante);
            }
            
            return RedirectToAction("Index");
        }
        #endregion

        #region Dia Anterior
        [RestauranteVotacao.Filters.Autorizar]
        public ActionResult DiaAnterior()
        {
            Votacao.DiaAnteriorVotacao();
            return RedirectToAction("Index");
        }
        #endregion

        #region Próximo dia da votação.
        [RestauranteVotacao.Filters.Autorizar]
        public ActionResult ProximoDia()
        {
            Votacao.ProximoDiaVotacao();
            return RedirectToAction("Index");
        }
        #endregion
    }
}