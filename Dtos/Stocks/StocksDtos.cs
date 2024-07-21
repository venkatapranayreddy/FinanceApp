using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;


namespace api.Dtos.Stocks
{
    public class StocksDtos
    {
         public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;

        public decimal Purchase { get; set; }
        public string? FilePath {get; set; } = string.Empty;

        public List<CommentDto> Comments { get; set; }  = new List<CommentDto>();
    }
}