using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Model.Domain;
using TestProject.Model.Enums;
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
            Status = user.StatusEnum;
            Roles = user.Roles.Select(r => r.Role.ToString()).ToList();
        }

        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserStatusEnum Status { get; set; }
        public IEnumerable<string> Roles { get; set; }
        
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

        public IList<UserRole> ToUserRoles()
        {
            return Roles.Select(r => new UserRole
            {
                Role = (UserRoleEnum) Enum.Parse(typeof (UserRoleEnum), r)
            }).ToList();
        }
    }
}
