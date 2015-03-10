using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using TestProject.Service;
using TestProject.Service.Interface;

namespace TestProject.Filters
{
    public class Authenticate : ActionFilterAttribute
    {

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            const string tokenKey = "X-Limitless-Token";
            const string userKey = "X-Limitless-UserId";

            try
            {
                var sessionService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(ISessionService)) as SessionService;

                Guid guid;
                var isTokenValid = Guid.TryParse(actionContext.Request.Headers.GetValues(tokenKey).First(), out guid);
                var userId = Convert.ToInt32(actionContext.Request.Headers.GetValues(userKey).First());

                if (sessionService == null || !isTokenValid)
                {
                    throw new Exception();
                }

                var session = sessionService.GetByGuid(guid);

                if (session == null || !session.UserId.Equals(userId))
                {
                    throw new Exception();
                }

                base.OnActionExecuting(actionContext);
            }
            catch (Exception)
            {
                var response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                actionContext.Response = response;
            }
            
        }
    }
}