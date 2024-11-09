using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/appuserstock")]
    public class AppUserStockController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IAppUserStockRepository _appUserStockRepo;
        public AppUserStockController(UserManager<AppUser> userManager, IStockRepository stockRepo, IAppUserStockRepository appUserStockRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _appUserStockRepo = appUserStockRepo;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserStock()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userAppStock = await _appUserStockRepo.GetUserStocks(appUser);
            return Ok(userAppStock);

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddUserStock(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if (stock == null)
            {
                return BadRequest("Stock does not exist");
            }

            var userStocks = await _appUserStockRepo.GetUserStocks(appUser);

            if (userStocks.Any(s => s.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Cannot add duplicate stock to user stocks");
            }

            var userStockModel = new AppUserStock
            {
                StockId = stock.Id,
                AppUserId = appUser.Id,
            };

            await _appUserStockRepo.CreateAsync(userStockModel);
            if (userStockModel == null)
            {
                return StatusCode(500, "Was not able to create");
            }
            else
            {
                return Created();
            }

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUserStock(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userStock = await _appUserStockRepo.GetUserStocks(appUser);

            var filteredStock = userStock.Where(s => s.Symbol.ToLower() == symbol.ToLower());

            if (filteredStock.Count() > 0)
            {
                await _appUserStockRepo.DeleteUserStocks(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock is not in user stocks");
            }
            return Ok();
        }

    }
}