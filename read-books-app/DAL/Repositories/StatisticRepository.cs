using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    class StatisticRepository : IRepository<Statistic>
    {
        private ReadBooksContext db;
        public StatisticRepository(ReadBooksContext context)
        {
            this.db = context;
        }
        public void Create(Statistic item)
        {
            this.db.Statistic.Add(item);
        }

        public void Delete(int id)
        {
            Statistic statistic = this.db.Statistic.Find(id);
            if (statistic != null)
                this.db.Statistic.Remove(statistic);
        }

        public IEnumerable<Statistic> Find(Func<Statistic, bool> predicate)
        {
            return this.db.Statistic.Where(predicate).ToList();
        }

        public Statistic Get(int id)
        {
            return this.db.Statistic.Find(id);
        }

        public IEnumerable<Statistic> GetAll()
        {
            return this.db.Statistic;
        }

        public void Update(Statistic item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
