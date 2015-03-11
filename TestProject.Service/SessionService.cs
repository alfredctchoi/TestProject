using System;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;
using TestProject.Service.Interface;

namespace TestProject.Service
{
    public class SessionService : ISessionService
    {

        private readonly ISessionRepository _sessionRepository;

        public SessionService(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public void Save(object id, Session item)
        {
            var session = _sessionRepository.Get(id);
            session.Expires = item.Expires;
            _sessionRepository.Save();
        }

        public object Create(Session item)
        {
            _sessionRepository.Create(item);

            return item.SessionId;
        }


        public Session GetByGuid(Guid guid)
        {
            return _sessionRepository.Search(s => s.Token.Equals(guid));
        }

        public Session GetActiveSessionByUserId(int userId)
        {
            var currentDateTime = DateTime.UtcNow;
            var session = _sessionRepository.Search(s => s.UserId == userId && s.Expires > currentDateTime);
            if (session != null)
            {
                return session;
            }

            return null;
        }
    }
}
