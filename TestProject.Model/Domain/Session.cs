using System;

namespace TestProject.Model.Domain
{
    public class Session
    {
        public Int64 SessionId { get; set; }
        public Guid Token { get; set; }
        public int UserId { get; set; }
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }

        public virtual User User{ get; set; }
    }
}
