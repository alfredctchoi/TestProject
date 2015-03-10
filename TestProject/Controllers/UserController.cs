using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProject.Filters;
using TestProject.Model.View;
using TestProject.Service.Interface;

namespace TestProject.Controllers
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("authenticate")]
        public HttpResponseMessage PostAuthenticate(UserLogin login)
        {
            var token = _userService.Authenticate(login);
            return token == null 
                ? Request.CreateResponse(HttpStatusCode.Unauthorized) 
                : Request.CreateResponse(HttpStatusCode.OK, token);
        }

        // GET api/user
        [Route("")]
        [Authenticate]
        public HttpResponseMessage Get()
        {
            var users = _userService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        // GET api/user/5
        [Route("{id:int}")]
        [Authenticate]
        public HttpResponseMessage Get(int id)
        {
            var user = _userService.Get(id);
            return user == null
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, user);
        }

        // POST api/user
        [Route("")]
        [Authenticate]
        public HttpResponseMessage Post(User user)
        {
            _userService.Create(user);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/user/5
        [Route("{id:int}")]
        [Authenticate]
        public HttpResponseMessage Put(int id, User user)
        {
            _userService.Save(id, user);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
