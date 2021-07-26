using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}