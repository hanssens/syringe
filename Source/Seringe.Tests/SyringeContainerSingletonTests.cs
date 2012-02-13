using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Syringe.Tests
{
    [TestClass]
    public class SyringeContainerSingletonTests
    {
        public static SyringeContainer staticContainer { get; set; }

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        { 
            // Initialize the Singleton
            staticContainer = new SyringeContainer();
            staticContainer.Register<IMathNode, Zero>("zero").AsSingleton();
        }

        [ClassCleanup()]
        public static void MyClassCleanup() 
        { 
            // TODO: Dispose()
        }

        [TestMethod]
        public void AnonymousSingletonRegistration()
        {
            //staticContainer.Register<IMathNode, Zero>();
            var m = staticContainer.Resolve<IMathNode>();
            Assert.IsInstanceOfType(m, typeof(Zero));
            Assert.AreEqual(0, m.Calculate());
        }
    }
}
