using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using latest_prices.Models;

namespace latest_prices.Queries
{
    /* 
     * The LatestPriceQuery class contains database SQL detail
     * for queries that are not implemented as views for example.
     * This level of detail typically does not belong in other
     * layers of the application, such as controllers or models.
     */
    public class LatestPriceQuery
    {

        private readonly MarketContext db;

        public LatestPriceQuery(MarketContext context)
        {
            this.db = context;
        }

        /* 
         * Groupwise maximum using an uncorelated subquery.
         *
         * This query returns the most recent price for each ticker,
         * before a specified date and time.
         * This enables delayed pricing, for example where there
         * is a requirement to provide a feed of prices delayed by 10 minutes.
         */
        private const String raw_sql = @"
            SELECT p1.id, 
            p1.ticker, 
            p1.published_at,
            p1.price_in_cents
            FROM prices p1
            JOIN
            ( SELECT id, ticker, MAX(published_at) 
                FROM prices 
                WHERE published_at BETWEEN {0} AND {1}
                GROUP BY ticker)
            AS p2
            ON p1.id = p2.id
            ";

        /* Returns the latest price for every ticker before the given date and time.
         * The return object is IQueryable so further filters may be added.
         */
        public IQueryable<Price> Between(DateTime start, DateTime end)
        {
            return this.db.Prices.FromSqlRaw(LatestPriceQuery.raw_sql, start, end);
        }
    }

}