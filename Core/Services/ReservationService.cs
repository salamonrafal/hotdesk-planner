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
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;

        public ReservationService(IRepository<Reservation> repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Add(Reservation model)
        {
            model.GenerateUuid();

            await _repository.Insert(model);

            return model.Id;
        }

        public async Task<Reservation> Get(Reservation model)
        {
            return await _repository.SelectOne(model);
        }

        public async Task<List<Reservation>> Get()
        {
            return await _repository.Select();
        }

        public async Task<bool> Remove(Reservation model)
        {
            return await _repository.Delete(model);
        }

        public async Task<List<Reservation>> Search(BsonDocument query)
        {
            return await _repository.Select(new QueryDocument(query));
        }

        public async Task<bool> Update(Reservation model)
        {
            return await _repository.Update(model);
        }
    }
}
