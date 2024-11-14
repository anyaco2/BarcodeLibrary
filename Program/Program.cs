using BarcodeLibrary;
using ProductLibrary;
using StorageLibrary;
using System.Text;
class Program
{
    static void Main(string[] args)
    {

        BarcodeRecord barcode = new BarcodeRecord { Text = "Hello world!" };

        // Вывод информации о штрих-коде
        Console.WriteLine("Текст штрих-кода: " + barcode.Text); // Вывод: Hello world!
        Console.WriteLine("Штрих-код: " + barcode.Code);
        //var book1 = new Book("790920", "Гарри Поттер", 2023, "Джоан Роулинг", "Horror");
        //Console.WriteLine(book1.ToString());

        //Storage shell = 5;

        //shell[1] = book1;

        //Console.WriteLine(shell);
        //var books = new List<Product>
        //{
        //    new Book("234757", "Nefsdf", 2024, "hsdsdf", "dfsdf"),
        //    new Book("89122", "ffwsffs", 2018, "hsdsdf", "dfsdf"),
        //    new Book("562037", "pghkkr", 1999, "hsdsdf", "dfsdf"),
        //};


        //foreach (var book in books)
        //{
        //    shell.Push(book);
        //}
        //Console.WriteLine(shell.ToString());
        //shell.ChangePosition(1, 3);
        //Console.WriteLine(shell);

        //shell.SortName();

        //Console.WriteLine(shell.ToString());

        //shell.SortID();
        //Console.WriteLine(shell);

        //shell.ID++;

        //Console.WriteLine(shell);

    }
}