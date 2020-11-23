// <copyright file="IUnitOfWork.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// interface IUnitOfWork.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets books.
        /// </summary>
        IRepository<Book> Books { get; }

        /// <summary>
        ///  Gets statistics.
        /// </summary>
        IRepository<Statistic> Statistics { get; }

        /// <summary>
        /// Gets users.
        /// </summary>
        IRepository<User> Users { get; }

        /// <summary>
        /// Save.
        /// </summary>
        void Save();
    }
}
