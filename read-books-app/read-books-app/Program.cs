using System;
using Npgsql;

namespace read_books_app
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=read-books-db";

            using var connection = new NpgsqlConnection(connectionString);

            connection.Open();

            //var sql = "SELECT * FROM users";

            //using var cmd = new NpgsqlCommand(sql, connection);

            //using NpgsqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
            //            rdr.GetString(2));
            //}
        }
    }
}
