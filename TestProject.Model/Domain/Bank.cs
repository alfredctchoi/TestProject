using System;
using TestProject.Model.Enums;

namespace TestProject.Model.Domain
{
    public class Bank : BaseEntity
    {

        public Bank()
        {
            // empty
        }

        public Bank(View.Bank view, CountryEnum country, int creator)
        {
            CreatedUserId = creator;
            Created = DateTime.UtcNow;
            Deleted = false;

            if (country == CountryEnum.Usa)
            {
                Address = view.Address;
                City = view.City;
                State = view.State;
                Zip = view.Zip;
            }
            else
            {
                AccountNumber = view.AccountNumber;
                BranchNumber = view.BranchNumber;
                BankCode = view.BankCode;
            }
        }

        public int VendorId { get; set; }
        public string AccountNumber { get; set; }
        public string BranchNumber { get; set; }
        public string BankCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual Vendor Vendor { get; set; }

    }
}
