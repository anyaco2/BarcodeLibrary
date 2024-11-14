using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
    public class Book : Product
    {
        public Book(string id, string name, int year, string author, string genre) : base(id, name)
        {
            Author = author;
            Year = year;
            Genre = genre;
        }

        public string Author { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        protected override void GetProductType(StringBuilder sb)
        {
            sb.Append("Книга");
        }
        protected override void GetInfo(StringBuilder sb)
        {
            sb.AppendLine("Год выпуска: " + Year);
            sb.AppendLine("Автор: " + Author);
            sb.AppendLine("Жанр: " + Genre);
        }
    }
}
