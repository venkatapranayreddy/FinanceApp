using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment){
            
            return new CommentDto
            {
                    Id = comment.Id,
                    Title =comment.Title,
                    Content = comment.Content,
                    CreatedOn = comment.CreatedOn,
                    CreatedBy = comment.appUser.UserName,
                    StockId = comment.StockId
            };
        }


        public static Comment ToCreateCommentFromCreate(this CreateCommentDto createCommentDto, int StockId){

            return new Comment 
            {
                Title = createCommentDto.Title,
                Content = createCommentDto.Content,
                StockId = StockId
            };
        } 


        public static Comment ToCommentFromUpdate(this UpdateCommentDto updateCommentDto){

            return new Comment 
            {
                Title = updateCommentDto.Title,
                Content = updateCommentDto.Content
            };
        }    
        
    }
}