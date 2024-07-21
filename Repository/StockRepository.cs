using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stocks;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public StockRepository(ApplicationDBContext applicationDBContext){

            _applicationDBContext = applicationDBContext;
        }

        

        

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _applicationDBContext.Stocks.Include(s => s.Comments).FirstOrDefaultAsync(x => x.Id == id);

            if (stock == null){
                return null;
            }
            _applicationDBContext.Comments.RemoveRange(stock.Comments);
            _applicationDBContext.Stocks.Remove(stock);
            await _applicationDBContext.SaveChangesAsync();
            return stock;  
        }



        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {

           var stock =   _applicationDBContext.Stocks
                    .Include(c=> c.Comments)
                    .ThenInclude(a => a.appUser)
                    .AsQueryable();

           if(!string.IsNullOrWhiteSpace(queryObject.CompanyName)){
            stock = stock.Where(s => s.CompanyName.Contains(queryObject.CompanyName));
            }

            if(!string.IsNullOrWhiteSpace(queryObject.Symbol)){
                stock = stock.Where(s => s.Symbol.Contains(queryObject.Symbol));   

            }

            if(!string.IsNullOrWhiteSpace(queryObject.SortBy)){

                if(queryObject.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase)){
                    stock = queryObject.Desending ?  stock.OrderByDescending(s => s.Symbol) : stock.OrderBy(s => s.Symbol);
                }
            }

            var skipNumber = (queryObject.PageNumber -1) *  queryObject.PageSize;

          return await stock.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();


        }

        

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _applicationDBContext.Stocks.Include(c=> c.Comments).FirstOrDefaultAsync(i => i.Id==id);
            
        }

        public async Task<Stock?> GetBySymbolAsync(string symbol)
        {
            return await _applicationDBContext.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
        }

        public Task<bool> StockExists(int id)
        {
            return _applicationDBContext.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateStockRequestDto)
        {
            var existingStock = await _applicationDBContext.Stocks.FirstOrDefaultAsync(x => x.Id==id);

            if(existingStock == null){
                return null;
            }
            existingStock.Symbol =updateStockRequestDto.Symbol;
            existingStock.CompanyName = updateStockRequestDto.CompanyName;
            existingStock.Purchase = updateStockRequestDto.Purchase;
            existingStock.LastDiv = updateStockRequestDto.LastDiv;
            existingStock.Industry = updateStockRequestDto.Industry;
            existingStock.MarketCap = updateStockRequestDto.MarketCap;

            await _applicationDBContext.SaveChangesAsync();
            return existingStock;


        }

        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _applicationDBContext.Stocks.AddAsync(stock);
            await _applicationDBContext.SaveChangesAsync();
            return stock;

        }


        public async Task<string>  Upload(IFormFile file)
        {
            //extension
            List<string> validExtentions = new List<string>(){".jpg", ".png", ".gif"};
            var extention =Path.GetExtension(file.FileName);
            if(!validExtentions.Contains(extention))
            {
                return $"Extention is not valid({string.Join(',', validExtentions)})";
            }

            //filesize
            long size = file.Length;
            if(size > (5*1024*1024))
            {return "Max sixe can 5mb";}


            //name changing
            string FileName = Guid.NewGuid().ToString()+extention;
            string path = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
            using FileStream stream = new FileStream(Path.Combine(path, FileName) +  FileName, FileMode.Create);
            await file.CopyToAsync(stream);
            
            return FileName;



        }







        
    }
}