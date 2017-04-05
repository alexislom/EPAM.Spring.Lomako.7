using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task2Logic;

namespace Task2Console
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> listOfSomeBooks = new List<Book>()
            {
                new Book(){Name = "Oliver Twist", Author = "Mark Twen", Year = 1935, Pages = 350},
                new Book(){Name = "Computer Networks", Author = "Andew Tanenbaum",Year = 2010, Pages = 750},
                new Book(){Name = "Design Patterns", Author = "Gang Of Fours", Year = 1995, Pages = 520}
            };

            BinaryFileRepository repository = new BinaryFileRepository("list.bin");

            BookListService service = new BookListService(repository);

            foreach (var i in listOfSomeBooks)
            {
                service.AddBook(i);
            }

            foreach (var i in service)
            {
                Console.WriteLine(i + Environment.NewLine);
            }

            //local repository
            BookListService books = new BookListService();

            books.AddBook(new Book() { Name = "Crime and Punishment", Author = "Fedor Dostoevsky", Year = 1995, Pages = 777 });
            books.AddBook(new Book() { Name = "To Kill a Mockingbird", Author = "Harper Li", Year = 1995, Pages = 377 });
            books.AddBook(new Book() { Name = "Solaris", Author = "Stanislav Lem", Year = 1995, Pages = 277 });

            foreach (var i in books)
                Console.WriteLine(i + Environment.NewLine);

            Console.WriteLine("Search methods:" + Environment.NewLine);
            Console.WriteLine("Dostoevsky's book" + Environment.NewLine + books.FindBookByTag(b => b.Author == "Fedor Dostoevsky"));

            List<Book> result = books.FindByYear(1995);

            Console.WriteLine(Environment.NewLine + "Searching by year:" + Environment.NewLine);
            foreach (var i in result)
            {
                Console.WriteLine(i + Environment.NewLine);
            }

            var bls = new BookListService(new BinaryFileRepository("anotherBinFile.bin"));

            bls.AddBook(new Book() { Name = "To Kill a Mockingbird", Author = "Harper Li", Year = 1995, Pages = 377 });
            bls.AddBook(new Book() { Name = "Бесы", Author = "Fedor Dostoevsky", Year = 1995, Pages = 977 });
            bls.AddBook(new Book("1984", "George Orwell", 1949, 267));
            bls.AddBook(new Book("Harry Potter and the Goblet of Fire", "J. K. Rowling", 2000, 636));
            bls.AddBook(new Book("We Children from Bahnhof Zoo", "Christiane F.",  1980, 300));

            Console.WriteLine(Environment.NewLine + "New book storage after sorting by year:" + Environment.NewLine);

            bls.SortBooksByTag((x, y) => x.Year - y.Year);

            foreach (var i in bls)
            {
                Console.WriteLine(i + Environment.NewLine);
            }
        }
    }
}
