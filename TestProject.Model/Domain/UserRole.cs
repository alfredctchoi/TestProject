using TestProject.Model.Enums;

namespace TestProject.Model.Domain
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public int? UserId { get; set; }
        public UserRoleEnum Role { get; set; }

        public virtual User User { get; set; }
    }
}
