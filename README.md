# dappertypes
 I was looking for examples of Dapper using its unlimited **Query** override and even though I found [an example which helped me to use it](https://stackoverflow.com/questions/10202584/using-dapper-to-map-more-than-5-types/17029996), I think a complete example could help someone lost on the internet. 
  This is a repository to show the implementation of a SQL query using dapper without restriction on the amount of types and subtypes in the query.
  
  The test application is in the repository and was built using **Visual Studio 2017 and .NET Core 2.2 with Dapper 1.60.6**.
  
  I used a [SQL Server in Docker](https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-docker?view=sql-server-2017&pivots=cs1-bash) to test.
  
  Imagina a database modelled as follows (*I know this is not the best way to model this database, but the purpose of this repository is to show the Dapper override only*):
  
  ![Database](/Images/DapperTypesDiagram.jpg)
  
  A customer has an identifier, a number (which is the ID the customer will see), a name, an address and an account. The account has an identifier and the amount of money the customer has. The address has an identifier, a city, a street, a number, a zipcode and a state. The state has an identifier, a name and a country. The country also has an identifier and a name.
 
 A query to get the full customer object could be done as follows:
 
 ```
 private const string SQL_QUERY = @"SELECT
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
	customer.number=@customerNumber"
```

 
 And the Dapper call to this query could be done as follows:
 
 

 ```
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

using (var conn = CreateConnection())
{
    return Query<Customer>(SQL_QUERY, types, GetFullCustomerMapping, parameters);
}
```