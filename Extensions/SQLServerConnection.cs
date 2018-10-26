﻿using System;
using System.Data.SqlClient;

namespace Core.Extensions
{
    public class SqlServerConnection
    {
        private static String sConn = "Data Source=192.168.99.100;Initial Catalog=who-app;User ID=ct;Password=ct-2011;Max Pool Size=200;";
        private readonly SqlConnection conn = new SqlConnection(sConn);
        private SqlCommand comm = new SqlCommand();

        public SqlDataReader SqlServerConnect(string SqlString)
        {

            try {
                conn.Open();
                comm = new SqlCommand(SqlString, conn);

                return comm.ExecuteReader();
            }
            catch (Exception){
                return null;
            }
        }

        public int SqlServerUpdate(string SqlString)
        {
            try
            {
                SqlCommand command = new SqlCommand(SqlString, conn);
                command.Connection.Open();

                return Convert.ToInt16(command.ExecuteScalar());
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }
        }

        public string GetQueryString(string sString, string command, string sAdditionalString = "", bool AndJoin = true)
        {
            string query = "";
            string JOIN = " AND ";

            if (!AndJoin)
                JOIN = " OR ";

            char[] Seps = new [] { '.', ' ', '*', '-', '+', '&', '%', '/', '$', '#' };
            string[] MyInfo = sString.Split(Seps, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i <= (MyInfo.Length - 1); i++)
            {
                if (JOIN.Trim() == "OR" & !(MyInfo[i].Length > 1))
                    continue;

                if (query.Trim() == "")
                    query = " WHERE (" + command + " LIKE '%" + GetValidSqlString(MyInfo[i]) + "%'";
                else
                    query += JOIN + command + " LIKE '%" + GetValidSqlString(MyInfo[i]) + "%'";
            }

            if (query != "")
                query += ")";

            if (sAdditionalString != "")
            {
                if (query == "")
                    query = " WHERE " + sAdditionalString;
                else
                    query += " AND " + sAdditionalString;
            }

            return query;
        }

        private string GetValidSqlString(String query){
            return query.Replace("'", "''");
        }

    }
}
