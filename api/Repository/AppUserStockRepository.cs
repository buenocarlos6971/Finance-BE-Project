using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class AppUserStockRepository : IAppUserStockRepository
    {
        private readonly ApplicationDBContext _context;
        public AppUserStockRepository(ApplicationDBContext context)
        {
            _context = context;

        }

        public async Task<AppUserStock> CreateAsync(AppUserStock userStock)
        {
            await _context.AppUserStocks.AddAsync(userStock);
            await _context.SaveChangesAsync();
            return userStock;
        }

        public async Task<AppUserStock> DeleteUserStocks(AppUser appUser, string symbol)
        {
            var userStockModel = await _context.AppUserStocks.FirstOrDefaultAsync(
                x => x.AppUserId == appUser.Id
                && x.Stock.Symbol.ToLower() == symbol.ToLower()
            );
            if (userStockModel == null)
            {
                return null;
            }
            _context.AppUserStocks.Remove(userStockModel);
            await _context.SaveChangesAsync();
            return userStockModel;
        }

        public async Task<List<Stock>> GetUserStocks(AppUser user)
        {
            return await _context.AppUserStocks.Where(u => u.AppUserId == user.Id)
            .Select(stock => new Stock
            {
                Id = stock.StockId,
                Symbol = stock.Stock.Symbol,
                CompanyName = stock.Stock.CompanyName,
                LastDiv = stock.Stock.LastDiv,
                Industry = stock.Stock.Industry,

            }).ToListAsync();
        }
    }
}