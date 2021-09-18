using Core.Infrastructure;
using Core.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.ApplicationFactories.Mocks
{
    internal class ReservationRepositoryMock : IRepository<Reservation>
    {
        public Task<bool> Delete(Reservation model)
        {
            throw new NotImplementedException();
        }

        public Task Insert(Reservation model)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reservation>> Select(QueryDocument query)
        {
            throw new NotImplementedException();
        }

        public Task<List<Reservation>> Select()
        {
            throw new NotImplementedException();
        }

        public Task<Reservation> SelectOne(Reservation model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Reservation model)
        {
            throw new NotImplementedException();
        }
    }
}
