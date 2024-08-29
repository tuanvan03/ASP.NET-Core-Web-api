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
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stocks = await _stockRepository.GetAllStocks();
            var stockDTO = stocks.Select(s => s.ToStockDTO());

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stock = await _stockRepository.GetStockById(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockCreatedModel stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stockmodel = stock.ToStockFromCreatedModel();
            await _stockRepository.CreateStock(stockmodel);

            return CreatedAtAction(nameof(GetById), new { id = stockmodel.Id }, stockmodel.ToStockDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, StockUpdateModel stockUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stock = await _stockRepository.UpdateStock(id, stockUpdateModel);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDTO());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStock([FromRoute] int id)
        {
            var stock = await _stockRepository.DeleteStock(id);
            if (stock == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}