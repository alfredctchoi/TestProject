using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TestProject.Extensions;
using TestProject.Model.View;
using TestProject.Service.Interface;

namespace TestProject.Controllers
{

    [RoutePrefix("vendor")]
    public class VendorController : ApiController
    {

        private readonly IVendorService _vendorService;

        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
            
        }

        // GET vendor
        [Route("")]
        //[Authenticate]
        public HttpResponseMessage Get([FromUri]VendorQuery query)
        {
            var vendors = _vendorService.Query(query);
            return !vendors.Any()
                ? Request.CreateResponse(HttpStatusCode.NotFound, new HttpError("Vendors not found.")) 
                : Request.CreateResponse(HttpStatusCode.OK, vendors);
        }

        // GET vendor/5
        [Route("{id:int}")]
        //[Authenticate]
        public HttpResponseMessage Get(int id)
        {
            var vendor = _vendorService.Get(id);
            return vendor == null
                ? Request.CreateErrorResponse(HttpStatusCode.NotFound, new HttpError("Vendor not found.")) 
                : Request.CreateResponse(HttpStatusCode.OK, vendor);
        }

        // POST vendor
        [Route("")]
        //[Authenticate]
        public HttpResponseMessage Post(Vendor vendor)
        {
            var vendorId = _vendorService.Create(vendor, Request.GetUserId());
            return Request.CreateResponse(HttpStatusCode.Created, new {VendorId = vendorId});
        }

        // PUT vendor/5
        [Route("{id:int}")]
        public HttpResponseMessage Put(int id, Vendor vendor)
        {
            _vendorService.Save(id, vendor, Request.GetUserId());
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE vendor/5
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            _vendorService.Remove(id, Request.GetUserId());
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
