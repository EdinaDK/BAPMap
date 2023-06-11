using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BAPMap.Model;
namespace BAPMap
{
    public static class Repository
    {
        public static Context db = new Context();
        private static string conn = "Data Source=CitiesData.db";
        internal static string GetConnectionString() => conn;
        public static void LoadBase()
        {
            db.Database.EnsureCreated();
            db.Cities.Load();
        }
        public static void SaveUpdates(IEnumerable<City> list)
        {
            db.Cities.UpdateRange(list);
            db.SaveChanges();
        }
        public static void RefreshUpdates()
        {
            foreach (var entry in db.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }
        public static City? AddCity(City city)
        {
            db.Cities.Add(city);
            db.SaveChanges();
            return db.Cities.FirstOrDefault(c => c.ID == city.ID);
        }
        public static City? RemoveCity(City city)
        {
            var result = db.Cities.FirstOrDefault(x => x.ID == city.ID);
            if (result != null)
                db.Cities.Remove(result);
            db.SaveChanges();
            return result;
        }
    }
}