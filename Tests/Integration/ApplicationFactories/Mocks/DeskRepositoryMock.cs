using Core.Infrastructure;
using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.ApplicationFactories.Mocks
{
    internal class DeskRepositoryMock : IRepository<Desk>
    {
        public Task<bool> Delete(Desk model)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Desk model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Desk>> Select(QueryDocument query)
        {
            throw new NotImplementedException();
        }

        public Task<List<Desk>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<Desk> SelectOne(Desk model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Desk model)
        {
            throw new NotImplementedException();
        }
    }
}
