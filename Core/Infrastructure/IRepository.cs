using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface IRepository<TClass>
    {
        public Task Insert(TClass model);
        public Task<TClass> SelectOne(TClass model);
        public Task<List<TClass>> Select(QueryDocument query);
        public Task<List<TClass>> Select();
        public Task<bool> Update(TClass model);
        public Task<bool> Delete(TClass model);
    }
}
