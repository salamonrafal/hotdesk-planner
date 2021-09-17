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
    public class DeskService : IDeskService
    {
        private readonly IRepository<Desk> _repository;

        public DeskService(IRepository<Desk> repository)
        {
            _repository = repository;
        }

        public async Task<Desk> Get(Desk model)
        {
            return await _repository.SelectOne(model);
        }

        public async Task<List<Desk>> Get()
        {
            return await _repository.Select();
        }

        public async Task<Guid> Add(Desk model)
        {
            model.GenerateUuid();

            await _repository.Insert(model);

            return model.Id;
        }

        public async Task<bool> Update(Desk model)
        {
            return await _repository.Update(model);
        }

        public void ChnageState(bool isBlocked, int deskId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(Desk model)
        {
            return await _repository.Delete(model);
        }

        public async Task<List<Desk>> Search(BsonDocument query)
        {
            return await _repository.Select(new QueryDocument(query));
        }
    }
}
