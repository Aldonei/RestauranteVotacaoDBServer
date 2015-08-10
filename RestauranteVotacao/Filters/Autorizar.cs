using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestauranteVotacao.Filters
{
    public class Autorizar : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Session["colaborador"] == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Colaborador", action = "Login" }));
                context.Result.ExecuteResult(context.Controller.ControllerContext);
            }
        }

    }
}