using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Integration.Helpers
{
    public enum CompareOperator
    {
        Eq,
        Neq,
        Gt,
        Lt,
        Gte,
        Lte
    }

    public class InvalidCompare : Exception
    {
        public InvalidCompare(string message) : base(message)
        {
            
        }
    }
    
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

        public List<TClassOutput> FindItem(
            string field,
            string value
        )
        {
            var element = SelectItems(field, value);

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

        private IEnumerable<TClassOutput> SelectItems(string field, string value)
        {
            return _data
                .Where(item => FindElementInList(item, field, value))
                .Select(items => items);
        }
        
        private bool FindElementInList(
            TClassOutput item, 
            string field,
            string value,
            CompareOperator @operator = CompareOperator.Eq
        )
        {
            var property = item.GetType()
                .GetProperty(field)
                ?.GetValue(item);
            var type = item.GetType()
                .GetProperty(field)
                ?.PropertyType;

            return MakeLogicStringOperation<string>(property?.ToString(), value, @operator) &&
                   property?.ToString() != type?.ToString();
        }

        private bool MakeLogicStringOperation<TValueType>(TValueType property1, TValueType property2, CompareOperator @operator)
        {
            switch (@operator)
            {
                case CompareOperator.Eq:
                    return  EqualityComparer<TValueType>.Default.Equals(property1, property2);
                
                case CompareOperator.Gt:
                    if (typeof(int) != typeof(TValueType))
                        throw new InvalidCompare("Gt[e], Lt[e] available for int type");
                    
                    return (int)(object)property1 > (int)(object) property2;
   
                case CompareOperator.Lt:
                    if (typeof(int) != typeof(TValueType))
                        throw new InvalidCompare("Gt[e], Lt[e] available for int type");
                    
                    return (int)(object)property1 < (int)(object) property2;
                    
                case CompareOperator.Gte:
                    if (typeof(int) != typeof(TValueType))
                        throw new InvalidCompare("Gt[e], Lt[e] available for int type");
                    
                    return (int)(object)property1 >= (int)(object) property2;
                
                case CompareOperator.Lte:
                    if (typeof(int) != typeof(TValueType))
                        throw new InvalidCompare("Gt[e], Lt[e] available for int type");
                    
                    return (int)(object)property1 <= (int)(object) property2;
                
                case CompareOperator.Neq:
                    return  !EqualityComparer<TValueType>.Default.Equals(property1, property2);
                
                default:
                    throw new InvalidCompare("Unhandled compare operation");
            }
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