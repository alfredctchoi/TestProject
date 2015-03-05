using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProject.Model.View;
using TestProject.Service;

namespace TestProject.Controllers
{
    public class UserController : ApiController
    {

        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        // GET api/user
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        public HttpResponseMessage Post(User user)
        {
            _userService.Save(user);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
