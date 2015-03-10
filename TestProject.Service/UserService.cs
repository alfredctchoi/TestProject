using System;
using System.Collections.Generic;
using System.Linq;
using TestProject.Model.Domain;
using TestProject.Model.Enums;
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

            var hashPassword = PasswordUtility.Encrypt(login.Password);
            var user = _userRepository.Search(u => u.Email.Equals(login.Email) && u.Password.Equals(hashPassword) && !u.Deleted);

            if (user == null)
            {
                return null;
            }

            var session = _sessionService.GetActiveSessionByUserId(user.UserId);

            if (session != null)
            {
                session.Expires = DateTime.UtcNow.AddHours(24);
                _sessionService.Save(session.SessionId, session);
                return session.Token;
            }

            session = new Session
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

            if (user == null || user.Deleted)
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

            var existingUser = _userRepository.Search(u => u.Email.Equals(userModel.Email) && !u.Deleted);

            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new DomainUser(userModel)
            {
                Password = PasswordUtility.Encrypt(userModel.Password),
                Created = DateTime.UtcNow,
                Deleted = false,
                StatusEnum = UserStatusEnum.Active,
                Roles = userModel.ToUserRoles()
            };

            _userRepository.Create(user);
        }

        public void Remove(object id)
        {
            var user = _userRepository.Get(id);
            
            if (user.Deleted)
            {
                return;
            }

            user.Deleted = true;
            _userRepository.Save();
        }

        public ModelUser Get(object id)
        {
            var domainUser = _userRepository.Get(id);
            return domainUser == null || domainUser.Deleted ? null : new ModelUser(domainUser);
        }
    }
}
