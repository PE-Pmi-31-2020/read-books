// <copyright file="StatisticRepository.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DAL.Interfaces;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticRepository"/> class.
    /// </summary>
    internal class StatisticRepository : IRepository<Statistic>
    {
        private ReadBooksContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticRepository"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public StatisticRepository(ReadBooksContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="item">item.</param>
        public void Create(Statistic item)
        {
            this.db.Statistic.Add(item);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">id.</param>
        public void Delete(int id)
        {
            Statistic statistic = this.db.Statistic.Find(id);
            if (statistic != null)
            {
                this.db.Statistic.Remove(statistic);
            }
        }

        /// <summary>
        /// Find.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>List of stats.</returns>
        public IEnumerable<Statistic> Find(Func<Statistic, bool> predicate)
        {
            return this.db.Statistic.Where(predicate).ToList();
        }

        /// <summary>
        /// Get stats.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>stats.</returns>
        public Statistic Get(int id)
        {
            return this.db.Statistic.Find(id);
        }

        /// <summary>
        /// Get all stats.
        /// </summary>
        /// <returns>stats.</returns>
        public IEnumerable<Statistic> GetAll()
        {
            return this.db.Statistic;
        }

        /// <summary>
        /// Update stats.
        /// </summary>
        /// <param name="item">new stats.</param>
        public void Update(Statistic item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
