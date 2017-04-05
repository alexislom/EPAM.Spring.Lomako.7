using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2Logic
{
    public interface IBookStorage
    {
        IEnumerable<Book> LoadData();
        void SaveData(IEnumerable<Book> collection);
        void AddItem(Book book);
    }
}
