using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateStock(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> DeleteStock(int id)
        {
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);
            if (existingStock == null)
            {
                return null;
            }
            _context.Stocks.Remove(existingStock);
            await _context.SaveChangesAsync();
            return existingStock;
        }

        public Task<List<Stock>> GetAllStocks()
        {
            return _context.Stocks.Include(c => c.Comments).ToListAsync();
        }

        public async Task<Stock?> GetStockById(int id)
        {
            var stock = await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(s => s.Id == id);
            if (stock == null)
            {
                return null;
            }
            return stock;
        }

        public Task<bool> StockExists(int id)
        {
            return _context.Stocks.AnyAsync(s => s.Id == id);
        }

        public async Task<Stock?> UpdateStock(int id, StockUpdateModel stockUpdateModel)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
            {
                return null;
            }

            stock.Symbol = stockUpdateModel.Symbol;
            stock.CompanyName = stockUpdateModel.CompanyName;
            stock.Purchase = stockUpdateModel.Purchase;
            stock.LastDividend = stockUpdateModel.LastDividend;
            stock.Industry = stockUpdateModel.Industry;
            stock.MarketCap = stockUpdateModel.MarketCap;

            await _context.SaveChangesAsync();
            return stock;
        }
    }
}