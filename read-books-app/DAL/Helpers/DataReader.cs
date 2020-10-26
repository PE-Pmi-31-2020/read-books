using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Helpers
{
    class DataReader
    {
        public static void PrintUsers(NpgsqlConnection connection)
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

        public static void PrintBooks(NpgsqlConnection connection)
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

        public static void PrintStatistic(NpgsqlConnection connection)
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
    }
}
