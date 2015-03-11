using System;
using TestProject.Model.Enums;

namespace TestProject.Model.Domain
{
    public class Vendor : BaseEntity
    {

        public Vendor()
        {
            //empty
        }

        public Vendor(View.Vendor view, int creatorId)
        {
            Name = view.Name;
            Code = view.Code;
            Currency = view.Currency;
            Country = view.Country;
            Created = DateTime.UtcNow;
            CreatedUserId = creatorId;
            Status = (VendorStatusEnum) Enum.Parse(typeof (VendorStatusEnum), view.Status);
            Deleted = false;
        }

        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public CurrencyEnum Currency { get; set; }
        public CountryEnum Country { get; set; }
        public VendorStatusEnum Status { get; set; }

        public virtual Bank Bank { get; set; }
    }
}
