
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperationalTransform;
namespace TestProject1
{
    [TestClass]
    public class TextTransformTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            TextTransformCollection y = new TextTransformCollection("Hi");
            y.add(new TextTransformActor(2,"Hi There",DateTime.Now));
            Assert.AreEqual("HiHi There",y.CalculateConsolidatedString());
            //delete from it
            y.add(new TextTransformActor(0, 2, DateTime.Now));
            Assert.AreEqual("Hi There", y.CalculateConsolidatedString());
            y.add(new TextTransformActor(6, " You person", DateTime.Now));
            Assert.AreEqual("Hi There You person", y.consolidated);
        }
    }
}
