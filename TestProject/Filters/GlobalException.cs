using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Web.Http;
using System.Web.Http.Filters;

namespace TestProject.Filters
{
    public class GlobalException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            var response = new HttpResponseMessage
            {
                Content = new StringContent(actionExecutedContext.Exception.Message),
                StatusCode = HttpStatusCode.BadRequest
            };;

            if (actionExecutedContext.Exception is AuthenticationException)
            {
                response = new HttpResponseMessage
                {
                    Content = new StringContent("Unauthorized"),
                    StatusCode = HttpStatusCode.Unauthorized
                };
            } 


            throw new HttpResponseException(response);
        }
    }
}