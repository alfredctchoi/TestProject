using System;
using TestProject.Model.Domain;

namespace TestProject.Service.Interface
{
    public interface ISessionService : IBaseService<Session>
    {
        Session GetByGuid(Guid guid);

        Session GetActiveSessionByUserId(int userId);
    }
}
