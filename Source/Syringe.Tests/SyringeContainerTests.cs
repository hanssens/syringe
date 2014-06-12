using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;
using NUnit.Framework;
using FluentAssertions;

namespace Syringe.Tests
{
	[TestFixture]
    public class SyringeContainerTests
    {
		[Test]
		public void SyringeContainer_Should_Resolve_AnonymousRegistration()
        {
			// arrange
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>();

			// act
			var target = c.Resolve<IMathNode>();

			// assert
			target.Should ().BeOfType <Zero> ();
			target.Calculate ().Should ().Be (0);
        }

		[Test]
		public void SyringeContainer_Should_Resolve_NamedRegistration()
        {
			// arrange
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>("zero");

			// act
			var target = c.Resolve<IMathNode>("zero");

			// assert
			target.Should ().BeOfType <Zero> ();
			target.Calculate ().Should ().Be (0);
        }

		[Test]
        public void WithValue()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Number>("five").WithValue("number", 5);
            int i = c.Resolve<IMathNode>("five").Calculate();
            Assert.AreEqual(5, i);
        }

		[Test]
        public void WithValueAnonymous()
        {
            for (int i = 0; i < 3; i++)
            {
                var c = new SyringeContainer();
                c.Register<IMathNode, Number>().WithValue("number", i);
                int target = c.Resolve<IMathNode>().Calculate();
                Assert.AreEqual(i, target);
            }
        }

		[Test]
		public void SyringeContainer_Should_Allow_MultipleRegistration_And_Fetch_First_ByDefault()
        {
			// arrange
			int expected = 0;
			var c = new SyringeContainer();
            
			// act: register two instances - by default fetching the first
            c.Register<IMathNode, Zero>();
            c.Register<IMathNode, Number>();
			var target = c.Resolve<IMathNode>().Calculate();

			// assert:
			Assert.AreEqual(expected, target);
        }

		[Test]
		public void SyringeContainer_Should_Allow_MultipleRegistration_WithPreference()
        {
			// arrange
			int expected = 0;
            var c = new SyringeContainer();

			// act: register two instances - by default expecting the first
            c.Register<IMathNode, Zero>();
            c.Register<IMathNode, Number>().WithValue("number", expected);
			var target = c.Resolve<IMathNode>();

			// assert
			target.Should ().BeOfType <Zero> ();
			target.Calculate ().Should ().Be (expected);
        }


        /*
        [TestMethod]
        public void AnonymousSubDependency()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>();
            c.Register<IFormatter, MathFormatter>();
            IFormatter m = c.Resolve<IFormatter>();
            Assert.AreEqual("$0.00", m.Format("C2"));
        }

        [TestMethod]
        public void NamedSubDependency()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Number>("five").WithValue("number", 5);
            c.Register<IMathNode, Number>("six").WithValue("number", 6);
            c.Register<IMathNode, Add>("add").WithDependency("m1", "five").WithDependency("m2", "six");
            int i = c.Resolve<IMathNode>("add").Calculate();
            Assert.AreEqual(11, i);
        }
        */
    }
}
