using BarcodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
    public sealed class FinaleProduct: Book, IProduct 
    {
        private RecBar _barcode;

        public FinaleProduct(string id, string name, int year, string author, string genre): base(id, name, year, author, genre)
        {

        }

        string IProduct.Id
        {
            get => _barcode.ToString();
            set { }
        }

        IBarcode IProduct.Barcode
        {
            get => _barcode;
            set { }
        }

        public new string Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        public new RecBar Barcode
        {
            get => _barcode;
            set => _barcode.Text = value.ToString();
        }


    }
}
