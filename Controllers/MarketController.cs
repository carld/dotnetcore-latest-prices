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

        [HttpGet("")]
        public List<Price> Get() => db.Prices.ToList();

        [HttpGet("latest")]
        public List<LatestPrice> Latest()
        {
            return db.LatestPrices.FromSqlRaw(@"
            SELECT p1.id, 
                p1.ticker, 
                p1.published_at,
                p1.price_in_cents
            FROM prices p1
            LEFT JOIN
                prices p2
                    ON p1.ticker = p2.ticker
                    AND p1.published_at < p2.published_at
            WHERE 
                    p2.id is NULL;
            ").ToList();
        }
    }
}