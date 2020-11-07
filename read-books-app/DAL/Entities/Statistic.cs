using System;
using System.Collections.Generic;

namespace DAL
{
    public partial class Statistic
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public int? ReadedPages { get; set; }
        public string Review { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
