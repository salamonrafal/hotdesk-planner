using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public interface IRepository<TClass>
    {
        public int Insert(TClass data);
        public TClass Select(TClass data);
        public void Update(TClass data);
        public void Delete(TClass data);
    }
}
