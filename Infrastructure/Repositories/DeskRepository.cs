using Core.Infrastructure;
using Core.Models;
using Core.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DeskRepository<TClass> : IRepository<TClass> where TClass : Desk, new()
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TClass> _collection;
        private readonly DatabaseOptions _options;

        public DeskRepository(IMongoClient client, IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
            _database = client.GetDatabase(_options.Database);
            _collection = _database.GetCollection<TClass>("desks");
        }

        public async Task<bool> Delete(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq(x => x.Id, model.Id);
            
            if (model != null)
                await _collection.DeleteOneAsync(filter);

            return true;
        }

        public async Task Insert(TClass model)
        {
            await _collection.InsertOneAsync(model);
        }

        public async Task<TClass> SelectOne(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq(x => x.Id, model.Id);

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<TClass>> Select()
        {
            var data = await _collection.FindAsync<TClass>(FilterDefinition<TClass>.Empty);

            return await data.ToListAsync<TClass>();
        }

        public async Task<List<TClass>> Select(QueryDocument query)
        {
            return await _collection.Find(query).ToListAsync<TClass>();
        }

        public async Task<bool> Update(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq(x => x.Id, model.Id);

            if (model != null)
            {
                var p = await _collection.UpdateOneAsync(filter, CreateUpdateDefinition(model));

                return true;
            }
            else
            {
                return false;
            }
        }

        private UpdateDefinition<TClass> CreateUpdateDefinition(TClass model)
        {
            var update = new List<UpdateDefinition<TClass>>();

            if (model.Description != null)
                update.Add(Builders<TClass>.Update.Set("description", model.Description));

            if (model.Localization != null)
            {
                if (model.Localization.Floor != null)
                    update.Add(Builders<TClass>.Update.Set("localization.floor", model.Localization.Floor));

                if (model.Localization.Outbuilding != null)
                    update.Add(Builders<TClass>.Update.Set("localization.outbuilding", model.Localization.Outbuilding));

                if (model.Localization.Coordination != null)
                {
                    if (model.Localization.Coordination.X != null)
                        update.Add(Builders<TClass>.Update.Set("localization.coordination.x", model.Localization.Coordination.X));

                    if (model.Localization.Coordination.Y != null)
                        update.Add(Builders<TClass>.Update.Set("localization.coordination.y", model.Localization.Coordination.Y));
                }

            }

            if (model.IsBlocked != null)
                update.Add(Builders<TClass>.Update.Set("is_blocked", model.IsBlocked));

            var combineUpdate = Builders<TClass>.Update.Combine(update);

            return combineUpdate;
        }
    }
}
