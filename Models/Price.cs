using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace latest_prices.Models
{
    [Index(nameof(Ticker)), Index(nameof(Published))]
    public class Price
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column("id")]
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