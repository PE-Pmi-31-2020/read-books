// <copyright file="Statistic.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <see cref="Statistic"/> class.
    /// </summary>
    public partial class Statistic
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user id.
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Gets or sets book id.
        /// </summary>
        public int? BookId { get; set; }

        /// <summary>
        /// Gets or sets readed pages.
        /// </summary>
        public int? ReadedPages { get; set; }

        /// <summary>
        /// Gets or sets review.
        /// </summary>
        public string Review { get; set; }

        /// <summary>
        /// Gets or sets book.
        /// </summary>
        public virtual Book Book { get; set; }

        /// <summary>
        /// Gets or sets user.
        /// </summary>
        public virtual User User { get; set; }
    }
}
