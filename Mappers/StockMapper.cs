using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMapper
    {
        public static StockDTO ToStockDTO(this Stock stockModel)
        {
            return new StockDTO
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDividend = stockModel.LastDividend,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(c => c.ToCommentDTO()).ToList()
            };
        }

        public static Stock ToStockFromCreatedModel(this StockCreatedModel stockCreatedModel)
        {
            return new Stock
            {
                Symbol = stockCreatedModel.Symbol,
                CompanyName = stockCreatedModel.CompanyName,
                Purchase = stockCreatedModel.Purchase,
                LastDividend = stockCreatedModel.LastDividend,
                Industry = stockCreatedModel.Industry,
                MarketCap = stockCreatedModel.MarketCap
            };
        }
    }
}