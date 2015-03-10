using System;
using System.Collections.Generic;
using TestProject.Model.View;

namespace TestProject.Service.Interface
{
    public interface IUserService : IBaseService<User>
    {
        IEnumerable<User> GetAll();

        Guid? Authenticate(UserLogin login);
    }
}
