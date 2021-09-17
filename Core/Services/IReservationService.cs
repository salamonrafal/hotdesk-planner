using Core.Models;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IReservationService
    {
        public Task<Reservation> Get(Reservation model);
        public Task<List<Reservation>> Get();
        public Task<List<Reservation>> Search(BsonDocument query);
        public Task<Guid> Add(Reservation model);
        public Task<bool> Remove(Reservation model);
        public Task<bool> Update(Reservation model);
    }
}
