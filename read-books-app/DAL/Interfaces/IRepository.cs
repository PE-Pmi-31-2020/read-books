// <copyright file="IRepository.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// interface IRepository.
    /// </summary>
    /// <typeparam name="T">T.</typeparam>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns> all.</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get id.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>el id.</returns>
        T Get(int id);

        /// <summary>
        /// Find.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>rep.</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Create item.
        /// </summary>
        /// <param name="item">item.</param>
        void Create(T item);

        /// <summary>
        /// Update item.
        /// </summary>
        /// <param name="item">item.</param>
        void Update(T item);

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">id.</param>
        void Delete(int id);
    }
}
