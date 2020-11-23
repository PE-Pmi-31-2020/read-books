// <copyright file="UserRepository.cs" company="BakuninCompany">
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
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    internal class UserRepository : IRepository<User>
    {
        private ReadBooksContext db;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">context.</param>
        public UserRepository(ReadBooksContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="item">user.</param>
        public void Create(User item)
        {
            this.db.Users.Add(item);
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">user id.</param>
        public void Delete(int id)
        {
            User user = this.db.Users.Find(id);
            if (user != null)
            {
                this.db.Users.Remove(user);
            }
        }

        /// <summary>
        /// Find user.
        /// </summary>
        /// <param name="predicate">Predicate.</param>
        /// <returns>users.</returns>
        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return this.db.Users.Where(predicate).ToList();
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="id">id.</param>
        /// <returns>user.</returns>
        public User Get(int id)
        {
            return this.db.Users.Find(id);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>users.</returns>
        public IEnumerable<User> GetAll()
        {
            return this.db.Users;
        }

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="item">user.</param>
        public void Update(User item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
