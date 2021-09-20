using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Infrastructure;
using MongoDB.Driver;

namespace Integration.ApplicationFactories.Mocks
{
    public class RepositoryMock<TClass>: IRepository<TClass>
    {
        
        public async Task Insert(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<TClass> SelectOne(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TClass>> Select(QueryDocument query)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<TClass>> Select()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Update(TClass model)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> Delete(TClass model)
        {
            throw new System.NotImplementedException();
        }
    }
}