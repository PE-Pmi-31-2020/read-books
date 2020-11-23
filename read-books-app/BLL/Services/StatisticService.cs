// <copyright file="StatisticService.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AutoMapper;
    using BLL.DataTransferObjects;
    using BLL.Interfaces;
    using DAL;
    using DAL.Interfaces;
    using DAL.Repositories;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticService"/> class.
    /// </summary>
    public class StatisticService : IStatisticService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticService"/> class.
        /// </summary>
        public StatisticService()
        {
            this.DataBase = new EFUnitOfWork();
        }

        private IUnitOfWork DataBase { get; set; }

        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public void CreateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review)
        {
            User user = this.DataBase.Users.Get(userDTO.Id);
            this.CreateBook(bookDTO);
            Book book = this.DataBase.Books.Get(bookDTO.Id);
            Statistic statistic = new Statistic
            {
                UserId = user.Id,
                BookId = book.Id,
                ReadedPages = readedPages,
                Review = review,
            };
            this.DataBase.Statistics.Create(statistic);
            this.DataBase.Save();
        }

        /// <summary>
        /// Get books to read.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>List of books.</returns>
        public List<BookDTO> GetBooksToRead(int userId)
        {
            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>())
                .CreateMapper();
            List<BookDTO> allBooks = bookMapper.Map<IEnumerable<Book>, List<BookDTO>>(this.DataBase.Books.GetAll());

            var statisticMapper = new MapperConfiguration(cfg => cfg.CreateMap<Statistic, StatisticDTO>())
                .CreateMapper();
            List<StatisticDTO> allStatistic = statisticMapper.Map<IEnumerable<Statistic>, List<StatisticDTO>>(this.DataBase.Statistics.GetAll());

            List<BookDTO> booksToRead = new List<BookDTO>();
            foreach (var book in allBooks)
            {
                foreach (var statistic in allStatistic)
                {
                    if (book.Id == statistic.BookId && statistic.UserId == userId && book.Pages > statistic.ReadedPages)
                    {
                        booksToRead.Add(book);
                    }
                }
            }

            return booksToRead;
        }

        /// <summary>
        /// Get readed books.
        /// </summary>
        /// <param name="userId">User id.</param>
        /// <returns>List of books.</returns>
        public List<BookDTO> GetReadedBooks(int userId)
        {
            var bookMapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>())
                .CreateMapper();
            List<BookDTO> allBooks = bookMapper.Map<IEnumerable<Book>, List<BookDTO>>(this.DataBase.Books.GetAll());

            var statisticMapper = new MapperConfiguration(cfg => cfg.CreateMap<Statistic, StatisticDTO>())
                .CreateMapper();
            List<StatisticDTO> allStatistic = statisticMapper.Map<IEnumerable<Statistic>, List<StatisticDTO>>(this.DataBase.Statistics.GetAll());

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

        private void CreateBook(BookDTO bookDTO)
        {
            Book book = new Book
            {
                Author = bookDTO.Author,
                Name = bookDTO.Name,
                Pages = bookDTO.Pages,
            };
            this.DataBase.Books.Create(book);
            this.DataBase.Save();
        }
    }
}
