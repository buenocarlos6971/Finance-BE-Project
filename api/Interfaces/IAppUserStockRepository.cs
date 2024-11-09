using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IAppUserStockRepository
    {
        Task<List<Stock>> GetUserStocks(AppUser user);
        Task<AppUserStock> CreateAsync(AppUserStock userStock);
        Task<AppUserStock> DeleteUserStocks(AppUser appUser, String symbol);

    }
}