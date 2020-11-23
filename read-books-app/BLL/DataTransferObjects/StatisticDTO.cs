// <copyright file="StatisticDTO.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticDTO"/> class.
    /// </summary>
    public class StatisticDTO
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets book id.
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Gets or sets readed pages.
        /// </summary>
        public int ReadedPages { get; set; }

        /// <summary>
        /// Gets or sets review.
        /// </summary>
        public string Review { get; set; }
    }
}
