using System;

namespace TestProject.Model.Domain
{
    public class State : BaseEntity
    {

        public State()
        {
            // empty
        }

        public State(int countryId, string name, string code)
        {
            Deleted = false;
            Created = DateTime.UtcNow;
            CountryId = countryId;
            Name = name;
            Code = code;
        }

        public int StateId { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual Country Country { get; set; }
    }
}
