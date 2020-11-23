// <copyright file="Db.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using DAL.Helpers;
    using Npgsql;

    /// <summary>
    /// Initializes a new instance of the <see cref="Db"/> class.
    /// </summary>
    internal class Db
    {
        /// <summary>
        /// Run database.
        /// </summary>
        public static void Run()
        {
            var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=read-books-db";

            using var connection = new NpgsqlConnection(connectionString);

            // RandomDataGenerator.InsertData(connectionString, connection);
            DataReader.PrintUsers(connection);
            DataReader.PrintBooks(connection);
            DataReader.PrintStatistic(connection);
        }
    }
}
