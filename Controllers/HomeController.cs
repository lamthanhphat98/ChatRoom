using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ChatRoom.Database;
using ChatRoom.Models;
using ChatRoom.Models.Chat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatRoom.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly AppDbContext dbContext;
        public HomeController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var chatRoom = this.dbContext.Chats
                .Include(x=>x.Users)
                .Where(x=>!x.Users.Any(y=>y.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));

            return View(chatRoom);
            
        }

        [HttpGet("{id}")]
        public IActionResult Chat(int id)
        {
            var chat = this.dbContext.Chats
                .Include(x=>x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View(chat);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromForm] String message,int chatId)
        {
            var chat = this.dbContext.Chats
                .FirstOrDefault(x => x.Id == chatId);
            var objMsg = new Message
            {
                Name = User.Identity.Name,
                Text = message,
                Timestamp = DateTime.Now,
                ChatId = chatId
            };
            this.dbContext.Messages.Add(objMsg);
            await this.dbContext.SaveChangesAsync();
            return RedirectToAction("Chat", new { id = chatId });
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoom([FromForm]String name)
        {

            var chat = new Chat
            {
                Name = name,
                ChatType = ChatType.Room,
            };
            chat.Users.Add(new ChatUser
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Admin
            });

            dbContext.Chats.Add(chat);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> JoinRoom(int id)
        {

            var chatUser = new ChatUser
            {
                ChatId = id,
                UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value,
                UserRole = UserRole.Admin
            };
            dbContext.ChatUsers.Add(chatUser);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Chat",new { id = id});
        }
    }
}