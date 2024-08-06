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
        Task<List<Stock>> GETallAsync();

        Task<Stock?> GETByIdAsync(int id);// nullable
  
        Task<Stock> CREateAsync(Stock stockModel);

        Task<Stock?> UPDateAsync(int id, UpdateStockRequestDto updateStockRequestDto);
    
        Task<Stock?> DELeteAsync(int id);
    }

}