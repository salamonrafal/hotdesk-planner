using Core.Infrastructure;
using MongoDB.Driver;

namespace Infrastructure.Repositories
{
    class ReservationRepository<TClass> : IRepository<TClass> where TClass : class
    {
        private readonly IMongoDatabase _database;
        private readonly string _collection;

        public ReservationRepository(IMongoClient client)
        {
            _database = client.GetDatabase("hotdesk_planner");
            _collection = "reservations";
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
