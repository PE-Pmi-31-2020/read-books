// <copyright file="UserDTO.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace BLL.DataTransferObjects
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserDTO"/> class.
    /// </summary>
    public class UserDTO
    {
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
    }
}
