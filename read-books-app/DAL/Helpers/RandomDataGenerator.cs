// <copyright file="RandomDataGenerator.cs" company="BakuninCompany">
// Copyright (c) BakuninCompany. All rights reserved.
// </copyright>

namespace DAL.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Npgsql;

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomDataGenerator"/> class.
    /// </summary>
    internal class RandomDataGenerator
    {
        /// <summary>
        /// Insert Data.
        /// </summary>
        /// <param name="connectionString">connectionString.</param>
        /// <param name="npgsql">npgsql.</param>
        public static void InsertData(string connectionString, NpgsqlConnection npgsql)
        {
            string reviewTemplate = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Random random = new Random();
            int amountOfUsers = 40;
            int amountOfBooks = 35;
            int amountOfStatistic = 47;
            npgsql.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = npgsql;
            for (int i = 0; i < amountOfUsers; i++)
            {
                cmd.CommandText = $"INSERT INTO users(email, password) VALUES('{EmailGenerator()}','{PasswordGenerator()}')";
                cmd.ExecuteNonQuery();
            }

            for (int i = 0; i < amountOfBooks; i++)
            {
                cmd.CommandText = $"INSERT INTO books(author, name, pages) VALUES('{AuthorGenerator()}','Some name {i}', {PagesGenerator()})";
                cmd.ExecuteNonQuery();
            }

            for (int i = 0; i < amountOfStatistic; i++)
            {
                cmd.CommandText = $"INSERT INTO statistic(user_id, book_id, readed_pages, review) VALUES({random.Next(1, amountOfUsers)},{random.Next(1, amountOfBooks)}, 0, '{reviewTemplate}')";
                cmd.ExecuteNonQuery();
            }

            npgsql.Close();
        }

        private static string EmailGenerator()
        {
            string[] names = { "bob", "alice", "alex", "tom", "emma", "ada", "olivia", "aiden", "finn", "anna" };
            string[] mails = { "gmail.com", "ukr.net", "outlook.com" };
            Random random = new Random();
            return $"{names[random.Next(names.Length)]}{random.Next(100)}@{mails[random.Next(mails.Length)]}";
        }

        private static string AuthorGenerator()
        {
            Random random = new Random();
            string[] firstNames = { "Bob", "Alice", "Alex", "Tom", "Emma", "Ada", "Olivia", "Aiden", "Finn", "Anna" };
            string[] lastNames = { "White", "Yellow", "Orange", "Red", "Blue", "Pink", "Purple", "Brown", "Grey", "Black" };
            return $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
        }

        private static string PasswordGenerator()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(8, 20))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static int PagesGenerator()
        {
            Random random = new Random();
            return random.Next(5, 1000);
        }
    }
}
