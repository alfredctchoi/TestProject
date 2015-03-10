using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using TestProject.Model.Domain;
using TestProject.Model.Enum;
using TestProject.Model.View;
using TestProject.Repository.Interface;
using TestProject.Service.Interface;
using DomainUser = TestProject.Model.Domain.User;
using ModelUser = TestProject.Model.View.User;

namespace TestProject.Service
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly ISessionService _sessionService;

        public UserService(IUserRepository userRepository,
            ISessionService sessionService)
        {
            _userRepository = userRepository;
            _sessionService = sessionService;
        }

        public IEnumerable<ModelUser> GetAll()
        {
            var users = _userRepository.Query(u => true);
            return users.Select(u => u).ToList().Select(u => new ModelUser(u));
        }

        public Guid? Authenticate(UserLogin login)
        {
            if (login == null || string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Password))
            {
                throw new Exception("Invalid login.");
            }

            var hashPassword = HashPassword(login.Password);
            var user = _userRepository.Search(u => u.Email.Equals(login.Email) && u.Password.Equals(hashPassword));

            if (user == null)
            {
                return null;
            }

            var session = new Session
            {
                UserId = user.UserId,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddHours(24),
                Token = Guid.NewGuid(),
            };

            _sessionService.Create(session);

            return session.Token;
        }

        public void Save(object userId, ModelUser userModel)
        {
            if (!userModel.IsValid())
            {
                throw new Exception("Invalid user data");
            }

            var user = _userRepository.Get(userId);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            user.Email = userModel.Email;
            user.Password = userModel.Password;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.Modified = DateTime.UtcNow;

            _userRepository.Save();
        }

        public void Create(ModelUser userModel)
        {

            if (!userModel.IsValid())
            {
                throw new Exception("Invalid user data");
            }

            var existingUser = _userRepository.Search(u => u.Email.Equals(userModel.Email));

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new DomainUser(userModel)
            {
                Password = HashPassword(userModel.Password),
                Created = DateTime.UtcNow,
                Deleted = false,
                Status = UserStatus.Active
            };

            _userRepository.Create(user);
        }

        public ModelUser Get(object id)
        {
            var domainUser = _userRepository.Get(id);
            return domainUser == null ? null : new ModelUser(domainUser);
        }

        private static string HashPassword(string password)
        {
            string passwordHashed;
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                passwordHashed = sb.ToString();
            }

            return passwordHashed;
        }
    }
}
