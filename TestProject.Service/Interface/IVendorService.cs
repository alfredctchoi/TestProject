using System.Collections.Generic;
using TestProject.Model.View;

namespace TestProject.Service.Interface
{
    public interface IVendorService : IBaseService<Vendor>
    {

        List<Vendor> Query(VendorQuery filter);

    }
}
