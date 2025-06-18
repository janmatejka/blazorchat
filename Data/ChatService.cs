
using System.Collections.Generic;

namespace blazorchat3.Data
{
    public class ChatService
    {
        public event Action? OnMessagesChanged;
        private readonly List<ChatMessage> _messages = new();
        public IReadOnlyList<ChatMessage> Messages => _messages;

        public void ClearMessages()
        {
            _messages.Clear();
            OnMessagesChanged?.Invoke();
        }

        public void AddMessage(string user, string text)
        {
            _messages.Add(new ChatMessage { User = user, Text = text });
            OnMessagesChanged?.Invoke();
        }

        public void EditMessage(ChatMessage message, string newText)
        {
            if (_messages.Contains(message))
            {
                message.Text = newText;
                OnMessagesChanged?.Invoke();
            }
        }
    }

    public class ChatMessage
    {
        public string User { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
    }
}
