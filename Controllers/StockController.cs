using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockRepository.GetAllStocks();
            var stockDTO = stocks.Select(s => s.ToStockDTO());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockCreatedModel stock)
        {
            var stockmodel = stock.ToStockFromCreatedModel();
            await _context.Stocks.AddAsync(stockmodel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = stockmodel.Id }, stockmodel.ToStockDTO());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, StockUpdateModel stockUpdateModel)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Id == id);

            if (stock == null)
            {
                return NotFound();
            }

            stock.Symbol = stockUpdateModel.Symbol;
            stock.CompanyName = stockUpdateModel.CompanyName;
            stock.Purchase = stockUpdateModel.Purchase;
            stock.LastDividend = stockUpdateModel.LastDividend;
            stock.Industry = stockUpdateModel.Industry;
            stock.MarketCap = stockUpdateModel.MarketCap;

            await _context.SaveChangesAsync();
            return Ok(stock.ToStockDTO());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(e => e.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}