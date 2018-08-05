using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace SeeSharpTools.JY.Database
{
    /// <summary> 
    /// <para>Universal database access class library that encapsulates many operations on common databases.</para>
    /// <para>Chinese Simplified: 通用数据库访问类，封装了对常见数据库的多种操作。</para>
    /// </summary>  
    public sealed class DbOperation
    {
        /// <summary>
        /// <para>Database connection string</para>
        /// <para>Chinese Simplified: 数据库连接字符串</para>
        /// </summary>
        private string ConnectionString;

        /// <summary>
        /// <para>Database factory class</para>
        /// <para>Chinese Simplified: 数据库工厂类</para>
        /// </summary>
        private DbProviderFactory providerFactory;

        /// <summary>  
        /// <para>Database operation class</para>
        /// <para>Chinese Simplified: 数据库连接字符串</para>
        /// </summary>  
        /// <param name="connectionString">
        /// <para>database connection string</para>  
        /// <param>Chinese Simplified: 数据库连接字符串</param>  
        /// </param>
        /// <param name="providerType">
        /// <para>Database type enum</para>  
        /// <param>Chinese Simplified: 数据库类型枚举</param>  
        /// </param>
        public DbOperation(string connectionString, DbProviderType providerType)
        {
            ConnectionString = connectionString;
            providerFactory = ProviderFactory.GetDbProviderFactory(providerType);
            if (providerFactory == null)
            {
                throw new ArgumentException("Can't load DbProviderFactory for given value of providerType");
            }
        }


        /// <summary>     
        /// <para>UPDATE/INSTER/DELETE operations,if the return value which indicates number of affcted rows is not zero, operation is successful.</para>
        /// <para>Chinese Simplified: 对数据库执行增删改操作，返回受影响的行数，如果返回值大于0表明操作成功。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>UPDATE/INSTER/DELETE SQL statements</para>
        /// <para>Chinese Simplified: 要执行的增删改操作的SQL语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行增删改操作的SQL语句所需要的参数</para>
        /// </param>
        public int ExecuteNonQuery(string sql, IList<DbParameter> parameters)
        {
            return ExecuteNonQuery(sql, parameters, CommandType.Text);
        }


        /// <summary>     
        /// <para>Execute UPDATE/INSTER/DELETE operations,if the return value which indicates number of affcted rows is not zero, operation is successful.</para>
        /// <para>Chinese Simplified: 对数据库执行增删改操作，返回受影响的行数，如果返回值大于0表明操作成功。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>UPDATE/INSTER/DELETE SQL statements</para>
        /// <para>Chinese Simplified: 要执行的增删改操作的SQL语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行增删改操作的SQL语句所需要的参数</para>
        /// </param> 
        ///  <param name="commandType">
        /// <para>CommandType enum that includes Text/StoredProcedure/TableDirect</para>
        /// <para>Chinese Simplified: 执行的SQL语句的类型，包括SQL子句/存储过程/表语句</para>
        /// </param>   
        public int ExecuteNonQuery(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            using (DbCommand command = CreateDbCommand(sql, parameters, commandType))
            {
                command.Connection.Open();
                int affectedRows = command.ExecuteNonQuery();
                command.Connection.Close();
                return affectedRows;
            }
        }


        /// <summary>     
        /// <para>Execute query operations and return a related DataReader object</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个关联的DataReader实例。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param>
        public DbDataReader ExecuteReader(string sql, IList<DbParameter> parameters)
        {
            return ExecuteReader(sql, parameters, CommandType.Text);
        }


        /// <summary>     
        /// <para>Execute query operations and return a related DataReader object</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个关联的DataReader实例。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param>  
        /// <param name="commandType">
        /// <para>CommandType enum that includes Text/StoredProcedure/TableDirect</para>
        /// <para>Chinese Simplified: 执行的SQL语句的类型，包括SQL子句/存储过程/表语句</para>
        /// </param> 
        public DbDataReader ExecuteReader(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            DbCommand command = CreateDbCommand(sql, parameters, commandType);
            command.Connection.Open();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }


        /// <summary>     
        /// <para>Execute query operations and return a DataTable object</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个数据表对象。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param>
        public DataTable ExecuteDataTable(string sql, IList<DbParameter> parameters)
        {
            return ExecuteDataTable(sql, parameters, CommandType.Text);
        }


        /// <summary>     
        /// <para>Execute query operations and return a DataTable object</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个数据表对象。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param>
        /// <param name="commandType">
        /// <para>CommandType enum that includes Text/StoredProcedure/TableDirect</para>
        /// <para>Chinese Simplified: 执行的SQL语句的类型，包括SQL子句/存储过程/表语句</para>
        /// </param> 
        public DataTable ExecuteDataTable(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            using (DbCommand command = CreateDbCommand(sql, parameters, commandType))
            {
                using (DbDataAdapter adapter = providerFactory.CreateDataAdapter())
                {
                    adapter.SelectCommand = command;
                    DataTable data = new DataTable();
                    adapter.Fill(data);
                    return data;
                }
            }
        }


        /// <summary>     
        /// <para>Execute query operations and return a result or a column.</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个结果或者一列数据。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param>    
        public Object ExecuteScalar(string sql, IList<DbParameter> parameters)
        {
            return ExecuteScalar(sql, parameters, CommandType.Text);
        }


        /// <summary>     
        /// <para>Execute query operations and return a result or a column.</para>
        /// <para>Chinese Simplified: 执行一个查询语句，返回一个结果或者一列数据。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param> 
        /// <param name="commandType">
        /// <para>CommandType enum that includes Text/StoredProcedure/TableDirect</para>
        /// <para>Chinese Simplified: 执行的SQL语句的类型，包括SQL子句/存储过程/表语句</para>
        /// </param> 
        public Object ExecuteScalar(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            using (DbCommand command = CreateDbCommand(sql, parameters, commandType))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                command.Connection.Close();
                return result;
            }
        }


        /// <summary>     
        /// <para>Create a DbCommand object.</para>
        /// <para>Chinese Simplified: 创建一个DbCommand对象。</para>     
        /// </summary>     
        /// <param name="sql">
        /// <para>query SQL statements</para>
        /// <para>Chinese Simplified: 要执行的查询语句</para>
        /// </param>  
        /// <param name="parameters">
        /// <para>parameters that SQL starments require</para>
        /// <para>Chinese Simplified: 执行SQL查询语句所需要的参数</para>
        /// </param> 
        /// <param name="commandType">
        /// <para>CommandType enum that includes Text/StoredProcedure/TableDirect</para>
        /// <para>Chinese Simplified: 执行的SQL语句的类型，包括SQL子句/存储过程/表语句</para>
        /// </param> 
        private DbCommand CreateDbCommand(string sql, IList<DbParameter> parameters, CommandType commandType)
        {
            DbConnection connection = providerFactory.CreateConnection();
            DbCommand command = providerFactory.CreateCommand();
            connection.ConnectionString = ConnectionString;
            command.CommandText = sql;
            command.CommandType = commandType;
            command.Connection = connection;
            if (!(parameters == null || parameters.Count == 0))
            {
                foreach (DbParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
    }


    /// <summary>  
    /// 数据库类型枚举  
    /// </summary>  
    public enum DbProviderType : byte
    {
        /// <summary>
        /// Microsoft SQL Server
        /// </summary>
        SqlServer,

        /// <summary>
        /// Oracle MySQL 
        /// </summary>
        MySQL,

        /// <summary>
        /// SQLite
        /// </summary>
        SQLite,

        /// <summary>
        /// Oracle Database
        /// </summary>
        Oracle,

        /// <summary>
        /// ODBC Database
        /// </summary>
        ODBC,

        /// <summary>
        /// OleDb Database
        /// </summary>
        OleDb,

        /// <summary>
        /// Firebird Database
        /// </summary>
        Firebird,

        /// <summary>
        /// PostgreSql Database
        /// </summary>
        PostgreSql,

        /// <summary>
        /// DB2 Database
        /// </summary>
        DB2,

        /// <summary>
        /// Informix Database
        /// </summary>
        Informix,

        /// <summary>
        /// SQL Server CE Database
        /// </summary>
        SqlServerCe
    }


    /// <summary>  
    /// DbProviderFactory工厂类  
    /// </summary>  
    internal class ProviderFactory
    {
        private static Dictionary<DbProviderType, string> providerInvariantNames = new Dictionary<DbProviderType, string>();
        private static Dictionary<DbProviderType, DbProviderFactory> providerFactoies = new Dictionary<DbProviderType, DbProviderFactory>(20);
        static ProviderFactory()
        {
            //加载已知的数据库访问类的程序集  
            providerInvariantNames.Add(DbProviderType.SqlServer, "System.Data.SqlClient");
            providerInvariantNames.Add(DbProviderType.OleDb, "System.Data.OleDb");
            providerInvariantNames.Add(DbProviderType.ODBC, "System.Data.ODBC");
            providerInvariantNames.Add(DbProviderType.Oracle, "Oracle.DataAccess.Client");
            providerInvariantNames.Add(DbProviderType.MySQL, "MySql.Data.MySqlClient");
            providerInvariantNames.Add(DbProviderType.SQLite, "System.Data.SQLite");
            providerInvariantNames.Add(DbProviderType.Firebird, "FirebirdSql.Data.Firebird");
            providerInvariantNames.Add(DbProviderType.PostgreSql, "Npgsql");
            providerInvariantNames.Add(DbProviderType.DB2, "IBM.Data.DB2.iSeries");
            providerInvariantNames.Add(DbProviderType.Informix, "IBM.Data.Informix");
            providerInvariantNames.Add(DbProviderType.SqlServerCe, "System.Data.SqlServerCe");
        }


        /// <summary>  
        /// 获取指定数据库类型对应的程序集名称  
        /// </summary>  
        /// <param name="providerType">数据库类型枚举</param>  
        /// <returns></returns>  
        private static string GetProviderInvariantName(DbProviderType providerType)
        {
            return providerInvariantNames[providerType];
        }


        /// <summary>  
        /// 获取指定类型的数据库对应的DbProviderFactory  
        /// </summary>  
        /// <param name="providerType">数据库类型枚举</param>  
        /// <returns></returns>  
        internal static DbProviderFactory GetDbProviderFactory(DbProviderType providerType)
        {
            //如果还没有加载，则加载该DbProviderFactory  
            if (!providerFactoies.ContainsKey(providerType))
            {
                providerFactoies.Add(providerType, ImportDbProviderFactory(providerType));
            }
            return providerFactoies[providerType];
        }


        /// <summary>  
        /// 加载指定数据库类型的DbProviderFactory  
        /// </summary>  
        /// <param name="providerType">数据库类型枚举</param>  
        /// <returns></returns>  
        private static DbProviderFactory ImportDbProviderFactory(DbProviderType providerType)
        {
            string providerName = providerInvariantNames[providerType];
            DbProviderFactory factory = null;
            try
            {
                //从全局程序集中查找  
                factory = DbProviderFactories.GetFactory(providerName);
            }
            catch (ArgumentException)
            {
                factory = null;
            }
            return factory;
        }
    }
}
