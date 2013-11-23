using NUnit.Framework;
using System;

namespace Syringe.Tests
{
	[TestFixture ()]
	public class CustomerServiceTests
	{
		/*
		 * Note: these integration tests are temporary
		 */

		[Test ()]
		public void CompanyServiceTest ()
		{
			var container = new SyringeContainer();
			container.Register<ICustomerService, CompanyService> ("CustomerService").AsSingleton ();

			var targetId = 1001;
			var target = container.Resolve<ICustomerService> ().Retrieve (targetId);
			Assert.IsNotNull (target);
			Assert.IsTrue (target.Id == targetId);
		}

		[Test ()]
		public void PersonServiceTest ()
		{
			var container = new SyringeContainer();
			container.Register<ICustomerService, PersonService> ("CustomerService").AsSingleton ();

			var targetId = 1002;
			var target = container.Resolve<ICustomerService> ().Retrieve (targetId);
			Assert.IsNotNull (target);
			Assert.IsTrue (target.Id == targetId);
		}

		[Test ()]
		public void BothCompanyAndPersonServiceTest ()
		{
			var container = new SyringeContainer();
			container.Register<ICustomerService, CompanyService> ("CompanyService").AsSingleton ();
			container.Register<ICustomerService, PersonService> ("PersonService").AsSingleton ();

			// by default, the first will be used e.g. 'companyservice'
			var targetId = 1001;
			var target = container.Resolve<ICustomerService> ().Retrieve (targetId);
			Assert.IsNotNull (target);
			Assert.IsTrue (target.Id == targetId);

			// fetch explicitely through PersonService
			var secondTargetId = 1002;
			var secondTarget = container.Resolve<ICustomerService> ("PersonService").Retrieve (secondTargetId);
			Assert.IsNotNull (secondTarget);
			Assert.IsTrue (secondTarget.Id == secondTargetId);
		}
	}
}

