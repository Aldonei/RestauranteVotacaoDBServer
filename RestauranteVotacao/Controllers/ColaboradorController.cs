using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace RestauranteVotacao.Controllers
{
    public class ColaboradorController : Controller
    {
        [HttpPost]
        public ActionResult Login(string email, string senha)
        {
            // Autentica o colaborador logado.
            var colaborador = RestauranteVotacao.Models.Colaborador.Autenticar(email, senha);
            if (colaborador != null)
            {
                // Coloca na sessão o colaborador logado.
                Session["colaborador"] = colaborador;
                // Redirecionara para o método Votacao.
                return RedirectToAction("Index", "Votacao");
            }

            ViewBag.MensagemErroLogin = "Usuário ou Senha incorretos ou não cadastrado.";
            return View();
        }

        // Get
        public ActionResult Login()
        {
            // Information: If you need to access the Session then one way would be to override the "OnActionExecuting" 
            // method and access it there, as it will be available by that time.
            // A informação acima é feita usando o [RestauranteVotacao.Filters.Autorizar] - para mais infornações veja
            // public override void OnActionExecuting(ActionExecutingContext context)

            // Estancia uma sessão nula.
            Session["colaborador"] = null;
            
            return View();
        }
    }
}