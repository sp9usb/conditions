using System;
using System.Globalization;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Conditions.Tests.StringTests
{
    /// <summary>
    /// Tests the ValidatorExtensions.EndsWith method.
    /// </summary>
    [TestClass]
    public class StringEndsWithTests
    {
        [TestMethod]
        [Description("Calling EndsWith on string x with 'x EndsWith x' should pass.")]
        public void EndsWithTest01()
        {
            string a = "test";
            Condition.Requires(a).EndsWith(a);
        }

        [TestMethod]
        [Description("Calling EndsWith on string x (\"test\") with 'x EndsWith \"est\"' should pass.")]
        public void EndsWithTest02()
        {
            string a = "test";
            Condition.Requires(a).EndsWith("est");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling EndsWith on string x (\"test\") with 'x EndsWith null' should fail.")]
        public void EndsWithTest03()
        {
            string a = "test";
            // A null value will never be found
            Condition.Requires(a).EndsWith(null);
        }

        [TestMethod]
        [Description("Calling EndsWith on string x (\"test\") with 'x EndsWith \"\"' should pass.")]
        public void EndsWithTest04()
        {
            string a = "test";
            // An empty string will always be found
            Condition.Requires(a).EndsWith(String.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        [Description("Calling EndsWith on string x (null) with 'x EndsWith \"\"' should fail.")]
        public void EndsWithTest05()
        {
            string a = null;
            // A null string only contains other null strings.
            Condition.Requires(a).EndsWith(String.Empty);
        }

        [TestMethod]
        [Description("Calling EndsWith on string x (null) with 'x EndsWith null' should pass.")]
        public void EndsWithTest06()
        {
            string a = null;
            Condition.Requires(a).EndsWith(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        [Description("Calling EndsWith on string x (\"test\") with 'x EndsWith \"me test\"' should fail.")]
        public void EndsWithTest07()
        {
            string a = "test";
            Condition.Requires(a).EndsWith("me test");
        }

        [TestMethod]
        [Description(
            "Calling EndsWith on string x (\"test\") with 'x EndsWith \"test me\"' should fail with a correct exception message."
            )]
        public void EndsWithTest08()
        {
            string expectedMessage =
                "a should end with 'test me'." + Environment.NewLine +
                TestHelper.CultureSensitiveArgumentExceptionParameterText + ": a";

            try
            {
                string a = "test";
                Condition.Requires(a, "a").EndsWith("test me");
            }
            catch (Exception ex)
            {
                Assert.AreEqual(expectedMessage, ex.Message);
            }
        }

        [TestMethod]
        [Description("Calling EndsWith with conditionDescription parameter should pass.")]
        public void EndsWithTest09()
        {
            string a = null;
            Condition.Requires(a).EndsWith(null, string.Empty);
        }

        [TestMethod]
        [Description(
            "Calling a failing EndsWith should throw an Exception with an exception message that contains the given parameterized condition description argument."
            )]
        public void EndsWithTest10()
        {
            string a = "test";
            try
            {
                Condition.Requires(a, "a").EndsWith("test me", "qwe {0} xyz");
                Assert.Fail();
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("qwe a xyz"));
            }
        }

        [TestMethod]
        [Description("Calling EndsWith should be language dependent.")]
        public void StartsWithTest11()
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;

            string a = "hello and hi";

            try
            {
                Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

                // We check this using the Turkish-I problem.
                // see: http://msdn.microsoft.com/en-us/library/ms973919.aspx#stringsinnet20_topic5
                string turkishUpperCase = "Hİ";

                Condition.Requires(a).EndsWith(turkishUpperCase, StringComparison.CurrentCultureIgnoreCase);
            }
            finally
            {
                Thread.CurrentThread.CurrentCulture = originalCulture;
            }
        }

        [TestMethod]
        [Description(
            "Calling EndsWith on string x (\"test\") with 'x EndsWith null' should succeed when exceptions are suppressed."
            )]
        public void StartsWithTest12()
        {
            string a = "test";
            // A null value will never be found
            Condition.Requires(a).SuppressExceptionsForTest().EndsWith(null);
        }
    }
}