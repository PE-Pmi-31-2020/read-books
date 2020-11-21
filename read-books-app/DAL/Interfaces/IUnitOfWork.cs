using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Statistic> Statistics { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
