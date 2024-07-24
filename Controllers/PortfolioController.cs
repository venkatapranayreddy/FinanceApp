using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


namespace api.Controllers
{
   [Route("api/portfolio")]
   [ApiController]
    public class PortfolioController : ControllerBase
    {
      private readonly UserManager<AppUser> _userManager;
      private readonly IStockRepository _istockRepository;
      private readonly IPortfolioRepository _iPortfolioRepository;
      private readonly IFMPService _iFMPService;


      public PortfolioController(UserManager<AppUser> userManager, IStockRepository istockRepository, IPortfolioRepository iportfolioRepository, IFMPService iFMPService)
      {
        _userManager = userManager;
        _istockRepository = istockRepository;
        _iPortfolioRepository = iportfolioRepository;
        _iFMPService = iFMPService;
      }

      [HttpGet]
      [Authorize]
      public async Task<IActionResult> GetUserPortfolio()
      {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var userPortfolio = await _iPortfolioRepository.GetUserPortfolioAsync(appUser);
        return Ok(userPortfolio);
      }


      [HttpPost]
      [Authorize]
      public async Task<IActionResult> AddPortfolio(string symbol)

      {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _istockRepository.GetBySymbolAsync(symbol);
        if(stock == null)
        {
             stock = await _iFMPService.FindStockBySymbolAsync(symbol);
            if(stock == null)
            {
                return BadRequest("Stock does not exits");
            }
            else{
                await _istockRepository.CreateAsync(stock);
            }
        }

        if(stock == null) {return BadRequest("Stock not found");}

        var userPortfolio = await _iPortfolioRepository.GetUserPortfolioAsync(appUser);

        if(userPortfolio.Any(e => e.Symbol.ToLower() ==symbol.ToLower())) {return BadRequest("Stock Already Exists in the portfolio");}

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id
        };

        await _iPortfolioRepository.CreatePortfolioAsync(portfolioModel);

        if(portfolioModel == null)
        {
            return StatusCode(500, "Cloud not create");
        }
        else {
            return Created();
        }

      }

      [HttpDelete]
      [Authorize]
      public async Task<IActionResult> DeletePortfolio(string symbol)
      
       {

        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);

        var userPortfolio = await _iPortfolioRepository.GetUserPortfolioAsync(appUser);

        var filteredStock = userPortfolio.Where(e => e.Symbol.ToLower() == symbol.ToLower()).ToList();

        if(filteredStock.Count()==1)
        {
            await _iPortfolioRepository.DeletePortfolioAsync(appUser, symbol);
        } 
        else 
        {
            return BadRequest("Stock not in your portfolio");
        }

        return Ok();

      }












      }
}