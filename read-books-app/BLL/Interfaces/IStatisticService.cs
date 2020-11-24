// <copyright file="IStatisticService.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BLL.DataTransferObjects;

    /// <summary>
    /// Initializes a new instance of the <see cref="IStatisticService"/> class.
    /// </summary>
    public interface IStatisticService
    {
        /// <summary>
        /// Create statistic.
        /// </summary>
        /// <param name="bookDTO">book.</param>
        /// <param name="userDTO">user.</param>
        /// <param name="readedPages">number of pages.</param>
        /// <param name="review">book review.</param>
        void CreateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review);

        /// <summary>
        /// Get Readed Books method.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>readed books.</returns>
        List<BookDTO> GetReadedBooks(int userId);

        /// <summary>
        /// Get Books To Read method.
        /// </summary>
        /// <param name="userId">user id.</param>
        /// <returns>books to read.</returns>
        List<BookDTO> GetBooksToRead(int userId);

        /// <summary>
        /// Dispose.
        /// </summary>
        void Dispose();
        void DeleteStatistic(UserDTO userDTO, string bookNameToLookFor);
        void UpdateStatistic(BookDTO bookDTO, UserDTO userDTO, int readedPages, string review, string bookNameToLookFor);
    }
}
