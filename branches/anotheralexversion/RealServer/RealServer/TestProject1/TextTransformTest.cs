namespace TestProject1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using OperationalTransform;

    [TestClass]
    public class TextTransformTest
    {
        #region Methods

        [TestMethod]
        public void TestMethod1()
        {
            TextTransformCollection y = new TextTransformCollection("Hi");
            y.Add(new TextTransformActor(2,"Hi There"));
            
            Assert.AreEqual("HiHi There",y.CalculateConsolidatedString());
            //delete from it
            y.Add(new TextTransformActor(0, 2));
            Assert.AreEqual("Hi There", y.CalculateConsolidatedString());
            y.Add(new TextTransformActor(6, " You person"));
            Assert.AreEqual("Hi There You person", y.consolidated);
        }

        #endregion Methods
    }
}