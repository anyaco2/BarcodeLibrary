using BarcodeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
    public interface IProduct
    {
        string Id { get; set; }
        string Idi { get;set; }
        IBarcode Barcode { get; set; }
    }
}
