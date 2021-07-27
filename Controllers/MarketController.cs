using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using latest_prices.Models;

namespace latest_prices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {
        private readonly MarketContext db;
        /*
        
        The controller constructor needs to be public for dependency injection to work.
        
        */
        public MarketController(MarketContext dbcontext)
        {
            this.db = dbcontext;
        }

        [HttpGet]
        public List<Price> Get() => db.Prices.ToList();

        [Route("latest")]
        [HttpGet]
        public List<LatestPrice> LatestPrices(DateTime? before = null)
        {
            var parameter1 = before.HasValue ? 
                before.Value.ToString("s") 
                : DateTime.Now.ToString("s");

            Console.WriteLine("Parameter ++ '{0}'", parameter1);

            return db.LatestPrices.FromSqlRaw(
                @"
                    SELECT p1.id, 
                    p1.ticker, 
                    p1.published_at,
                    p1.price_in_cents
                    FROM prices p1
                    JOIN
                    ( SELECT id, ticker, MAX(published_at) 
                        FROM prices 
                        WHERE published_at < {0}
                        GROUP BY ticker)
                    AS p2
                    ON p1.id = p2.id
                    "
                , 
                parameter1 
            ).ToList();
        }
    }
}