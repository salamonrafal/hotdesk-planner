using Core.Infrastructure;
using Core.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class UserRepository<TClass> : IRepository<TClass> where TClass : User, new()
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        public UserRepository(IMongoClient client)
        {
            _database = client.GetDatabase("hotdesk_planner");
            _collection = "users";
        }

        public Task<bool> Delete(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public Task Insert(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public Task<TClass> SelectOne(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<TClass>> Select()
        {
            throw new System.NotImplementedException();
        }
        public Task<List<TClass>> Select(QueryDocument query)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(TClass model)
        {
            throw new System.NotImplementedException();
        }
    }
}
