using Core.Infrastructure;
using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.ApplicationFactories.Mocks
{
    internal class UserRepositoryMock : IRepository<User>
    {
        public Task<bool> Delete(User model)
        {
            throw new NotImplementedException();
        }

        public Task Insert(User model)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Select(QueryDocument query)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<User> SelectOne(User model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User model)
        {
            throw new NotImplementedException();
        }
    }
}
