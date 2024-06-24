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

  public IActionResult Details(int id)
  {
    Message message = Message.GetDetails(id);
    return View(message);
  }

  public ActionResult Create()
  {
    return View();
  }

  [HttpPost]
  public ActionResult Create(Message message)
  {
    Message.Post(message);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Message message = Message.GetDetails(id);
    return View(message);
  }

  [HttpPost]
  public ActionResult Edit(Message message)
  {
    Message.Put(message);
    return RedirectToAction("Details", new { id = Message.MessageId});
  }
}

