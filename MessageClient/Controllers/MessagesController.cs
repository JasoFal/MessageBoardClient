using Microsoft.AspNetCore.Mvc;
using MessageClient.Models;

namespace MessageClient.Controllers;

public class MessagesController : Controller
{
  public IActionResult Index()
  {
    List<Message> messages = Message.GetMessages();
    return View(messages);
  }

  
}

