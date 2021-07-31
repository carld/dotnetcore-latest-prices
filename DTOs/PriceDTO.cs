using System;

namespace latest_prices.DTOs
{
    public class PriceDTO
    {
        public DateTime Published { get; set; }
        public string Ticker { get; set; }
        public int Price { get; set; }
    }
}
