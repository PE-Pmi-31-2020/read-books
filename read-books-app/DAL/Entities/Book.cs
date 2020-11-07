using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Book
    {
        public Book()
        {
            Statistic = new HashSet<Statistic>();
        }

        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }

        public virtual ICollection<Statistic> Statistic { get; set; }
    }
}
