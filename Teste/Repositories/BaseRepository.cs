using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DapperTypes.Repositories
{
	public class BaseRepository
	{
		private readonly string _connectionString;

		public BaseRepository(string connectionstring)
		{
			_connectionString = connectionstring;
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection(_connectionString);
		}

		protected T QueryFirstOrDefault<T>(string sql, Type[] types, Func<object[], T> mapping, object parameters = null, string splitOn = "Id", int? timeOut = 10000)
		{
			using (var connection = CreateConnection())
			{
				return connection.Query<T>(sql, types, mapping, parameters, splitOn: splitOn, commandTimeout: timeOut).FirstOrDefault();
			}
		}

		protected List<T> Query<T>(string sql, Type[] types, Func<object[], T> mapping, object parameters = null, string splitOn = "Id", int? timeOut = 10000)
		{
			using (var connection = CreateConnection())
			{
				return connection.Query(sql, types, mapping, parameters, splitOn: splitOn, commandTimeout: timeOut).ToList();
			}
		}

		protected int Execute(string sql, object parameters = null)
		{
			using (var connection = CreateConnection())
			{
				return connection.Execute(sql, parameters);
			}
		}
	}
}
