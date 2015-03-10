using System;

namespace TestProject.Model.Domain
{
    public class BaseEntity
    {
        public int? CreatedUserId { get; set; }
        public int? ModifiedUserId { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
        public bool Deleted { get; set; }

        public virtual User CreatedUser { get; set; }
        public virtual User ModifiedUser { get; set; }
    }
}
