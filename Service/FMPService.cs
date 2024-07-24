using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Stocks;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Newtonsoft.Json;

namespace api.Service
{
    public class FMPService : IFMPService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _iConfiguration;

        public FMPService(HttpClient httpClient, IConfiguration iConfiguration)
        {
            _httpClient = httpClient;
            _iConfiguration = iConfiguration;
        }

        public async Task<Stock> FindStockBySymbolAsync(string symbol)
        {
            try{
            var result = await _httpClient.GetAsync($"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_iConfiguration["FMPKey"]}");
            if(result.IsSuccessStatusCode)
            {
                var Content = await result.Content.ReadAsStringAsync();
                var tasks = JsonConvert.DeserializeObject<FMPStock[]>(Content);
                var stock = tasks[0];
                if(stock != null)
                {
                    return stock.ToStockFromFMPService();
                }
                
                return null;
            }
             return null;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
                
            }
        }
    }
}