using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Data;
using Microsoft.EntityFrameworkCore;
using api.DTOs.Stock;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _applicationDBContext;
        public StockRepository(ApplicationDBContext applicationDBContext)
        {

            _applicationDBContext = applicationDBContext;
        }
        public async Task<List<Stock>> GETallAsync(){
            // throw new NotImplementedException();

            return await _applicationDBContext.Stock.Include(c => c.Comments).ToListAsync();   
        }

        public async Task<Stock?> GETByIdAsync(int id){
            return await _applicationDBContext.Stock.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
        }
  
        public async Task<Stock> CREateAsync(Stock stockModel){
            await _applicationDBContext.Stock.AddAsync(stockModel);
            await _applicationDBContext.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UPDateAsync(int id, UpdateStockRequestDto updateStockRequestDto){
            var stockModel = await _applicationDBContext.Stock.FirstOrDefaultAsync(x => x.Id == id);
      
            if (stockModel == null) return null;

            stockModel.Symbol = updateStockRequestDto.Symbol;
            stockModel.CompanyName = updateStockRequestDto.CompanyName;
            stockModel.Purchase = updateStockRequestDto.Purchase;
            stockModel.LastDiv = updateStockRequestDto.LastDiv;
            stockModel.Industry = updateStockRequestDto.Industry;
            stockModel.MarketCap = updateStockRequestDto.MarketCap;

            await _applicationDBContext.SaveChangesAsync();
            return stockModel;

        }
    
        public async Task<Stock?> DELeteAsync(int id){
            var stockModel = await _applicationDBContext.Stock.FirstOrDefaultAsync(x => x.Id == id);
        
            if(null == stockModel) return null;

            _applicationDBContext.Stock.Remove(stockModel);
            await _applicationDBContext.SaveChangesAsync();

            return stockModel;

        }
    }
}