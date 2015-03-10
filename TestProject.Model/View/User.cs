using TestProject.Model.Enum;
using TestProject.Model.View.Interfaces;

namespace TestProject.Model.View
{
    public class User : IValid
    {

        public User()
        {
            // empty
        }

        public User(Domain.User user)
        {
            UserId = user.UserId;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Status = user.Status;
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserStatus Status { get; set; }
        
        public virtual bool IsValid()
        {
            if (string.IsNullOrEmpty(Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(FirstName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(LastName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                return false;
            }

            return true;
        }
    }
}
