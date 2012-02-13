using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Syringe.Tests
{
    [TestClass]
    public class SyringeContainerTests
    {
        [TestMethod]
        public void AnonymousRegistration()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>();
            IMathNode m = c.Resolve<IMathNode>();
            Assert.IsInstanceOfType(m, typeof(Zero));
            Assert.AreEqual(0, m.Calculate());
        }

        [TestMethod]
        public void NamedRegistration()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>("zero");
            IMathNode m = c.Resolve<IMathNode>("zero");
            Assert.IsInstanceOfType(m, typeof(Zero));
            Assert.AreEqual(0, m.Calculate());
        }

        [TestMethod]
        public void WithValue()
        {
            var c = new SyringeContainer();
            c.Register<IMathNode, Number>("five").WithValue("number", 5);
            int i = c.Resolve<IMathNode>("five").Calculate();
            Assert.AreEqual(5, i);
        }

        [TestMethod]
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

        [TestMethod]
        public void MultipleRegistration()
        {
            // Register two instances - by default fetching the first
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>();
            c.Register<IMathNode, Number>();

            int target = c.Resolve<IMathNode>().Calculate();
            Assert.AreEqual(0, target);
        }

        [TestMethod]
        public void MultipleRegistrationWithPreference()
        {
            // Register two instances - by default fetching the first
            int expected = 5;
            var c = new SyringeContainer();
            c.Register<IMathNode, Zero>();
            c.Register<IMathNode, Number>().WithValue("number", expected);
            
            int target = c.Resolve<IMathNode>().Calculate();
            Assert.AreEqual(expected, target);
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
