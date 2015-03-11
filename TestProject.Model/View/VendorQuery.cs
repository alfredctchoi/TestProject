using System.Collections.Generic;
using TestProject.Model.Enums;

namespace TestProject.Model.View
{
    public class VendorQuery
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public VendorStatusEnum? Status { get; set; }
    }
}
