using Core.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService
    {
        public void SetRole(User model, UserRole userRole);
        public Task<User> Get(User model);
        public Task<List<User>> Get();
        public Task<List<User>> Search(BsonDocument query);
        public Task<Guid> Add(User model);
        public Task<bool> Remove(User model);
        public Task<bool> Update(User model);
    }
}
