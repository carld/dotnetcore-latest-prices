using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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

                // Improve performance of bulk seed by disabling change tracking
                context.ChangeTracker.AutoDetectChangesEnabled = false;

                List<String> tickers = DotNetUtilities.Combinations.Combo2(new List<string> { "A", "B", "C" }, "" );
                var randomTest = new Random();
                DateTime startDate = new DateTime(2021, 8, 21, 10, 0, 0);
                for(int m = 0; m < 500; m++ )
                {
                    startDate = startDate - new TimeSpan(1, 0, 0, 0);
                    for(int n = 0; n < 1000; n ++)
                    {
                        TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, 60 * 6), 0);
                        DateTime newDate = startDate + newSpan;

                        context.Prices.Add(
                            new Price
                            {
                                Ticker = tickers[(m + n) % tickers.Count],
                                Published = newDate,
                                Cents = randomTest.Next(70, 800)
                            }
                        );
                    }
                    
                }
                context.SaveChanges();

                // Restore tracking of changes
                context.ChangeTracker.AutoDetectChangesEnabled = true;
            }
        }
    }
}