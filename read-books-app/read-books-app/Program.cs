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
        static void PrintUsers(NpgsqlConnection connection)
        {
            Console.WriteLine("\n\t\t\tUsers Table\n");
            connection.Open();

            var sql = "SELECT * FROM users";

            using var cmd = new NpgsqlCommand(sql, connection);

            using NpgsqlDataReader rdr = cmd.ExecuteReader();

            Console.WriteLine("{0,-4}    |    {1,-25}    |    {2,-25}\n", rdr.GetName(0), rdr.GetName(1),
                rdr.GetName(2));

            while (rdr.Read())
            {
                Console.WriteLine("{0,-4}    |    {1,-25}    |    {2,-25}", rdr.GetInt32(0), rdr.GetString(1),
                    rdr.GetString(2));
            }
            connection.Close();
        }

        static void PrintBooks(NpgsqlConnection connection)
        {
            Console.WriteLine("\n\t\t\t\tBooks Table\n");
            connection.Open();
            var sqlb = "SELECT * FROM books";
            using var cmdb = new NpgsqlCommand(sqlb, connection);
            using NpgsqlDataReader rdrb = cmdb.ExecuteReader();

            Console.WriteLine("{0,-5}    |    {1,-18}    |    {2,-12}    |    {3,-10}\n", rdrb.GetName(0), rdrb.GetName(1),
                rdrb.GetName(2), rdrb.GetName(3));

            while (rdrb.Read())
            {
                Console.WriteLine("{0,-5}    |    {1,-18}    |    {2,-12}    |    {3,-10}", rdrb.GetInt32(0), rdrb.GetString(1),
                    rdrb.GetString(2), rdrb.GetInt32(3));
            }
            connection.Close();
        }

        static void PrintStatistic(NpgsqlConnection connection)
        {
            Console.WriteLine("\n\t\t\t\t\tStatistic Table\n");
            connection.Open();
            var sqls = "SELECT * FROM statistic";
            using var cmds = new NpgsqlCommand(sqls, connection);
            using NpgsqlDataReader rdrs = cmds.ExecuteReader();

            Console.WriteLine("{0,-8}    |    {1,-8}    |    {2,-8}    |    {3,-15}    |    {4,-6}\n", rdrs.GetName(0), rdrs.GetName(1),
                rdrs.GetName(2), rdrs.GetName(3), rdrs.GetName(4));

            while (rdrs.Read())
            {
                Console.WriteLine("{0,-8}    |    {1,-8}    |    {2,-8}    |    {3,-15}    |    {4,-6}...", rdrs.GetInt32(0), rdrs.GetInt32(1),
                    rdrs.GetInt32(2), rdrs.GetInt32(3), rdrs.GetString(4).Substring(0, rdrs.GetString(4).Length / 20));
            }
            connection.Close();
        }
        static void Main(string[] args)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=joanne19!;Database=read-books-db";

            using var connection = new NpgsqlConnection(connectionString);

            //InsertData(connectionString, connection);

            PrintUsers(connection);
            PrintBooks(connection);
            PrintStatistic(connection);
        }
    }
}
