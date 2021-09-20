using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Integration.Helpers
{
    public class VirtualDbJsonLoader<TClassOutput> : IVirtualDbJsonLoader<TClassOutput>
    {
        private readonly string _dbJsonPath;
        private List<TClassOutput> _data;

        public VirtualDbJsonLoader(string dbJsonPath)
        {
            _dbJsonPath = dbJsonPath;
            _data = new List<TClassOutput>();
            Load();
        }

        public List<TClassOutput> GetData()
        {
            return _data;
        }

        public List<TClassOutput> FindItem(string field,
            string value)
        {
            var element = _data.Where(item =>
                {
                    var property = item.GetType()
                        .GetProperty(field)
                        ?.GetValue(item);
                    var type = item.GetType()
                        .GetProperty(field)
                        ?.PropertyType;

                    return property?.ToString() == value &&
                           property?.ToString() != type?.ToString();
                })
                .Select(items => items);

            return element.ToList();
        }

        public bool FindAndDeleteItem(string field,
            string value)
        {
            throw new NotImplementedException();
        }

        public bool FindAndUpdateItem(string field,
            string value)
        {
            throw new NotImplementedException();
        }

        public bool InsertItem(TClassOutput data)
        {
            _data.Add(data);

            return true;
        }

        private async void Load()
        {
            try
            {
                var fileContent = await File.ReadAllTextAsync(_dbJsonPath,
                    Encoding.UTF8);
                _data = JsonConvert.DeserializeObject<List<TClassOutput>>(fileContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}