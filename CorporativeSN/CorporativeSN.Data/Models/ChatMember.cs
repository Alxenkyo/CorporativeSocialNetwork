using System;
using System.Collections.Generic;
using System.Text;

namespace CorporativeSN.Data.Models
{
    public class ChatMember
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual Users User { get; set; }
        public int ChatId { get; set; }
        public virtual Chats Chat { get; set; }

        public ChatMember(int userId, int chatId)
        {
            UserId = userId;
            ChatId = chatId;
        }

    }
}
