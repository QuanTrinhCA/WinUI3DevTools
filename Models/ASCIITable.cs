using System.Collections.Generic;

namespace WinUI3DevTools.Models
{
    public class ASCIIItem
    {
        public string Binary { get; set; }
        public string Character { get; set; }
        public int Decimal { get; set; }
        public string Description { get; set; }
        public string Hex { get; set; }
        public string HTML { get; set; }
        public string HTMLName { get; set; }
    }

    internal class ASCII
    {
        internal static List<ASCIIItem> GetControlASCIITable()
        {
            return new List<ASCIIItem>()
            {
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" },
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" },
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" },
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" },
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" },
                new ASCIIItem(){ Character = "NUL", Description = "Null char", Decimal = 0, Hex = "00", Binary = "00000000", HTML = "&#000;", HTMLName = "" }
            };
        }
    }
}