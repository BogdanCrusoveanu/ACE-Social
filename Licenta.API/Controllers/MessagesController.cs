using AutoMapper;
using Licenta.API.Data;
using Licenta.API.Services;
using Licenta.Data;
using Licenta.Dtos;
using Licenta.Helpers;
using Licenta.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Licenta.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("users/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;
        private readonly IUserService _userService;

        public MessagesController(IMessagesService messagesService, IUserService userService)
        {
            _messagesService = messagesService;
            _userService = userService;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _messagesService.GetMessage(id);

            if (messageFromRepo == null)
                return NotFound();

            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagesForUser(int userId, [FromQuery]MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageParams.UserId = userId;

            var messagesFromRepo = await _messagesService.GetMessagesForUser(messageParams);

            var messages = _messagesService.MapMessagesForUser(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
                messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messages);
        }

        [HttpGet("thread/{recipientId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipientId)
        {
            var uid = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messagesFromRepo = await _messagesService.GetMessageThread(userId, recipientId);

            var messageThread = _messagesService.MapMessagesForThread(messagesFromRepo);

            return Ok(messageThread);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
            var sender = await _userService.GetUser(userId);

            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageForCreationDto.SenderId = userId;

            var recipient = await _userService.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
                return BadRequest("Could not find user!");

            var message = _messagesService.MapCreatedMessage(messageForCreationDto);

            if (await _userService.SaveChangesInContext())
            {
                var messageToReturn = _messagesService.MapMessageToReturn(message);
                return CreatedAtRoute("GetMessage", new { userId, id = message.Id }, messageToReturn);
            }

            throw new Exception("Creating the message failed on save");
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messageFromRepo = await _messagesService.GetMessage(id);

            if (messageFromRepo.SenderId == userId)
            {
                messageFromRepo.SenderDeleted = true;
            }

            if (messageFromRepo.RecipientId == userId)
            {
                messageFromRepo.RecipientDeleted = true;
            }

            if (messageFromRepo.SenderDeleted && messageFromRepo.RecipientDeleted)
            {
                _messagesService.DeleteMessage(messageFromRepo);
            }

            if (await _userService.SaveChangesInContext())
            {
                return NoContent();
            }

            throw new Exception("Error deleting the message!");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var message = await _messagesService.GetMessage(id);

            if (message.RecipientId != userId)
            {
                return Unauthorized();
            }

            _messagesService.MarkAsRead(message);

            await _userService.SaveChangesInContext();

            return NoContent();
        }
    }
}
