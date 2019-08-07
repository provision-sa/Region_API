using Region_API.Models;
using System;
using System.Collections.Generic;

namespace Region_API.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        List<TEntity> Get(Func<TEntity, bool> where);        
        void Insert(TEntity item);
        void Insert(IEnumerable<TEntity> items);
        void Remove(TEntity item);
        void Remove(IEnumerable<TEntity> items);
        void RemoveAtId(int item);
        void Update(TEntity item);
        void Save();
    }

    public interface ICityRepository : IRepository<City>
    {
        List<City> GetBy(string province);
    }

    public interface ISuburbRepository : IRepository<Suburb>
    {
        List<Suburb> GetBy(string province, string city);
    }
}
