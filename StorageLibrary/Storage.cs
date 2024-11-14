using ProductLibrary;
using BarcodeLibrary;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Data.SqlTypes;
namespace StorageLibrary
{
    public class Storage:IStorage
    {
        private Product[] books;
        private Storage storages;
        private int _id;

        private Storage(int size)
        {
            books = new Product[size];
            ID = new Random().Next(1, 9999);
        }

        public static implicit operator Storage(int size)
        {
            return new Storage(size);
        }

        public int ID
        {
            get { return _id; }
            set
            {
                if (_id == 0) { _id = value; }
                else
                {
                    _id = value; 
                    UpdateBarcode();
                }
            }
        }



        public Product this[int index]
        {
            get
            {
                if (index >= 0 && index < books.Length)
                {
                    Product book = books[index];
                    books[index] = null;
                    return book;
                }
                return null;
            }
            set
            {
                if (index >= 0 && index < books.Length)
                {
                    books[index] = value;
                    UpdateBarcode();
                }
            }
        }

        public void Push(Product book)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] == null)
                {
                    this[i] = book;
                    return;
                }
            }
        }

        public void Push(int index, Product book)
        {
            if (index >= 0 && index < books.Length)
            {
                this[index] = book;
            }
        }

        public void Del(int index)
        {
            this[index] = null;
        }

        public void Replace(int index, Product book)
        {
            if (index >= 0 && index < books.Length)
            {
                this[index] = book;
            }
        }

        public void ChangePosition(int index1, int index2)
        {
            if (index1 >= 0 && index1 < books.Length && index2 >= 0 && index2 < books.Length)
            {
                Product tmp = books[index1];
                books[index1] = books[index2];
                books[index2] = tmp;
            }
        }

        public int SearchID(int ID)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != null && books[i].Id.Equals(ID))
                {
                    return i;
                }
            }
            return -1;
        }

        public int SearchName(string name)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i] != null && books[i].Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }
            return -1;
        }

        public void SortID()
        {
            Array.Sort(books, (x, y) =>
            {
                int xIde = x?.Id != null ? int.Parse(x.Id) : 0;
                int yIde = y?.Id != null ? int.Parse(y.Id) : 0;

                return xIde.CompareTo(yIde);
            });
            UpdateBarcode();
        }

        public void SortName()
        {
            Array.Sort(books, (x, y) => string.Compare(x?.Name, y?.Name, StringComparison.OrdinalIgnoreCase));
        }

        public override string ToString()
        {
            string result = $"Витрина ID: {ID}\nТовары: \n";
            for (int i = 0; i < books.Length; i++)
            {
                result += $"Ячейка {i}: Книга: {(books[i] != null ? books[i].Name : "Пусто")}, ID: {(books[i] != null ? books[i].Idi : "000000")}\n{(books[i] != null ? books[i].Barcode : "")}\n";
            }
            return result;
        }

        private void UpdateBarcode(Product book, Storage storagese, int index = -1)
        {
            if (book != null)
            {
                // Сбросить штрихкод до ID товара, если это необходимо
                book.Barcode = null;

                if (index != -1) // если индекс задан
                {
                    book.Idi = $"{book.Id} {ID} {index}";
                    book.Barcode = new Barcode(book.Idi);
                }
            }
        }

        private void UpdateBarcode()
        {
            for (int i = 0; i < books.Length; i++)
            {
                UpdateBarcode(books[i], ID, i);
            }
        }

        //public void ChangeID()
        //{
        //    ID++;
        //    UpdateBarcode();
        //}






    }
}
