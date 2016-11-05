using System.Collections.Generic;

namespace Domain
{
    public interface IDataAccessProvider<T> where T : class
    {
        void Add(T record);
        void Update(T record);
        void Delete(int recordId);
        T Get(int recordId);
        IEnumerable<T> GetAll();
    }
}