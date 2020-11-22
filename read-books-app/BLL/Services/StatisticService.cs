using BLL.DataTransferObjects;
using BLL.Interfaces;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    class StatisticService : IStatisticService
    {
        IUnitOfWork DataBase { get; set; }
        public void CreateStatistic(BookDTO bookDTO, UserDTO userDTO)
        {
            throw new NotImplementedException();
        }
        public StatisticService(IUnitOfWork unitOfWork)
        {
            DataBase = unitOfWork;
        }

        public void Dispose()
        {
            
        }

        public List<BookDTO> GetBooksToRead(int userId)
        {
            throw new NotImplementedException();
        }

        public List<BookDTO> GetReadedBooks(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
