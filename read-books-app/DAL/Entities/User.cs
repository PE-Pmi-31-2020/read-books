// <copyright file="User.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    public partial class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.Statistic = new HashSet<Statistic>();
        }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets statistic.
        /// </summary>
        public virtual ICollection<Statistic> Statistic { get; set; }
    }
}
