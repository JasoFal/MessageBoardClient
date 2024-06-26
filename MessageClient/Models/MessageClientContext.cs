using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MessageClient.Models
{
  public class MessageClientContext : IdentityDbContext<ApplicationUser>
  {
    public MessageClientContext(DbContextOptions options) : base(options) { }
  }
}