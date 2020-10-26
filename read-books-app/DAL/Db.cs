using DAL.Helpers;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class Db
    {
        public static void Run()
        {
            var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=read-books-db";

            using var connection = new NpgsqlConnection(connectionString);

            //RandomDataGenerator.InsertData(connectionString, connection);

            DataReader.PrintUsers(connection);
            DataReader.PrintBooks(connection);
            DataReader.PrintStatistic(connection);
        }
    }
}
