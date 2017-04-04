using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Task1Logic;

namespace CustomerTests
{
    [TestFixture]
    public class CustomerTest
    {
        private Customer customer;

        [SetUp]
        public void Initialize()
        {
            customer = new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);
        }

        [Test]
        [TestCase(Result = "Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100")]
        public string CustomerToString_Test()
        {
            return customer.ToString();
        }

        [Test]
        [TestCase(Result = "1 000 000,00 ₽")]
        public string CustomerToString_RuFormatProviderTest()
        {
            return customer.ToString("R", new CultureInfo("ru-RU"));
        }

        [Test]
        [TestCase(Result = "1.000.000,00 €")]
        public string CustomerToString_DeFormatProviderTest()
        {
            return customer.ToString("R", new CultureInfo("de-DE"));
        }

        [Test]
        [TestCase(Result = "Jeffrey Richter")]
        public string CustomerToString_NameTest()
        {
            return customer.ToString("N");
        }

        [Test]
        [TestCase(Result = "Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100")]
        public string CustomerToString_NullProviderTest()
        {
            return customer.ToString("G", null);
        }

        [Test]
        [TestCase(Result = "Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100")]
        public string CustomerToString_NullTest()
        {
            return customer.ToString(null, null);
        }

        [Test]
        [TestCase(Result = "Customer record: Jeffrey Richter, $1,000,000.00")]
        public string CustomerToString_CustomerFormatProviderTest()
        {
            return customer.ToString("NR", new CustomerFormatProvider());
        }

        [Test]
        [TestCase(Result = "+1 (425) 555-0100, $1,000,000.00")]
        public string CustomerToString_NullFormatProviderTest()
        {
            return customer.ToString("PR", null);
        }

        public IEnumerable<TestCaseData> CustomerFormattingTestData
        {
            get
            {
                yield return new TestCaseData("G", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100");
                yield return new TestCaseData("NRP", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100");
                yield return new TestCaseData("NPR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter, +1 (425) 555-0100, $1,000,000.00");
                yield return new TestCaseData("NP", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter, +1 (425) 555-0100");
                yield return new TestCaseData("NR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter, $1,000,000.00");
                yield return new TestCaseData("PR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("+1 (425) 555-0100, $1,000,000.00");
                yield return new TestCaseData("N", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Jeffrey Richter");
                yield return new TestCaseData("P", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("+1 (425) 555-0100");
                yield return new TestCaseData("R", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("$1,000,000.00");
            }
        }

        [Test, TestCaseSource(nameof(CustomerFormattingTestData))]
        public static string CustomerFormatting_Test(string format, Customer customer)
        {
            return string.Format("{0:" + format + "}", customer);
        }

        static object[] CustomerFormatProviderTestData =
        {
            new TestCaseData(new CustomerFormatProvider(), "NRP", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100"),
            new TestCaseData(new CustomerFormatProvider(), "R", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: $1,000,000.00"),
            new TestCaseData(new CustomerFormatProvider(), "G", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter, $1,000,000.00, +1 (425) 555-0100"),
            new TestCaseData(new CustomerFormatProvider(), "NPR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter, +1 (425) 555-0100, $1,000,000.00"),
            new TestCaseData(new CustomerFormatProvider(), "NP", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter, +1 (425) 555-0100"),
            new TestCaseData(new CustomerFormatProvider(), "NR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter, $1,000,000.00"),
            new TestCaseData(new CustomerFormatProvider(), "PR", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: +1 (425) 555-0100, $1,000,000.00"),
            new TestCaseData(new CustomerFormatProvider(), "N", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: Jeffrey Richter"),
            new TestCaseData(new CustomerFormatProvider(), "P", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: +1 (425) 555-0100"),
            new TestCaseData(new CustomerFormatProvider(), "R", new Customer() { Name = "Jeffrey Richter", ContactPhone = "+1 (425) 555-0100", Revenue = 1000000 }).Returns("Customer record: $1,000,000.00")
        };

        [Test, TestCaseSource(nameof(CustomerFormatProviderTestData))]
        public static string CustomerFormatProvider_Test(IFormatProvider formatProvider, string format, Customer customer)
        {
            return string.Format(formatProvider, "{0:" + format + "}", customer);
        }

    }
}

