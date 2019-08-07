using Microsoft.EntityFrameworkCore;
using Region_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Region_API.Repositories
{
    public class SuburbRepository : ISuburbRepository
    {
        private readonly DBContext dBContext;

        public SuburbRepository(DBContext _dBContext)
        {
            dBContext = _dBContext;
        }

        public List<Suburb> Get(Func<Suburb, bool> where)
        {
            return dBContext.Suburbs.Where(where).ToList();
        }

        public List<Suburb> GetAll()
        {
            return dBContext.Suburbs.ToList();
        }

        public List<Suburb> GetBy(string province, string city)
        {
            var provObj = dBContext.Provinces.Where(p => p.Description == province).FirstOrDefault();
            var cityObj = dBContext.Cities.Where(c => c.ProvinceId == provObj.Id && c.Description == city).FirstOrDefault();
            if (cityObj != null)
                return dBContext.Suburbs.Where(s => s.CityId == cityObj.Id).OrderBy(s => s.Description).ToList();
            else
                return null;
        }

        public void Insert(Suburb item)
        {
            dBContext.Suburbs.Add(item);
            Save();
        }

        public void Insert(IEnumerable<Suburb> items)
        {
            foreach (var item in items)
            {
                dBContext.Suburbs.Add(item);
                Save();
            }
        }

        public void Remove(Suburb item)
        {
            dBContext.Suburbs.Remove(item);
            Save();
        }

        public void Remove(IEnumerable<Suburb> items)
        {
            foreach (var item in items)
            {
                dBContext.Suburbs.Remove(item);
            }
            Save();
        }

        public void RemoveAtId(int item)
        {
            var suburb = Get(x => x.Id == item).FirstOrDefault();
            if (suburb != null)
            {
                dBContext.Suburbs.Remove(suburb);
                Save();
            }
        }

        public void Save()
        {
            dBContext.SaveChanges();
        }

        public void Update(Suburb item)
        {
            dBContext.Entry(item).State = EntityState.Modified;
            Save();
        }
    }
}
