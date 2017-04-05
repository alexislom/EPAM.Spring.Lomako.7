using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Logic
{
    public class Book : IEquatable<Book>, IComparable<Book>
    {
        #region Fields & Properties

        public string Author { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }

        #endregion

        #region Ctors

        public Book() { }

        public Book(string name, string author, int year, int pages)
        {
            this.Name = name;
            this.Author = author;
            this.Year = year;
            this.Pages = pages;
        }

        #endregion

        #region Interfaces methods

        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return ToString().Equals(other.ToString());
        }

        public int CompareTo(Book book)
        {
            if (Equals(book))
                return 0;
            if(ReferenceEquals(book, null))
                return 1;
            return string.Compare(Name, book.Name, true);
        }

        #endregion

        #region Object's Methods

        public override string ToString()
        {
            return $"Name: {Name} {Environment.NewLine }" + 
                   $"Author: {Author} { Environment.NewLine }" + 
                   $"Publishing Year: {Year} { Environment.NewLine }" +
                   $"Number Of Pages: {Pages}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Book)) return false;
            return Equals((Book)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 +this.Name.GetHashCode();
                hash = hash * 31 + this.Author.GetHashCode();
                hash = hash * 31 + this.Pages.GetHashCode();
                hash = hash * 31 + this.Year.GetHashCode();
                return hash;
            }
        }

        #endregion
    }
}
