using DapperTypes.Entities;
using DapperTypes.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MainClass
{
	//You may need to edit this connectionString variable to your DB connection string.
	private readonly static string _connectionString = @"Data Source=192.168.99.100,1433;Initial Catalog=dapper_types;User ID=sa;Password=forwarDash11!;";

	[TestMethod]
	public void Test()
	{
		var customerRepository = new CustomerRepository(_connectionString);
		Customer customerUsingQueryStartingWithIdCol = customerRepository.GetFullCustomerWithStartingIdCol(7);
		Customer customerUsingQueryStartingWithMixedCols = customerRepository.GetFullCustomerMixedStartingCol(7);

		AreEqual(customerUsingQueryStartingWithIdCol, customerUsingQueryStartingWithMixedCols);
	}

	private static void AreEqual(Customer customerUsingQueryStartingWithIdCol, Customer customerUsingQueryStartingWithMixedCols)
	{
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Id == customerUsingQueryStartingWithMixedCols.Id);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Name == customerUsingQueryStartingWithMixedCols.Name);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Number == customerUsingQueryStartingWithMixedCols.Number);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Account.Id == customerUsingQueryStartingWithMixedCols.Account.Id);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Account.Money == customerUsingQueryStartingWithMixedCols.Account.Money);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.Id == customerUsingQueryStartingWithMixedCols.Address.Id);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.City == customerUsingQueryStartingWithMixedCols.Address.City);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.Number == customerUsingQueryStartingWithMixedCols.Address.Number);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.Street == customerUsingQueryStartingWithMixedCols.Address.Street);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.Zip == customerUsingQueryStartingWithMixedCols.Address.Zip);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.State.Id == customerUsingQueryStartingWithMixedCols.Address.State.Id);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.State.Name == customerUsingQueryStartingWithMixedCols.Address.State.Name);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.State.Country.Id == customerUsingQueryStartingWithMixedCols.Address.State.Country.Id);
		Assert.IsTrue(customerUsingQueryStartingWithIdCol.Address.State.Country.Name == customerUsingQueryStartingWithMixedCols.Address.State.Country.Name);
	}
}