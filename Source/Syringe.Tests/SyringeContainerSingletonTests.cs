using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace Syringe.Tests
{
	[TestFixture]
    public class SyringeContainerSingletonTests
    {
        public static SyringeContainer staticContainer { get; set; }

		[SetUp]
        public static void MyClassInitialize() 
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
			var target = staticContainer.Resolve<IMathNode>();

			// assert
			target.Should ().BeOfType (typeof(IMathNode));
			target.Calculate ().Should ().Be (0);
        }
    }
}
