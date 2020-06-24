using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatRoom.Models
{
    public class ChatUser
    {
        public String UserId { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat.Chat Chat { get; set; }
        public UserRole UserRole { get; set; }
    }
}
