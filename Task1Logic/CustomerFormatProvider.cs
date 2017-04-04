using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task1Logic
{
    public class CustomerFormatProvider : ICustomFormatter, IFormatProvider
    {
        private readonly IFormatProvider parent;

        public CustomerFormatProvider() : this(new CultureInfo("en-US")) { }

        public CustomerFormatProvider(IFormatProvider parent)
        {
            this.parent = parent;
        }

        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            return Thread.CurrentThread.CurrentCulture.GetFormat(formatType);
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg == null || !(arg is Customer) || string.IsNullOrEmpty(format))
                return string.Format(this.parent, "{0:" + format + "}", arg);

            Customer customer = arg as Customer;

            switch (format.ToUpperInvariant())
            {
                case "N":
                    return string.Format(formatProvider, "Customer record: {0}", customer.Name);
                case "R":
                    return string.Format(formatProvider, "Customer record: {0:C}", customer.Revenue);
                case "P":
                    return string.Format(formatProvider, "Customer record: {0}", customer.ContactPhone);
                case "NR":
                    return string.Format(formatProvider, "Customer record: {0}, {1:C}", customer.Name, customer.Revenue);
                case "NP":
                    return string.Format(formatProvider, "Customer record: {0}, {1}", customer.Name, customer.ContactPhone);
                case "RP":
                    return string.Format(formatProvider, "Customer record: {0:C}, {1}", customer.Revenue, customer.ContactPhone);
                case "PR":
                    return string.Format(formatProvider, "Customer record: {0}, {1:C}", customer.ContactPhone, customer.Revenue);
                case "G":
                case "NRP":
                    return string.Format(formatProvider, "Customer record: {0}, {1:C}, {2}", customer.Name, customer.Revenue, customer.ContactPhone);
                case "NPR":
                    return string.Format(formatProvider, "Customer record: {0}, {1}, {2:C}", customer.Name, customer.ContactPhone, customer.Revenue);
                default:
                    throw new FormatException($"The {format} format string is not supported.");
            }
        }
    }
}
