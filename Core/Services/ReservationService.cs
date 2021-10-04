using Core.Helpers;
using Core.Infrastructure;
using Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Enums;
using FluentValidation;

namespace Core.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IRepository<Reservation> _repository;
        private readonly IValidator<Reservation> _validator;

        public ReservationService(IRepository<Reservation> repository, IValidator<Reservation> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Guid> Add(Reservation model)
        {
            await ServiceExtensions.ValidateModel (_validator, ValidationModelType.Insert, model);
            
            model.GenerateUuid();

            await _repository.Insert(model);

            return model.Id;
        }

        public async Task<Reservation> Get(Reservation model)
        {
            await ServiceExtensions.ValidateModel (_validator, ValidationModelType.GetOne, model);
            
            return await _repository.SelectOne(model);
        }

        public async Task<List<Reservation>> Get()
        {
            return await _repository.Select();
        }

        public async Task<bool> Remove(Reservation model)
        {
            await ServiceExtensions.ValidateModel (_validator, ValidationModelType.Delete, model);
            
            return await _repository.Delete(model);
        }

        public async Task<List<Reservation>> Search(BsonDocument query)
        {
            return await _repository.Select(new QueryDocument(query));
        }

        public async Task<bool> Update(Reservation model)
        {
            await ServiceExtensions.ValidateModel (_validator, ValidationModelType.Update, model);
            
            return await _repository.Update(model);
        }
    }
}
