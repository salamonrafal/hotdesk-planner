#nullable enable
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
        private readonly IMongoCollection<TClass> _collection;

        public DeskRepository(IMongoClient client, IOptions<DatabaseOptions> options)
        {
            var options1 = options.Value;
            var database = client.GetDatabase(options1.Database);
            _collection = database.GetCollection<TClass>("desks");
        }

        public async Task<bool> Delete(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq (x => x.Id, model.Id);
            await _collection.DeleteOneAsync (filter);

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

            return await data.ToListAsync();
        }

        public async Task<List<TClass>> Select(QueryDocument query)
        {
            return await _collection.Find(query).ToListAsync();
        }

        public async Task<bool> Update(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq(x => x.Id, model.Id);
            
            await _collection.UpdateOneAsync(filter, CreateUpdateDefinition(model));

            return true;
        }

        private static UpdateDefinition<TClass> CreateUpdateDefinition(TClass model)
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
