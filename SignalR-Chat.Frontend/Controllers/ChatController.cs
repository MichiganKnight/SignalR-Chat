using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Frontend.ViewModels;
using SignalR_Chat.Shared.DTOs;

namespace SignalR_Chat.Frontend.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            ChatViewModel model = new()
            {
                CurrentConversation = new ConversationDto
                {
                    Id = 1,
                    Name = "General",
                    IsGroup = true,

                    Messages =
                    [
                        new MessageDto()
                        {
                            Id = 1,
                            SenderUsername = "Bob",
                            Content = "Hello!",
                            SentAt = DateTime.Now.AddMinutes(-5)
                        },

                        new MessageDto
                        {
                            Id = 2,
                            SenderUsername = "You",
                            Content = "Hey Bob!",
                            SentAt = DateTime.Now.AddMinutes(-4)
                        }
                    ]
                }
            };
            
            return View(model);
        }
    }
}