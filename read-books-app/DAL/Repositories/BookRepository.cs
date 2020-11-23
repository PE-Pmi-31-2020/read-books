// <copyright file="BookRepository.cs" company="BakuninCompany">
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
    /// Initializes a new instance of the <see cref="BookRepository"/> class.
    /// </summary>
    internal class BookRepository : IRepository<Book>
    {
        private ReadBooksContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="context">cont.</param>
        public BookRepository(ReadBooksContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="item">book.</param>
        public void Create(Book item)
        {
            this.db.Books.Add(item);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id">id.</param>
        public void Delete(int id)
        {
            Book book = this.db.Books.Find(id);
            if (book != null)
            {
                this.db.Books.Remove(book);
            }
        }

        /// <summary>
        /// Find.
        /// </summary>
        /// <param name="predicate">predicate.</param>
        /// <returns>books.</returns>
        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            return this.db.Books.Where(predicate).ToList();
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>book.</returns>
        public Book Get(int id)
        {
            return this.db.Books.Find(id);
        }

        /// <summary>
        /// Get all books.
        /// </summary>
        /// <returns>books.</returns>
        public IEnumerable<Book> GetAll()
        {
            return this.db.Books;
        }

        /// <summary>
        /// update.
        /// </summary>
        /// <param name="item">book.</param>
        public void Update(Book item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
