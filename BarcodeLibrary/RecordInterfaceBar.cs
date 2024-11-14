using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BarcodeLibrary
{
    public record RecBar: IBarcode
    {
        private string text;
        public string Code { get; private set; }
        public string Text
        {
            get => text; set
            {
                if (text == value) return;
                text = value;
                Code = BarcodeHelper.GetCode(value);
            }
        }

        string IBarcode.Text
        {
            get => text;
            set { }
        }

        public RecBar(string _text)
        {
            Text = _text;
        }

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
