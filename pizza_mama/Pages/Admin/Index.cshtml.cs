using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool DisplayInvalidAccountMessage = false;
          private readonly pizza_mama.Data.DataContext _context;

        IConfiguration configuration;

        public IndexModel (IConfiguration configuration, pizza_mama.Data.DataContext context)
        {   
            this.configuration = configuration;
            this._context = context;    
        }
        public IActionResult OnGet() 
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string username, string password, string ReturnUrl) 
        {
            var authSection = configuration.GetSection("Auth");
            //string adminLogin = authSection["AdminLogin"];
            //string AdminPassword = authSection["AdminPassword"];

            //if((username == adminLogin)&& (password == AdminPassword)) {

            //    DisplayInvalidAccountMessage = false;
            //    var claims = new List<Claim>
            //{
            //        new Claim(ClaimTypes.Name, username)
            //    };
            //    var claimsIdentity = new ClaimsIdentity(claims, "Login");
            //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
            //    ClaimsPrincipal(claimsIdentity));
            //    return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
                
            //}


            if( username != null) {
            
                var user =  await _context.Accounts.FirstOrDefaultAsync(u=> u.Username == username && u.Password==password);

                if (user != null)
                {

                    DisplayInvalidAccountMessage = false;
                    var claims = new List<Claim>
                    {
                       new Claim(ClaimTypes.Name, user.Username),
                       new Claim(ClaimTypes.Role, user.Roles)
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, "Login");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
                    ClaimsPrincipal(claimsIdentity));
                    return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl);
                }

            }

            DisplayInvalidAccountMessage = true;
            return Page();
        }
        
        public async Task<RedirectResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
    }
}
