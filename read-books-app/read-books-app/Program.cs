using System;
using System.Linq;
using Npgsql;

namespace read_books_app
{
    class Program
    {
        static string EmailGenerator()
        {
            string[] names = { "bob", "alice", "alex", "tom", "emma", "ada", "olivia", "aiden", "finn", "anna" };
            string[] mails = { "gmail.com", "ukr.net", "outlook.com" };
            Random random = new Random();
            return $"{names[random.Next(names.Length)]}{random.Next(100)}@{mails[random.Next(mails.Length)]}";
        }
        static string AuthorGenerator()
        {
            Random random = new Random();
            string[] firstNames = { "Bob", "Alice", "Alex", "Tom", "Emma", "Ada", "Olivia", "Aiden", "Finn", "Anna" };
            string[] lastNames = { "White", "Yellow", "Orange", "Red", "Blue", "Pink", "Purple", "Brown", "Grey", "Black" };
            return $"{firstNames[random.Next(firstNames.Length)]} {lastNames[random.Next(lastNames.Length)]}";
        }
        static string PasswordGenerator()
        {
            Random random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, random.Next(8, 20))
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static int PagesGenerator()
        {
            Random random = new Random();
            return random.Next(5, 1000);
        }
        static void InsertData(string connectionString, NpgsqlConnection npgsql)
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

        }
        static void Main(string[] args)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=read-books-db";

            using var connection = new NpgsqlConnection(connectionString);

            InsertData(connectionString, connection);

            connection.Open();

            var sql = "SELECT * FROM users";
            

            using var cmd = new NpgsqlCommand(sql, connection);
           

            using NpgsqlDataReader rdr = cmd.ExecuteReader();
            Console.WriteLine($"{rdr.GetName(0),-4}\t {rdr.GetName(1),-10}\t {rdr.GetName(2),10}\t");

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0),-4}\t {rdr.GetString(1),-10}\t {rdr.GetString(2),-10}\t");
            }
            connection.Close();
            connection.Open();
            var sqlb = "SELECT * FROM books";
            using var cmdb = new NpgsqlCommand(sqlb, connection);
            using NpgsqlDataReader rdrb = cmdb.ExecuteReader();
            
            Console.WriteLine("\tBooks\t\n");
            Console.WriteLine($"{rdrb.GetName(0),-4}\t {rdrb.GetName(1),-10}\t {rdrb.GetName(2),10}\t {rdrb.GetName(3),-4}\t");

            while (rdrb.Read())
            {
                Console.WriteLine($"{rdrb.GetInt32(0),-4}\t {rdrb.GetString(1),-10}\t {rdrb.GetString(2),-10}\t {rdrb.GetInt32(3),-4}\t");
            }
            connection.Close();
            connection.Open();
            var sqls = "SELECT * FROM statistic";
            using var cmds = new NpgsqlCommand(sqls, connection);
            using NpgsqlDataReader rdrs = cmds.ExecuteReader();

            Console.WriteLine("\tStaticsic\t\n");
            Console.WriteLine($"{rdrs.GetName(0),-4}\t {rdrs.GetName(1),-4}\t {rdrs.GetName(2),-4}\t {rdrs.GetName(3),-4}\t {rdrb.GetName(4),10}\t");

            while (rdrs.Read())
            {
                Console.WriteLine($"{rdrs.GetInt32(0),-4}\t {rdrs.GetInt32(1),-4}\t {rdrs.GetInt32(2),-4}\t {rdrs.GetInt32(3),-4}\t {rdrb.GetString(4),-10}\t");
            }
        }
    }
}
