using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
   public class StatisticDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int ReadedPages { get; set; }
        public string Review { get; set; }
    }
}
