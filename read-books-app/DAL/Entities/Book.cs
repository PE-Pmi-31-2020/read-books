// <copyright file="Book.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <see cref="Book"/> class.
    /// </summary>
    public partial class Book
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book()
        {
            this.Statistic = new HashSet<Statistic>();
        }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets pages.
        /// </summary>
        public int Pages { get; set; }

        /// <summary>
        /// Gets or sets Statistic.
        /// </summary>
        public virtual ICollection<Statistic> Statistic { get; set; }
    }
}
