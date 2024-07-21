using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using api.Dtos.Stocks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using api.Interfaces;
using api.Helpers;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace api.Controllers

{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase

    {
        private readonly ApplicationDBContext _applicationDBContext;
        private readonly IStockRepository _iStockRepository;

        public StockController(ApplicationDBContext applicationDBContext, IStockRepository iStockRepository)

        {
            _applicationDBContext = applicationDBContext;
            _iStockRepository = iStockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject queryObject){
            if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

            var stocks = await _iStockRepository.GetAllAsync(queryObject);

            var stock = stocks.Select(s => s.ToStockDto()).ToList();

            return Ok(stock);
        }



        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

            var stock =  await _iStockRepository.GetByIdAsync(id);
            if(stock == null) {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockRequestDto)
        {
            if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
            var stock = await _iStockRepository.UpdateAsync(id, updateStockRequestDto);
            if(stock == null){

                return NotFound();
            }
            
            return Ok(stock.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>  Delete([FromRoute] int id){
            
                if(!ModelState.IsValid){
            return BadRequest(ModelState);
                }
                var stock  = await _iStockRepository.DeleteAsync(id);

                if(stock == null)
                {
                    return NotFound();
                }
                return NoContent();
            
        }


        [HttpPost]
        // [Authorize]
        public async  Task<IActionResult> Create([FromForm] CreateStockRequestDto createStockRequestDto, IFormFile file) {
            if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
            var fileUploader = await _iStockRepository.Upload(file);

            var stock = createStockRequestDto.ToStockFromCreateDto();
            stock.FilePath = fileUploader;
            await _iStockRepository.CreateAsync(stock);
           
            return CreatedAtAction(nameof(GetById), new {id = stock.Id}, stock.ToStockDto());
        }


   















    }
}