// <copyright file="StatisticService.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            Book book = this.DataBase.Books.Get(this.GetBookId(bookDTO));
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

        public void CreateUser(UserDTO userDTO)
        {
            User user = new User
            {
                Email = userDTO.Email,
                Password = userDTO.Password
            };
            this.DataBase.Users.Create(user);
            this.DataBase.Save();
        }

        public void DeleteStatistic(UserDTO userDTO, string bookNameToLookFor)
        {
            User user = this.DataBase.Users.Get(this.GetUserId(userDTO));
            Book bookToDelete = this.DataBase.Books.Get(this.GetBookId(bookNameToLookFor));
            Statistic statistic = this.DataBase.Statistics.Get(this.GetStatisticId(user.Id, bookToDelete.Id));
            this.DataBase.Statistics.Delete(statistic.Id);
            this.DataBase.Books.Delete(bookToDelete.Id);
            this.DataBase.Save();
        }

        public void UpdateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review, string bookNameToLookFor)
        {
            User user = this.DataBase.Users.Get(this.GetUserId(userDTO));
            Book bookToReplace = this.DataBase.Books.Get(this.GetBookId(bookNameToLookFor));
            Statistic statistic = this.DataBase.Statistics.Get(this.GetStatisticId(user.Id, bookToReplace.Id));
            bookToReplace.Name = bookDTO.Name;
            bookToReplace.Author = bookDTO.Author;
            bookToReplace.Pages = bookDTO.Pages;
            this.DataBase.Save();
            statistic.Book = bookToReplace;
            statistic.ReadedPages = readedPages;
            statistic.Review = review;
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

        private int GetBookId(BookDTO bookDTO)
        {
            int bookId = this.DataBase.Books
                .Find(b => b.Author == bookDTO.Author
                && b.Name == bookDTO.Name
                && b.Pages == bookDTO.Pages).First().Id;
            return bookId;
        }

        private int GetBookId(string bookName)
        {
            int bookId = this.DataBase.Books
                .Find(b => b.Name == bookName).First().Id;
            return bookId;
        }

        private int GetUserId(UserDTO userDTO)
        {
            int userId = this.DataBase.Users
                .Find(u => u.Email == userDTO.Email
                && u.Password == userDTO.Password).First().Id;
            return userId;
        }

        private int GetStatisticId(int userId, int bookId)
        {
            int statisticId = this.DataBase.Statistics
                .Find(s => s.BookId == bookId
                && s.UserId == userId).First().Id;
            return statisticId;
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
