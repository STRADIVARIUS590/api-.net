using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController: ControllerBase
    {
        private readonly ApplicationDBContext _applicationContext;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDBContext applicationDBContext, IStockRepository istockRepository)
        {
            _applicationContext = applicationDBContext;
            _stockRepository = istockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // var stocks = _applicationContext.Stock.ToList().Select(s => s.ToStockDto());
            
            // var stocks = await _applicationContext.Stock.ToListAsync();
            // var StockDto = stocks.Select(s => s.ToStockDto());

            var stocks = await _stockRepository.GETallAsync();
            var StockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // var stock = await _applicationContext.Stock.FindAsync(id);
            var stock = await _stockRepository.GETByIdAsync(id);
            if(stock == null) return NotFound();
        
            return Ok(stock.ToStockDto());
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = StockDto.ToStockFromRequestDto();

            // await _applicationContext.Stock.AddAsync(stockModel);
            // await _applicationContext.SaveChangesAsync();

            await _stockRepository.CREateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto());

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            // var stockModel = await _applicationContext.Stock.FirstOrDefaultAsync(stock => stock.Id == id);

            // if(stockModel == null) return NotFound();

            // stockModel.Symbol = updateStockDto.Symbol;
            // stockModel.CompanyName = updateStockDto.CompanyName;
            // stockModel.Purchase = updateStockDto.Purchase;
            // stockModel.LastDiv = updateStockDto.LastDiv;
            // stockModel.Industry = updateStockDto.Industry;
            // stockModel.MarketCap = updateStockDto.MarketCap;

            // await _applicationContext.SaveChangesAsync();

            var stockModel = await _stockRepository.UPDateAsync(id, updateStockDto);

            if(null == stockModel) return NotFound();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            // var stockModel = await _applicationContext.Stock.FirstOrDefaultAsync(stock => stock.Id == id);
        
            // if(null == stockModel) return NotFound();

            // _applicationContext.Stock.Remove(stockModel);
         
            // await _applicationContext.SaveChangesAsync();

            var stockModel = await _stockRepository.DELeteAsync(id);
         
            if(null == stockModel) return NotFound();

            return NoContent();
        }
    }
}