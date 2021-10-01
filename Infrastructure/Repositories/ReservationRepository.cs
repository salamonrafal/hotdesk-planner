using Core.Infrastructure;
using Core.Models;
using Core.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReservationRepository<TClass> : IRepository<TClass> where TClass : Reservation, new()
    {
        private readonly IMongoCollection<TClass> _collection;

        public ReservationRepository(IMongoClient client, IOptions<DatabaseOptions> options)
        {
            var options1 = options.Value;
            var database = client.GetDatabase(options1.Database);
            _collection = database.GetCollection<TClass>("reservations");
        }

        public async Task<bool> Delete(TClass model)
        {
            var filter = Builders<TClass>.Filter.Eq(x => x.Id, model.Id);

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

            return await data.ToListAsync();
        }

        public async Task<List<TClass>> Select(QueryDocument query)
        {
            return await _collection.Find(query).Sort ("{id: -1}").ToListAsync();
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

            if (model.StartDate != null)
                update.Add(Builders<TClass>.Update.Set("start_date", model.StartDate));

            if (model.EndDate != null)
                update.Add(Builders<TClass>.Update.Set("end_date", model.EndDate));

            if (model.IsPeriodical != null)
                update.Add(Builders<TClass>.Update.Set("is_periodical", model.IsPeriodical));

            if (model.PeriodicDetail != null)
            {
                // ToDo: fill this
            }

            var combineUpdate = Builders<TClass>.Update.Combine(update);

            return combineUpdate;
        }
    }
}
