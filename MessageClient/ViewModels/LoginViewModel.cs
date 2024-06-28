using System.ComponentModel.DataAnnotations;

namespace MessageClient.ViewModels
{
  public class LoginViewModel
  {
    [Required, MaxLength(50)]
    [Display(Name = "User Name")]
    public string UserName { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}