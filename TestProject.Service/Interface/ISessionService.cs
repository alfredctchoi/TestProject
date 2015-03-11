using System;
using TestProject.Model.Domain;

namespace TestProject.Service.Interface
{
    public interface ISessionService
    {
        Session GetByGuid(Guid guid);

        Session GetActiveSessionByUserId(int userId);

        void Save(object id, Session item);

        object Create(Session item);
    }
}
