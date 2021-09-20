using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.Helpers
{
    public interface IVirtualDbJsonLoader<TOutputClass>
    {
        public List<TOutputClass> GetData();
        public List<TOutputClass> FindItem(string field, string value);
        public bool FindAndDeleteItem(string field, string value);
        public bool FindAndUpdateItem(string field, string value);
        public bool InsertItem(TOutputClass data);
    }
}