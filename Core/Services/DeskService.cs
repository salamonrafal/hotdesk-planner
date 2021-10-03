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
    public class DeskService : IDeskService
    {
        private readonly IRepository<Desk> _repository;
        private readonly IValidator<Desk> _validator;

        public DeskService(IRepository<Desk> repository, IValidator<Desk> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Desk> Get(Desk model)
        {
            await ValidateModel (ValidationModelType.GetOne, model);
            
            return await _repository.SelectOne(model);
        }

        public async Task<List<Desk>> Get()
        {
            return await _repository.Select();
        }

        public async Task<Guid> Add(Desk model)
        {
            await ValidateModel (ValidationModelType.Insert, model);
            
            model.GenerateUuid();
            
            await _repository.Insert(model);

            return model.Id;
        }

        public async Task<bool> Update(Desk model)
        {
            await ValidateModel (ValidationModelType.Update, model);
            
            return await _repository.Update(model);
        }

        public void ChnageState(bool isBlocked, int deskId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Remove(Desk model)
        {
            await ValidateModel (ValidationModelType.Delete, model);
            return await _repository.Delete(model);
        }

        public async Task<List<Desk>> Search(BsonDocument query)
        {
            return await _repository.Select(new QueryDocument(query));
        }

        private async Task ValidateModel (ValidationModelType validationType, Desk model)
        {
            await _validator.ValidateAsync
            (
                instance: model,
                options: o =>
                {
                    o.IncludeRuleSets (Enum.GetName (typeof(ValidationModelType), validationType));
                    o.ThrowOnFailures ();
                }
            );
        }
    }
}
