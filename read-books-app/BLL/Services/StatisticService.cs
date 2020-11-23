using AutoMapper;
using BLL.DataTransferObjects;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
   public class StatisticService : IStatisticService
    {
        IUnitOfWork DataBase { get; set; }
        private void CreateBook(BookDTO bookDTO)
        {
            Book book = new Book
            {
                Author = bookDTO.Author,
                Name = bookDTO.Name,
                Pages = bookDTO.Pages
            };
            DataBase.Books.Create(book);
            DataBase.Save();
        }
        public void CreateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review)
        {
            User user = DataBase.Users.Get(userDTO.Id);
            CreateBook(bookDTO);
            Book book = DataBase.Books.Get(bookDTO.Id);
            Statistic statistic = new Statistic
            {
                UserId = user.Id,
                BookId = book.Id,
                ReadedPages = readedPages,
                Review = review
            };
            DataBase.Statistics.Create(statistic);
            DataBase.Save();
        }
        public StatisticService()
        {
            DataBase = new EFUnitOfWork();
        }

        public void Dispose()
        {
            
        }

        public List<BookDTO> GetBooksToRead(int userId)
        {
            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>())
                .CreateMapper();
            List<BookDTO> allBooks = bookMapper.Map<IEnumerable<Book>, List<BookDTO>>(DataBase.Books.GetAll());

            var statisticMapper = new MapperConfiguration(cfg => cfg.CreateMap<Statistic, StatisticDTO>())
                .CreateMapper();
            List<StatisticDTO> allStatistic = statisticMapper.Map<IEnumerable<Statistic>, List<StatisticDTO>>(DataBase.Statistics.GetAll());

            List<BookDTO> booksToRead = new List<BookDTO>();
            foreach (var book in allBooks)
            {
                foreach (var statistic in allStatistic)
                {
                    if(book.Id == statistic.BookId && statistic.UserId == userId && book.Pages > statistic.ReadedPages)
                    {
                        booksToRead.Add(book);
                    }
                }
            }
            return booksToRead;
        }

        public List<BookDTO> GetReadedBooks(int userId)
        {
            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>())
                .CreateMapper();
            List<BookDTO> allBooks = bookMapper.Map<IEnumerable<Book>, List<BookDTO>>(DataBase.Books.GetAll());

            var statisticMapper = new MapperConfiguration(cfg => cfg.CreateMap<Statistic, StatisticDTO>())
                .CreateMapper();
            List<StatisticDTO> allStatistic = statisticMapper.Map<IEnumerable<Statistic>, List<StatisticDTO>>(DataBase.Statistics.GetAll());

            List<BookDTO> readedBooks = new List<BookDTO>();
            foreach (var book in allBooks)
            {
                foreach (var statistic in allStatistic)
                {
                    if (book.Id == statistic.BookId && statistic.UserId == userId && book.Pages == statistic.ReadedPages)
                    {
                        readedBooks.Add(book);
                    }
                }
            }
            return readedBooks;
        }
    }
}
