using BarcodeLibrary;
using System.Security.Cryptography;
using System.Text;

namespace ProductLibrary
{
    public abstract class Product: IProduct
    {
        
        private Barcode _barcode;
        private string _idi;
        private string _id;//самого устройства private string _idi;//товара (полка, место)
        public string Idi//создание id для товара
        {
            get { return _idi; }
            set
            {
                _idi = value; Barcode.Text = value.ToString();
            }
        }
         public string Id//создание id для устройства
        {
            get { return _id; }
            set
            {
                _id = value; Barcode.Text = value.ToString();
            }
        }
        public IBarcode Barcode { get; set; }

        protected Product(string id, string name)
        {
            Barcode = new Barcode(id.ToString());
            Id = id;
            Name = name;
        }

        public string Name { get; set; }

        

        public override string ToString() { 
            var sb = new StringBuilder();
            GetProductType(sb);
            sb.AppendLine($": {Name}");
            GetInfo(sb);
            sb.AppendLine($"Штрихкод: \n{Barcode.ToString()}");
            return sb.ToString();
        }

        protected abstract void GetInfo(StringBuilder sb);
        protected abstract void GetProductType(StringBuilder sb);  
        


    }
}
