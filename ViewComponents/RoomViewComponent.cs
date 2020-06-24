using ChatRoom.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatRoom.ViewComponents
{
    public class RoomViewComponent : ViewComponent
    {
        private readonly AppDbContext dbContext;
        public RoomViewComponent(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var chats = this.dbContext.ChatUsers
                .Include(x=>x.Chat)
                .Where(x=>x.UserId == HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select(x=>x.Chat)
                .ToList();
            return View(chats);
        }
    }
}
