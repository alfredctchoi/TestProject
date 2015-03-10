﻿using TestProject.Database.Context.Interface;
using TestProject.Model.Domain;
using TestProject.Repository.Interface;

namespace TestProject.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IAdminContext context)
            :base(context)
        {

        }
    }
}
