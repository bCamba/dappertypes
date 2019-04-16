using Dapper;
using DapperTypes.Entities;
using System;
using System.IO;

namespace DapperTypes.Repositories
{
	public class CustomerRepository : BaseRepository
	{
		private const string SQL_GET_FULL_CUSTOMER_STARTING_WITH_ID_COL = @"
			SELECT
				customer.id AS 'Id',
				customer.number AS 'Number',
				customer.name AS 'Name',
				address.id AS 'Id',
				address.city AS 'City',
				address.street AS 'Street',
				address.number AS 'Number',
				address.zip AS 'Zip',
				state.id AS 'Id',
				state.name AS 'Name',
				country.id AS 'Id',
				country.name AS 'Name',
				account.id AS 'Id',
				account.amount AS 'Money'
			FROM customer
			INNER JOIN address ON address.id=customer.id_address
			INNER JOIN state ON state.id=address.id_state
			INNER JOIN country ON country.id=state.id_country
			INNER JOIN account ON account.id=customer.id_account
			WHERE
				customer.number=@customerNumber
		";

		private const string SQL_GET_FULL_CUSTOMER_MIXED_COLUMNS = @"
			SELECT
				customer.number AS 'Number',
				customer.id AS 'Id',
				customer.name AS 'Name',
				address.city AS 'City',
				address.street AS 'Street',
				address.id AS 'Id',
				address.number AS 'Number',
				address.zip AS 'Zip',
				state.id AS 'Id',
				state.name AS 'Name',
				country.name AS 'Name',
				country.id AS 'Id',
				account.amount AS 'Money',
				account.id AS 'Id'
			FROM customer
			INNER JOIN account ON account.id=customer.id_account
			INNER JOIN address ON address.id=customer.id_address
			INNER JOIN state ON state.id=address.id_state
			INNER JOIN country ON country.id=state.id_country
			WHERE
				customer.number=@customerNumber
		";

		private const string SPLIT_ON_GET_FULL_CUSTOMER_MIXED_COLUMNS = "City,Id,Name,Money";

		private void BuildDb()
		{
			//You may need to change these to your sql scripts paths.
			var createDbAndTablesSqlPath = "Database/CreateDbAndTables.sql";
			var populateCustomerTablePath = "Database/PopulateCustomerTable.sql";
			string fullScript = File.ReadAllText(createDbAndTablesSqlPath);

			Execute(fullScript);

			fullScript = File.ReadAllText(populateCustomerTablePath);
			Execute(fullScript);
		}

		public CustomerRepository(string connectionstring) : base(connectionstring)
		{
			BuildDb();
		}

		public Customer GetFullCustomerWithStartingIdCol(int number)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@customerNumber", number, System.Data.DbType.Int32);

			var types = new Type[]
			{
				typeof(Customer),
				typeof(Address),
				typeof(State),
				typeof(Country),
				typeof(Account)
			};

			return QueryFirstOrDefault<Customer>(SQL_GET_FULL_CUSTOMER_STARTING_WITH_ID_COL, types, GetFullCustomerMapping, parameters);
		}

		public Customer GetFullCustomerMixedStartingCol(int number)
		{
			var parameters = new DynamicParameters();
			parameters.Add("@customerNumber", number, System.Data.DbType.Int32);

			var types = new Type[]
			{
				typeof(Customer),
				typeof(Address),
				typeof(State),
				typeof(Country),
				typeof(Account)
			};

			return QueryFirstOrDefault<Customer>(SQL_GET_FULL_CUSTOMER_MIXED_COLUMNS, types, GetFullCustomerMapping, parameters, splitOn: SPLIT_ON_GET_FULL_CUSTOMER_MIXED_COLUMNS);
		}

		private Customer GetFullCustomerMapping(object[] objects)
		{
			Customer customer = objects[0] as Customer;
			Address address = objects[1] as Address;
			State state = objects[2] as State;
			Country country = objects[3] as Country;
			Account account = objects[4] as Account;

			state.Country = country;
			address.State = state;
			customer.Address = address;
			customer.Account = account;

			return customer;
		}
	}
}

