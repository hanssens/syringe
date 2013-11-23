using System;

namespace Syringe.Tests
{
	public interface ICustomerService
	{
		Customer Retrieve(int id);
	}



	public class CompanyService : ICustomerService
	{
		#region ICustomerService implementation

		public Customer Retrieve (int id)
		{
			return new Customer () {
				Id = 1001,
				Name = "ACME Inc."
			};
		}

		#endregion
	}

	public class PersonService : ICustomerService
	{
		#region ICustomerService implementation

		public Customer Retrieve (int id)
		{
			return new Customer () {
				Id = 1002,
				Name = "Harry Potter"
			};
		}

		#endregion
	}
}

