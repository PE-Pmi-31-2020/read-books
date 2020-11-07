using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    class BookRepository : IRepository<Book>
    {
        private ReadBooksContext db;
        public BookRepository(ReadBooksContext context)
        {
            this.db = context;
        }
        public void Create(Book item)
        {
            this.db.Books.Add(item);
        }

        public void Delete(int id)
        {
            Book book = this.db.Books.Find(id);
            if (book != null)
                this.db.Books.Remove(book);
        }

        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            return this.db.Books.Where(predicate).ToList();
        }

        public Book Get(int id)
        {
            return this.db.Books.Find(id);
        }

        public IEnumerable<Book> GetAll()
        {
            return this.db.Books;
        }

        public void Update(Book item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
