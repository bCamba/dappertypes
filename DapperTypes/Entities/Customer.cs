using System;

namespace DapperTypes.Entities
{
	public class Customer
	{
		public Guid Id { get; set; }
		public int Number { get; set; }
		public string Name { get; set; }
		public Address Address { get; set; }
		public Account Account { get; set; }
	}
}
