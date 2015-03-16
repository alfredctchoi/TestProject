using System;

namespace TestProject.Model.Domain
{
    public class Country : BaseEntity
    {

        public Country()
        {
            // empty
        }

        public Country(string name, string isoShort, string isoLong)
        {
            Created = DateTime.UtcNow;
            Deleted = false;
            Name = name;
            IsoShort = isoShort;
            IsoLong = isoLong;
        }

        public int CountryId { get; set; }
        public string Name { get; set; }
        public string IsoShort { get; set; }
        public string IsoLong { get; set; }

        public virtual State State { get; set; }
    }
}
