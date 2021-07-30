using Obsidian.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CzomPack.Obsidian
{
    /// <summary>
    /// 
    /// </summary>
    public class ChatMessage// : IChatMessage
    {
        private IChatMessage _chatMessage;
        public ChatMessage()
        {
            _chatMessage = IChatMessage.CreateNew();
        }
        
        public ChatMessage(IChatMessage chatMessage)
        {
            SetInterface(chatMessage);
        }

        public ChatMessage AddExtra(ChatMessage chatMessage) => (ChatMessage)_chatMessage.AddExtra(chatMessage.GetInterface());
        public ChatMessage AddExtra(IEnumerable<ChatMessage> chatMessages) => (ChatMessage)_chatMessage.AddExtra(chatMessages.Select(x=>x.GetInterface()));
        public IChatMessage GetInterface() => _chatMessage;
        public void SetInterface(IChatMessage chatMessage) => this._chatMessage = chatMessage;
    }
}
