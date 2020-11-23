using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.DataTransferObjects
{
   public class BookDTO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int Pages { get; set; }
    }
}
