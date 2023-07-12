using BlazorApp1.Infastructure;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] public ILocalStorageService LocalStorageService { get; set; }

        [Inject] public NavigationManager NavigationManager { get; set; }
        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }

        public LoginViewModel LoginData { get; set; }

        protected async Task LoginAsync()
        {
            var token = new SecurityToken
            {
                AccessToken = LoginData.Password,
                UserName = LoginData.UserName,
                ExpiredAt = DateTime.UtcNow.AddDays(1)
            
            };
            await LocalStorageService.SetAsync(nameof(SecurityToken), token);

            NavigationManager.NavigateTo("/", true);
        }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Too long")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class SecurityToken
    {
        public string UserName { get; set; }
        public string AccessToken { get; set; }

        public DateTime ExpiredAt { get; set; }
    }
}