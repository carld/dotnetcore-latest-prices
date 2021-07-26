using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using MvcMovie.Data;
using System;
using System.Linq;
using System.Collections.Generic;

using System.IO;

namespace latest_prices.Models
{
    public static class SeedData
    {



        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MarketContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MarketContext>>()))
            {
                // Look for any prices
                if (context.Prices.Any())
                {
                    return;   // DB has been seeded
                }

                List<String> tickers = DotNetUtilities.Combinations.Combo2(new List<string> { "A", "B", "C" }, "" );
                List<DateTime> timestamps = new List<DateTime>();
                StreamReader sr = new StreamReader("timestamps.txt");
                String line;
                while ( (line = sr.ReadLine()) != null) {
                    timestamps.Add( DateTime.Parse(line));
                }
                int n = 0;
                var randomTest = new Random();
                foreach(DateTime dt  in timestamps)
                {
                    context.Prices.Add(
                        new Price
                        {
                            Ticker = tickers[n % tickers.Count],
                            PublishedAt = dt,
                            Cents = randomTest.Next(70, 800)
                        }
                    );
                    n++;
                }
                context.SaveChanges();
            }
        }
    }
}