using Core.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IDeskService
    {
        public Task<Desk> Get(Desk model);
        public Task<List<Desk>> Get();
        public Task<List<Desk>> Search(BsonDocument query);
        public Task<Guid> Add(Desk model);
        public Task<bool> Remove(Desk model);
        public void ChnageState(bool isBlocked, int deskId);
        public Task<bool> Update(Desk model);
    }
}
