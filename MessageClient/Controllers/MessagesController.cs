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
  
  public IActionResult Index(string group = null, string minimumPostDate = null, string maximumPostDate = null, int currentPageNum = 1)
  {
    ViewBag.PgNum = currentPageNum;
    int futurePageNum = currentPageNum + 1;
    List<Message> futureMessages = Message.GetMessages(group, minimumPostDate, maximumPostDate, futurePageNum);
    bool isEmpty = futureMessages.Any();
    ViewBag.EmptyList = isEmpty;
    List<Message> messages = Message.GetMessages(group, minimumPostDate, maximumPostDate, Math.Max(currentPageNum, 1));
    return View(messages);
  }

  public IActionResult IndexPagination(int pageNumModifier, int currentPage)
  {
    int newCurrentPageNum = currentPage + pageNumModifier;
    return RedirectToAction("Index", new { currentPageNum = newCurrentPageNum });
  }

  [HttpPost]
  public IActionResult IndexSearch(string groupSearch, string minimumPostDateSearch, string maximumPostDateSearch)
  {
    return RedirectToAction("Index", new { group = groupSearch, minimumPostDate = minimumPostDateSearch, maximumPostDate = maximumPostDateSearch });
  }

  public async Task<IActionResult> Details(int id)
  {
    string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
    if (currentUser != null)
    {
      ViewBag.CurrentUser = currentUser.UserName;
    }
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
    if (currentUser != null)
    {
      message.user_name = currentUser.UserName;
      message.PostTime = DateTime.UtcNow;
      Message.Post(message);
      return RedirectToAction("Index");
    }
    else
    {
      return View(message);
    }
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
    if (currentUser != null)
    {
      Message.Put(message, currentUser.UserName);
      return RedirectToAction("Details", new { id = message.MessageId });
    }
    else
    {
      return View(message);
    }
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
    if (currentUser != null)
    {
      Message.Delete(id, currentUser.UserName);
      return RedirectToAction("Index");
    }
    else
    {
      return View(id);
    }
  }
}