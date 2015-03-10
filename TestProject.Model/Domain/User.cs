using System.Collections.Generic;
using TestProject.Model.Enums;

namespace TestProject.Model.Domain
{
    public class User : BaseEntity
    {

        public User()
        {
        }

        public User(View.User model)
        {
            UserId = model.UserId;
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            StatusEnum = model.Status;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatusEnum StatusEnum { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

    }
}
