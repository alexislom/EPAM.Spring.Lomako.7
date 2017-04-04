using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1Logic
{
    /// <summary>
    /// Class that describes customers
    /// </summary>
    public class Customer : IFormattable
    {
        #region Prop

        public string Name { get; set; }
        public string ContactPhone { get; set; }
        public decimal Revenue { get; set; }

        #endregion

        #region Ctors

        public Customer() { }

        public Customer(string name, string phone, decimal revenue)
        {
            this.Name = name;
            this.ContactPhone = phone;
            this.Revenue = revenue;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return ToString(null, formatProvider);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format))
                format = "G";
            if (formatProvider == null)
                formatProvider = new CultureInfo("en-US");
            if (!(formatProvider is CultureInfo))
                return string.Format(formatProvider, "{0:" + format + "}", this);

            switch (format.ToUpperInvariant())
            {
                case "G":
                case "NRP":
                    return string.Format(formatProvider, "{0}, {1:C}, {2}", Name, Revenue, ContactPhone);
                case "NPR":
                    return string.Format(formatProvider, "{0}, {1}, {2:C}", Name, ContactPhone, Revenue);
                case "NR":
                    return string.Format(formatProvider, "{0}, {1:C}", Name, Revenue);
                case "NP":
                    return string.Format(formatProvider, "{0}, {1}", Name, ContactPhone);
                case "RP":
                    return string.Format(formatProvider, "{0:C}, {1}", Revenue, ContactPhone);
                case "PR":
                    return string.Format(formatProvider, "{0}, {1:C}", ContactPhone, Revenue);
                case "N":
                    return string.Format(formatProvider, "{0}", Name);
                case "R":
                    return string.Format(formatProvider, "{0:C}", Revenue);
                case "P":
                    return string.Format(formatProvider, "{0:C}", ContactPhone);
                default:
                    throw new FormatException($"The {format} format string isn't supported.");
            }

            #endregion
        }
    }
}
