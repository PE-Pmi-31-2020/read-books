// <copyright file="BookDTO.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookDTO"/> class.
    /// </summary>
    public class BookDTO
    {
        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets Author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///  Gets or sets Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Pages.
        /// </summary>
        public int Pages { get; set; }
    }
}
