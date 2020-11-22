using BLL.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    interface IStatisticService
    {
        void CreateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review);
        List<BookDTO> GetReadedBooks(int userId);
        List<BookDTO> GetBooksToRead(int userId);
        void Dispose();
    }
}
