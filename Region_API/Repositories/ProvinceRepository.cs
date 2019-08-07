using Microsoft.EntityFrameworkCore;
using Region_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Region_API.Repositories
{
    public class ProvinceRepository : IRepository<Province>
    {
        private readonly DBContext dBContext;

        public ProvinceRepository(DBContext _dBContext)
        {
            dBContext = _dBContext;
        }

        public List<Province> Get(Func<Province, bool> where)
        {
            return dBContext.Provinces.Where(where).ToList();
        }

        public List<Province> GetAll()
        {
            return dBContext.Provinces.OrderBy(p => p.Description).ToList();
        }
        
        public void Insert(Province item)
        {
            dBContext.Provinces.Add(item);
            Save();
        }

        public void Insert(IEnumerable<Province> items)
        {            
            foreach (var item in items)
            {
                dBContext.Provinces.Add(item);
                Save();
            }            
        }

        public void Remove(Province item)
        {
            dBContext.Provinces.Remove(item);
            Save();
        }

        public void Remove(IEnumerable<Province> items)
        {
            foreach (var item in items)
            {
                dBContext.Provinces.Remove(item);
            }
            Save();
        }

        public void RemoveAtId(int item)
        {
            var province = Get(x => x.Id == item).FirstOrDefault();
            if (province != null)
            {
                dBContext.Provinces.Remove(province);
                Save();
            }
        }

        public void Save()
        {
            dBContext.SaveChanges();
        }

        public void Update(Province item)
        {
            dBContext.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
