using System;
using System.Collections.Generic;
using System.IO;


namespace Task2Logic
{
    public class BinaryFileRepository : IBookStorage
    {
        private readonly string sourceFilePath;

        public BinaryFileRepository(string path)
        {
            if (isFileNameValid(path))
                this.sourceFilePath = path;
        }

        #region Methods of interface

        public IEnumerable<Book> LoadData()
        {
            return ReadListFromBinaryFile();
        }

        public void AddItem(Book item)
        {
            if (item == null)
                throw new ArgumentNullException();
            AppendBookToBinaryFile(item);
        }

        public void SaveData(IEnumerable<Book> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            WriteListToBinaryFile(collection);
        }

        #endregion

        #region Private methods

        private IEnumerable<Book> ReadListFromBinaryFile()
        {
            var list = new List<Book>();
            Stream fileStream = File.Open(sourceFilePath, FileMode.OpenOrCreate, FileAccess.Read);
            using (BinaryReader input = new BinaryReader(fileStream))
            {
                while (input.BaseStream.Position < input.BaseStream.Length)
                {
                    var book = new Book();
                    book.Name = input.ReadString();
                    book.Author = input.ReadString();
                    book.Year = input.ReadInt32();
                    book.Pages = input.ReadInt32();
                    list.Add(book);
                }
            }
            return list;
        }

        private void AppendBookToBinaryFile(Book book)
        {
            Stream fileStream = File.Open(sourceFilePath, FileMode.Append, FileAccess.Write);
            using (BinaryWriter output = new BinaryWriter(fileStream))
            {
                output.Write(book.Name);
                output.Write(book.Author);
                output.Write(book.Year);
                output.Write(book.Pages);
            }
        }

        private void WriteListToBinaryFile(IEnumerable<Book> collection)
        {
            Stream fileStream = File.Open(sourceFilePath, FileMode.Truncate, FileAccess.Write);
            using (BinaryWriter output = new BinaryWriter(fileStream))
            {
                foreach (var book in collection)
                {
                    output.Write(book.Name);
                    output.Write(book.Author);
                    output.Write(book.Year);
                    output.Write(book.Pages);
                }
            }
        }

        /// <summary>
        /// Validation test
        /// </summary>
        /// <param name="fileName">path to file</param>
        /// <returns></returns>
        private bool isFileNameValid(string fileName)
        {
            if ((fileName == null) || (fileName.IndexOfAny(Path.GetInvalidPathChars()) != -1))
                return false;
            try
            {
                var tempFileInfo = new FileInfo(fileName);
                return true;
            }
            catch (NotSupportedException)
            {
                return false;
            }
        }

        #endregion
    }
}
