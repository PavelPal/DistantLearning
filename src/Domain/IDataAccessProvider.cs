using System.Collections.Generic;

namespace Domain
{
    public interface IDataAccessProvider<T> where T : class
    {
        void AddEventRecord(T record);
        void UpdateEventRecord(T record);
        void DeleteEventRecord(int recordId);
        T GetEventRecord(int recordId);
        IEnumerable<T> GetAllEventRecord();
    }
}