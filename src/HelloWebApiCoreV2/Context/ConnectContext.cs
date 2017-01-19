using HelloWebApiCoreV2.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HelloApiWithCoreDapper.Context
{
    public class ConnectContext
    {
        private ConnectContext() { }

        private static ConnectContext current;

        public static ConnectContext Current
        {
            get
            {
                if (current == null)
                {
                    current = new ConnectContext();
                }
                return current;
            }
        }
        private readonly string connStr = ApiContext.Current
         .Configuration["Data:DefaultConnection:ConnectionString"];

        public async Task<SqlConnection> GetOpenConnection()
        {
            SqlConnection connection = null;
            if (connection == null)
            {
                connection = new SqlConnection(connStr);
                await connection.OpenAsync();
            }
            else if (connection.State == System.Data.ConnectionState.Closed)
            {
                await connection.OpenAsync();
            }
            else if (connection.State == System.Data.ConnectionState.Broken)
            {
                 connection.Close();
                await connection.OpenAsync();
            }
            return connection;
        }

    }
}
