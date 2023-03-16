using System;

namespace Kiwoom.Net.Objects.Models
{
    public class Quote
    {
        public DateTime Time { get; set; }
        public int Open { get; set; }
        public int High { get; set; }
        public int Low { get; set; }
        public int Close { get; set; }
        public int Volume { get; set; }
    }
}