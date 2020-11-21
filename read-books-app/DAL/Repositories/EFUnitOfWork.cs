using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
    {
        private ReadBooksContext db;
        private BookRepository bookRepository;
        private StatisticRepository statisticRepository;
        private UserRepository userRepository;
        private bool disposed = false;
        public EFUnitOfWork()
        {
            db = new ReadBooksContext();
        }
        public IRepository<Book> Books
        {
            get
            {
                if(bookRepository == null)
                {
                    bookRepository = new BookRepository(db);
                }
                return bookRepository;
            }
        }

        public IRepository<Statistic> Statistics
        {
            get
            {
                if (statisticRepository == null)
                {
                    statisticRepository = new StatisticRepository(db);
                }
                return statisticRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(db);
                }
                return userRepository;
            }
        }
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
