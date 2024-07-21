using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {

        private readonly ApplicationDBContext _applicationDBContext;
        

        public PortfolioRepository(ApplicationDBContext applicationDBContext)
        {
            _applicationDBContext = applicationDBContext;
        }

        public async Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio)
        {
            await _applicationDBContext.Portfolios.AddAsync(portfolio);
             await _applicationDBContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolioAsync(AppUser appUser, string symbol)
        {
            var portfolioModel = await _applicationDBContext.Portfolios.FirstOrDefaultAsync(x =>x.AppUserId == appUser.Id && x.Stock.Symbol.ToLower() == symbol.ToLower());

            if(portfolioModel == null)
            {
                return null;
            }

            _applicationDBContext.Portfolios.Remove(portfolioModel);
            await _applicationDBContext.SaveChangesAsync();
            return portfolioModel;
        }

        public async Task<List<Stock>> GetUserPortfolioAsync(AppUser appUser)
        {
            return await _applicationDBContext.Portfolios.Where(u => u.AppUserId == appUser.Id)
            .Select(stock => new Stock 
            {
                Id  = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                Purchase = stock.Stock.Purchase,
                LastDiv =stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,
                MarketCap = stock.Stock.MarketCap

            }).ToListAsync();
        }
    }
}