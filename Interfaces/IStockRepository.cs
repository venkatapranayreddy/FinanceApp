using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Models;
using api.Helpers;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);

        Task<Stock?> GetByIdAsync(int id);  //GET NULL IF THERE IS NO NULL

        Task<Stock?> GetBySymbolAsync(string symbol);

        Task<Stock> CreateAsync(Stock stock);

        Task<Stock?> UpdateAsync(int id,  UpdateStockRequestDto updateStockRequestDto);

        Task<Stock?> DeleteAsync(int id);

        Task<bool> StockExists(int id);
        Task<string> Upload(IFormFile file);




    }
}