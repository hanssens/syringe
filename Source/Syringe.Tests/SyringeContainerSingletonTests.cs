using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Syringe.Tests
{
	[TestFixture]
    public class SyringeContainerSingletonTests
    {
        public static SyringeContainer staticContainer { get; set; }

		[SetUp]
        public static void MyClassInitialize(TestContext testContext) 
        { 
            // Initialize the Singleton
            staticContainer = new SyringeContainer();
            staticContainer.Register<IMathNode, Zero>("zero").AsSingleton();
        }

		[TearDown]
        public static void MyClassCleanup() 
        { 
            // TODO: Dispose()
        }

		[Test]
        public void AnonymousSingletonRegistration()
        {
            //staticContainer.Register<IMathNode, Zero>();
            var m = staticContainer.Resolve<IMathNode>();
			Assert.IsInstanceOf (typeof(IMathNode), m);
            Assert.AreEqual(0, m.Calculate());
        }
    }
}
