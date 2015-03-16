using System;

namespace TestProject.Model.Domain
{
    public class Bank : BaseEntity
    {

        public Bank()
        {
            // empty
        }

        public Bank(View.Bank view, int creator)
        {
            CreatedUserId = creator;
            Created = DateTime.UtcNow;
            Deleted = false;
            AccountNumber = view.AccountNumber;
            BranchNumber = view.BranchNumber;
            BankCode = view.BankCode;
        }

        public Bank(View.Bank view, int stateId, int creator)
            : this(view, creator)
        {
            Address = view.Address;
            City = view.City;
            StateId = stateId;
            Zip = view.Zip;
            BranchNumber = null;
            BankCode = null;
        }

        public int VendorId { get; set; }
        public string AccountNumber { get; set; }
        public string BranchNumber { get; set; }
        public string BankCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? StateId{ get; set; }
        public string Zip { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual State State { get; set; }

    }
}
