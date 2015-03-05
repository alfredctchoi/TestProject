using System;
using TestProject.Model.View;
using TestProject.Repository;
using TestProject.Repository.Interface;
using TestProject.Service.Interface;
using DomainUser = TestProject.Model.Domain.User;

namespace TestProject.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void Save(User item)
        {
            var user = new DomainUser
            {
                Created = DateTime.UtcNow,
                Deleted = false,
                Email = item.Email,
                Password = item.Password,
                Status = 0
            };

            _userRepository.Save(user);
        }
    }
}
