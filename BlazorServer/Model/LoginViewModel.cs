using System.ComponentModel.DataAnnotations;

namespace BlazorServer.Model
{
    public class LoginViewModel
    {
        [Display(Name ="用户名")]
        [Required(ErrorMessage ="{0}是必填项")]
        [StringLength(10,ErrorMessage ="{0}长度超出限制")]
        public string? UserName { get; set; }


        [Display(Name = "密码")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string? Password { get; set; }
    }
}
