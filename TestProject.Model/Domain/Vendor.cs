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

        public Vendor(View.Vendor view, int countryId, int creatorId)
        {
            Name = view.Name;
            Code = view.Code;
            Currency = view.Currency;
            CountryId = countryId;
            Created = DateTime.UtcNow;
            CreatedUserId = creatorId;
            Status = (VendorStatusEnum) Enum.Parse(typeof (VendorStatusEnum), view.Status);
            Deleted = false;
        }

        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CountryId { get; set; }
        public CurrencyEnum Currency { get; set; }
        public VendorStatusEnum Status { get; set; }

        public virtual Bank Bank { get; set; }
        public virtual Country Country { get; set; }
    }
}
