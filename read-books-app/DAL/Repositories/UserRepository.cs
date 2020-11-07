using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        private ReadBooksContext db;
        public UserRepository(ReadBooksContext context)
        {
            this.db = context;
        }
        public void Create(User item)
        {
            this.db.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = this.db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return this.db.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
            return this.db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return this.db.Users;
        }

        public void Update(User item)
        {
            this.db.Entry(item).State = EntityState.Modified;
        }
    }
}
