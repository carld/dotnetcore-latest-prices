using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

using latest_prices.Models;
using latest_prices.Queries;
using latest_prices.DTOs;

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

        /* This action is requested from a URL like:
         *
         *   https://localhost:5001/api/market/latest?before=2021-07-04
         *
         */
        [Route("latest")]
        [HttpGet]
        public List<PriceDTO> LatestPrices(DateTime? before = null)
        {
            DateTime datetime = before.HasValue ? 
                before.Value
                : 
                DateTime.Now;

            //Console.WriteLine("Parameter ++ '{0}'", datetime);

            IQueryable<PriceDTO> query = new LatestPriceQuery(this.db)
                .Before(datetime)
                .Select(p => new PriceDTO 
                    { 
                        Published = p.Published, 
                        Ticker = p.Ticker, 
                        Price = p.Cents 
                    });

            return query.ToList();
        }
    }
}