using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IPortfolioRepository
    {
        public Task<List<Stock>> GetUserPortfolioAsync(AppUser appUser);

        public Task<Portfolio> CreatePortfolioAsync(Portfolio portfolio);

        public Task<Portfolio> DeletePortfolioAsync(AppUser appUser, string symbol);
    }
}