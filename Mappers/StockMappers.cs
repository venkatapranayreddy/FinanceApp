using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Stocks;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StocksDtos ToStockDto(this Stock stockModel)
        {
            
            return new StocksDtos {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                FilePath = stockModel.FilePath,
                Comments = stockModel.Comments.Select( c => c.ToCommentDto()).ToList()


            };
        }


        public static Stock ToStockFromCreateDto(this CreateStockRequestDto createStockRequestDto)
        {


            return new Stock {
                
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,
                Purchase = createStockRequestDto.Purchase,
                LastDiv = createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                // FilePath = createStockRequestDto.FilePath,
                MarketCap = createStockRequestDto.MarketCap
                

            };

        }

        public static Stock ToStockFromFMPService(this FMPStock fMPStock)
        {


            return new Stock {
                
                Symbol = fMPStock.symbol,
                CompanyName = fMPStock.companyName,
                Purchase = (decimal)fMPStock.price,
                LastDiv = (decimal)fMPStock.lastDiv,
                Industry = fMPStock.industry,
                // FilePath = createStockRequestDto.FilePath,
                MarketCap = fMPStock.mktCap,
                

            };

        }

    }
}