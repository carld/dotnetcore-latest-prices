using Microsoft.EntityFrameworkCore;
using System;

namespace latest_prices.Models
{
    public class MarketContext : DbContext
    {

        public string DbPath { get; private set; }

        public MarketContext(DbContextOptions<MarketContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}market.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");

        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<Price> Prices { get; set; }
    }
}