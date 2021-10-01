using Core.Infrastructure;
using Core.Models;
using Core.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository<TClass> : IRepository<TClass> where TClass : User, new()
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<TClass> _collection;
        private readonly DatabaseOptions _options;

        public UserRepository(IMongoClient client, IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
            _database = client.GetDatabase(_options.Database);
            _collection = _database.GetCollection<TClass>("users");
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

        private static UpdateDefinition<TClass> CreateUpdateDefinition(TClass model)
        {
            var update = new List<UpdateDefinition<TClass>>();

            if (model.Name != null)
                update.Add(Builders<TClass>.Update.Set("name", model.Name));

            if (model.Password != null)
                update.Add(Builders<TClass>.Update.Set("password", model.Password));

            if (model.Surname != null)
                update.Add(Builders<TClass>.Update.Set("surname", model.Surname));

            if (model.UrlAvatar != null)
                update.Add(Builders<TClass>.Update.Set("url_avatar", model.UrlAvatar));

            if (model.Email != null)
                update.Add(Builders<TClass>.Update.Set("email", model.Email));


            if (model.Role != null)
            {
               // update.Add(Builders<TClass>.Update.Set("is_periodical", model.IsPeriodical));
            }

            var combineUpdate = Builders<TClass>.Update.Combine(update);

            return combineUpdate;
        }
    }
}
