using System;
using System.Data.SqlClient;
using Core.Extensions;
using Core.Models;

namespace Core.Services
{
    public class GlobalService
    {
        public GlobalProperties GetGlobalProperties(int idnt)
        {
            GlobalProperties properties = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT gp_idnt, gp_item, gp_value, gp_description FROM GlobalProperties WHERE gp_idnt=" + idnt);
            if (dr.Read())
            {
                properties = new GlobalProperties
                {
                    Id = Convert.ToInt16(dr[0]),
                    Item = dr[1].ToString(),
                    Value = dr[2].ToString(),
                    Description = dr[3].ToString()
                };
            }

            return properties;
        }

        public GlobalProperties GetGlobalProperties(string item)
        {
            GlobalProperties properties = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT gp_idnt, gp_item, gp_value, gp_description FROM GlobalProperties WHERE gp_item='" + item + "'");
            if (dr.Read())
            {
                properties = new GlobalProperties
                {
                    Id = Convert.ToInt16(dr[0]),
                    Item = dr[1].ToString(),
                    Value = dr[2].ToString(),
                    Description = dr[3].ToString()
                };
            }

            return properties;
        }
    }
}
