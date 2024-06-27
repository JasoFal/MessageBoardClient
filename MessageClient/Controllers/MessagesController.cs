using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MessageClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MessageClient.Controllers;

public class MessagesController : Controller
{
  private readonly UserManager<ApplicationUser> _userManager;
  private readonly MessageClientContext _db;

  public MessagesController(UserManager<ApplicationUser> userManager, MessageClientContext db)
  {
    _userManager = userManager;
    _db = db;
  }
  
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
  public async Task<ActionResult> Create(Message message)
  {
    string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    message.user_name = currentUser.UserName;
    Message.Post(message);
    return RedirectToAction("Index");
  }

  public ActionResult Edit(int id)
  {
    Message message = Message.GetDetails(id);
    return View(message);
  }

  [HttpPost]
  public async Task<ActionResult> Edit(Message message)
  {
    string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    Message.Put(message, currentUser.UserName);
    return RedirectToAction("Details", new { id = message.MessageId });
  }

  public ActionResult Delete(int id)
  {
    Message message = Message.GetDetails(id);
    return View(message);
  }

  [HttpPost, ActionName("Delete")]
  public async Task<ActionResult> DeleteConfirmed(int id)
  {
    string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    Message.Delete(id, currentUser.UserName);
    return RedirectToAction("Index");
  }
}

