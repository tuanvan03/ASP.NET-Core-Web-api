using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] StockQuery query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var stocks = await _stockRepository.GetAllStocks(query);
            var stockDTO = stocks.Select(s => s.ToStockDTO());

            return Ok(stocks);
        }

        [HttpGet("{id:int}")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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