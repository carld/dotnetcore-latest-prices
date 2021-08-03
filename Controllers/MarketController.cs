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
         *   https://localhost:5001/api/market/latest?start=2021-06-04&end=2021-07-20
         *
         * The latest price, i.e. that closest to the end date, within the provided time range 
         * will be returned.
         */
        [Route("latest")]
        [HttpGet]
        public List<PriceDTO> LatestPrices(DateTime? start = null, DateTime? end = null)
        {
            DateTime queryEnd = end.GetValueOrDefault(DateTime.Now);
            DateTime queryStart = start.GetValueOrDefault(
                new DateTime(queryEnd.Year, queryEnd.Month, queryEnd.Day, 0, 0, 0)
                );

            //Console.WriteLine("Parameter ++ start '{0}'", queryStart);
            //Console.WriteLine("Parameter ++ end   '{0}'", queryEnd);

            IQueryable<PriceDTO> query = new LatestPriceQuery(this.db)
                .Between(queryStart, queryEnd)
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