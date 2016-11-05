using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccessProvider
{
    public class DataAccessProvider<T> : IDataAccessProvider<T> where T : class
    {
        private readonly DomainModelContext _context;
        private readonly ILogger _logger;

        public DataAccessProvider(DomainModelContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessProvider");
        }

        public void AddEventRecord(T record)
        {
            try
            {
                _context.Set<T>().Add(record);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void UpdateEventRecord(T record)
        {
            try
            {
                _context.Entry(record).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public void DeleteEventRecord(int recordId)
        {
            try
            {
                var item = _context.Set<T>().Find(recordId);
                if (item != null)
                    _context.Set<T>().Remove(item);
                _context.SaveChanges();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public T GetEventRecord(int recordId)
        {
            try
            {
                return _context.Set<T>().Find(recordId);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }

        public IEnumerable<T> GetAllEventRecord()
        {
            try
            {
                return _context.Set<T>();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                throw;
            }
        }
    }
}