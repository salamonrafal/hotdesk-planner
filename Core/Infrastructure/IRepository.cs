﻿namespace Core.Infrastructure
{
    public interface IRepository<TClass> where TClass : class
    {
        public int Insert(TClass data);
        public TClass Select(TClass data);
        public void Update(TClass data);
        public void Delete(TClass data);
    }
}
