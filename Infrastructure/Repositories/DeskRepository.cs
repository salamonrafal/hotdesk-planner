using Core.Infrastructure;
using Core.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    public class DeskRepository<TClass> : IRepository<TClass> where TClass : class
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;
        private readonly DatabaseOptions _options;

        public DeskRepository(IMongoClient client, IOptions<DatabaseOptions> options)
        {
            _options = options.Value;
            _database = client.GetDatabase(_options.Database);
            _collection = "desks";
        }

        public void Delete(TClass data)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(TClass data)
        {
            throw new System.NotImplementedException();
        }

        public TClass Select(TClass data)
        {
            throw new System.NotImplementedException();
        }

        public void Update(TClass data)
        {
            throw new System.NotImplementedException();
        }
    }
}
