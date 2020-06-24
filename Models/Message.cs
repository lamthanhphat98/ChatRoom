using System;
using ChatRoom.Models.Chat;

namespace ChatRoom.Models
{
    public class Message
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChatId { get; set; }
        public Chat.Chat Chat { get; set; }
    }
}

