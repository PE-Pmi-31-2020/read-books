// <copyright file="EFUnitOfWork.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Interfaces;

    /// <summary>
    /// Initializes a new instance of the <see cref="EFUnitOfWork"/> class.
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private ReadBooksContext db;
        private BookRepository bookRepository;
        private StatisticRepository statisticRepository;
        private UserRepository userRepository;
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFUnitOfWork"/> class.
        /// </summary>
        public EFUnitOfWork()
        {
            this.db = new ReadBooksContext();
        }

        /// <summary>
        /// Gets books.
        /// </summary>
        public IRepository<Book> Books
        {
            get
            {
                if (this.bookRepository == null)
                {
                    this.bookRepository = new BookRepository(this.db);
                }

                return this.bookRepository;
            }
        }

        /// <summary>
        /// Gets statistic.
        /// </summary>
        public IRepository<Statistic> Statistics
        {
            get
            {
                if (this.statisticRepository == null)
                {
                    this.statisticRepository = new StatisticRepository(this.db);
                }

                return this.statisticRepository;
            }
        }

        /// <summary>
        /// Gets users.
        /// </summary>
        public IRepository<User> Users
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new UserRepository(this.db);
                }

                return this.userRepository;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        /// <param name="disposing">disposing.</param>
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.db.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Save.
        /// </summary>
        public void Save()
        {
            this.db.SaveChanges();
        }
    }
}
