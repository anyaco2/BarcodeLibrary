using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeLibrary
{

    /// <summary>
    /// Класс, представляющий штрих-код.
    /// </summary>
    public class Barcode: IBarcode
    {
        
        private string text;

        public string Text
        {
            get => text; set
            {
                if (text == value) return;
                text = value;
                Code = BarcodeHelper.GetCode(value);
            }
        }
        public Barcode(string text)
        {
            Text = text;
        }

        public string Code { get; private set; }

        public static BarcodeType OutputType { get; set; } = BarcodeType.Full;
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (OutputType)
            {
                case BarcodeType.Text:
                    sb.AppendLine(Text);
                    break;
                case BarcodeType.Barcode:
                    sb.AppendLine(Code);
                    break;
                case BarcodeType.Full:
                    sb.AppendLine(Code);
                    sb.AppendLine(new string(Text));
                    break;
            }
            return sb.ToString();
        }
    }
}
