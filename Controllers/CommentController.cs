using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Repository;
using api.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
       private readonly ICommentRepository _icommentRepository;
       private readonly IStockRepository _istockRepository;
       private readonly UserManager<AppUser> _userManager;
       private readonly IFMPService _ifMPService;

      
       public CommentController(ICommentRepository icommentRepository,IStockRepository istockRepository, UserManager<AppUser> userManager, IFMPService ifMPService){

        _icommentRepository = icommentRepository;
        _istockRepository = istockRepository;
        _userManager = userManager; 
        _ifMPService = ifMPService;
       
       }


    [HttpGet]
    [Authorize]

       public async Task<IActionResult> GetAllComments([FromQuery] CommentQueryObject commentQueryObject)
       
       {

        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var comments = await _icommentRepository.GetAllAsync(commentQueryObject);

        var commentDto = comments.Select(s => s.ToCommentDto());

        return Ok(commentDto);
        
       }


       [HttpGet("{id:int}")]
       public async Task<IActionResult> GetCommentByID([FromRoute] int id){

        var comment = await _icommentRepository.GetByIdAsync(id);
        if(comment == null) {
                return NotFound();
            }

        return Ok(comment.ToCommentDto());
       }


       [HttpPost("{symbol:alpha}")]
       [Authorize]
       public async Task<IActionResult> PostComment([FromRoute]string symbol, [FromBody] CreateCommentDto createCommentDto) {

        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        
        var stock = await _istockRepository.GetBySymbolAsync(symbol);

        if(stock == null)
        {
             stock = await _ifMPService.FindStockBySymbolAsync(symbol);
            if(stock == null)
            {
                return BadRequest("Stock does not exist");
            }
            else{
                await _istockRepository.CreateAsync(stock);
            }
        }

        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var comment = createCommentDto.ToCreateCommentFromCreate(stock.Id); //understand how this line work
        comment.AppUserId = appUser.Id;
        await _icommentRepository.CreateCommentAsync(comment);

        return CreatedAtAction(nameof(GetCommentByID), new {id = comment.Id}, comment.ToCommentDto());
       }


       [HttpPut]
       [Route("{id:int}")]
       public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto){

        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }

        var  comment = await _icommentRepository.UpdateCommentAsync(id, updateCommentDto.ToCommentFromUpdate());

        if(comment == null){
            return NotFound ("Comment not found");
        }   
        return Ok(comment.ToCommentDto());
       }


       [HttpDelete]
       [Route("{id:int}")]
       public async Task<IActionResult> DeleteCommennt([FromRoute]int id)
       {

        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
            var comment = await _icommentRepository.DeleteCommentAsync(id);
            if(comment == null){
                return NotFound ("Not found the comment");
            }

            return Ok(comment);
       }












    }

    
}