using ProductLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StorageLibrary
{
    public interface IStorage
    {
        int ID { get; set; }
        Product this[int id] { get; set; }
        void Push(Product product);
        void Push(int index, Product product);

        void Del(int id);
        void Replace (int index, Product product);
        void ChangePosition(int index1, int index2);
        int SearchID(int id);
        int SearchName(string name);
        void SortID();
        void SortName();
    }
}
