using Core.Helpers;
using Core.Infrastructure;
using Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Add(User model)
        {
            model.GenerateUuid();

            await _repository.Insert(model);

            return model.Id;
        }

        public async Task<User> Get(User model)
        {
            return await _repository.SelectOne(model);
        }

        public async Task<List<User>> Get()
        {
            return await _repository.Select();
        }

        public async Task<bool> Remove(User model)
        {
            return await _repository.Delete(model);
        }

        public async Task<List<User>> Search(BsonDocument query)
        {
            return await _repository.Select(new QueryDocument(query));
        }

        public void SetRole(User model, UserRole userRole)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(User model)
        {
            return await _repository.Update(model);
        }
    }
}
