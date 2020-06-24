using ChatRoom.Database;
using ChatRoom.Hubs;
using ChatRoom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> chat;
        public ChatController(IHubContext<ChatHub> chat)
        {
            this.chat = chat;
        }
        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinChat(String connectionId, String roomName)
        {
            await chat.Groups.AddToGroupAsync(connectionId,roomName);
            return Ok();
        }
        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(String connectionId, String roomName)
        {
            await chat.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(int chatId,String roomName, String message,[FromServices] AppDbContext dbContext)
        {
            var objMsg = new Message
            {
                Name = User.Identity.Name,
                Text = message,
                Timestamp = DateTime.Now,
                ChatId = chatId
            };

            dbContext.Messages.Add(objMsg);
            await dbContext.SaveChangesAsync();

            await chat.Clients.Group(roomName).SendAsync("ReceiveMessage",new { 
                 Text = objMsg.Text,
                 Name = objMsg.Name,
                 Timestamp = objMsg.Timestamp.ToString("dd/MM/yyyy hh:mm:ss")
            });;
            return Ok();
        }
    }
}
