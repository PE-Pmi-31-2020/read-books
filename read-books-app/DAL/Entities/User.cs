using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class User
    {
        public User()
        {
            Statistic = new HashSet<Statistic>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Statistic> Statistic { get; set; }
    }
}
