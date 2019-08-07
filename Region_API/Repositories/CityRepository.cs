using Microsoft.EntityFrameworkCore;
using Region_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Region_API.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly DBContext dBContext;

        public CityRepository(DBContext _dBContext)
        {
            dBContext = _dBContext;
        }

        public List<City> Get(Func<City, bool> where)
        {
            return dBContext.Cities.Where(where).ToList();
        }

        public List<City> GetAll()
        {
            return dBContext.Cities.ToList();
        }

        public List<City> GetBy(string province)
        {
            var provObj = dBContext.Provinces.Where(p => p.Description == province).First();
            if (provObj != null)
                return dBContext.Cities.Where(c => c.ProvinceId == provObj.Id).OrderBy(c => c.Description).ToList();
            else
                return null;
        }

        public void Insert(City item)
        {
            dBContext.Cities.Add(item);
            Save();
        }

        public void Insert(IEnumerable<City> items)
        {
            foreach (var item in items)
            {
                dBContext.Cities.Add(item);
                Save();
            }
        }

        public void Remove(City item)
        {
            dBContext.Cities.Remove(item);
            Save();
        }

        public void Remove(IEnumerable<City> items)
        {
            foreach (var item in items)
            {
                dBContext.Cities.Remove(item);
            }
            Save();
        }

        public void RemoveAtId(int item)
        {
            var city = Get(x => x.Id == item).FirstOrDefault();
            if (city != null)
            {
                dBContext.Cities.Remove(city);
                Save();
            }
        }

        public void Save()
        {
            dBContext.SaveChanges();
        }

        public void Update(City item)
        {
            dBContext.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
