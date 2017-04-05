using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Logic
{
    public class BookListService : IEnumerable<Book>
    {
        #region Properties

        private List<Book> booksList;
        private IBookStorage repository;

        #endregion

        #region Ctor

        public BookListService()
        {
            this.repository = null;
            booksList = new List<Book>();
        }

        public BookListService(IBookStorage repository)
        {
            this.repository = repository;
            booksList = (List<Book>)repository.LoadData();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add book to the storage
        /// </summary>
        /// <param name="book">Book to add</param>
        public void AddBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));
            if (booksList.Contains(book))
                throw new Exception($"This book({book.Name}) is already in storage");
            booksList.Add(book);
            if(repository != null)
                repository.AddItem(book);
        }

        /// <summary>
        /// Removes book from storage
        /// </summary>
        /// <param name="book">Book to remove</param>
        public void RemoveBook(Book book)
        {
            if (book == null)
                throw new ArgumentNullException($"{nameof(book)} must be not null");

            if (!booksList.Contains(book))
                throw new ArgumentException("There isn't such book in a collection");
            booksList.Remove(book);
            if (repository != null)
                repository.SaveData(booksList);
        }

        /// <summary>
        /// Finds book by the given criterion
        /// </summary>
        /// <param name="predicate">criterion of finding</param>
        /// <returns></returns>
        public Book FindBookByTag(Predicate<Book> predicate)
        {
            return booksList.Find(predicate);
        }

        public List<Book> FindByAuthor(string author)
        {
            return booksList.FindAll((Book book) => book.Author == author);
        }

        public List<Book> FindByYear(int year)
        {
            return booksList.FindAll((Book book) => book.Year == year);
        }

        /// <summary>
        /// Sort books by the given criterion
        /// </summary>
        /// <param name="comparision">criterion of sort(delegate)</param>
        /// <returns></returns>
        public void SortBooksByTag(Comparison<Book> comparision)
        {
            booksList.Sort(comparision);
        }

        /// <summary>
        /// Sort books by the given criterion
        /// </summary>
        /// <param name="comparision">criterion of sort(interface)</param>
        /// <returns></returns>
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            booksList.Sort(comparer);
        }

        #endregion

        #region IEnumerator<T> members

        public IEnumerator<Book> GetEnumerator()
        {
            return ((IEnumerable<Book>)booksList).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
