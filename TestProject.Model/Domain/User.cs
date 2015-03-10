using TestProject.Model.Enum;

namespace TestProject.Model.Domain
{
    public class User : BaseEntity
    {

        public User()
        {
            // empty
        }

        public User(View.User model)
        {
            UserId = model.UserId;
            Email = model.Email;
            FirstName = model.FirstName;
            LastName = model.LastName;
            Status = model.Status;

        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public UserStatus Status { get; set; }

    }
}
