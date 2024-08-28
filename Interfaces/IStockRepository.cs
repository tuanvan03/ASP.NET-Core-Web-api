using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocks();
        Task<Stock?> GetStockById(int id);
        Task<Stock> CreateStock(Stock stock);
        Task<Stock?> UpdateStock(int id, StockUpdateModel stock);
        Task<Stock?> DeleteStock(int id);
        Task<bool> StockExists(int id);
    }
}