using TestProject.Model.Enums;
using TestProject.Model.View.Interfaces;

namespace TestProject.Model.View
{
    public class Vendor : IValid
    {

        public Vendor()
        {
            // empty
        }

        public Vendor(Domain.Vendor vendor)
        {
            VendorId = vendor.VendorId;
            Name = vendor.Name;
            Code = vendor.Code;
            Status = vendor.Status.ToString();
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Code) || Bank == null || string.IsNullOrEmpty(Bank.AccountNumber))
            {
                return false;
            }

            if (Country == CountryEnum.Canada)
            {
                if (string.IsNullOrEmpty(Bank.BankCode) || string.IsNullOrEmpty(Bank.BranchNumber))
                {
                    return false;
                }
            }

            if (Country == CountryEnum.Usa)
            {
                if (string.IsNullOrEmpty(Bank.Address) || string.IsNullOrEmpty(Bank.City) ||
                    string.IsNullOrEmpty(Bank.Zip) || string.IsNullOrEmpty(Bank.State) || Currency != CurrencyEnum.Usd)
                {
                    return false;
                }
            }

            return true;
        }

        public int VendorId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public CurrencyEnum Currency { get; set; }
        public CountryEnum Country { get;set; }
        public Bank Bank { get; set; }
    }
}
