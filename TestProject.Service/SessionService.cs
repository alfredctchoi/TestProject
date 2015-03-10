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
            throw new System.NotImplementedException();
        }

        public void Create(Session item)
        {
            _sessionRepository.Create(item);
        }

        public Session Get(object id)
        {
            throw new System.NotImplementedException();
        }

        public Session GetByGuid(Guid guid)
        {
            return _sessionRepository.Search(s => s.Token.Equals(guid));
        }
    }
}
