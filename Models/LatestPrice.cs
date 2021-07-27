using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace latest_prices.Models
{
    [Keyless]
    public class LatestPrice
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("published_at")]
        [DataType(DataType.DateTime)]
        public DateTime Published { get; set; }
        [Column("ticker")]
        public string Ticker { get; set; }
        [Column("price_in_cents")]
        public int Cents { get; set; }

    }
}