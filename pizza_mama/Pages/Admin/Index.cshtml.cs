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
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace pizza_mama.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public bool DisplayInvalidAccountMessage = false;
        public bool IsDeveloppementMode = false;
        private readonly pizza_mama.Data.DataContext _context;

        IConfiguration configuration;

        public IndexModel(IConfiguration configuration, pizza_mama.Data.DataContext context, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this._context = context;


            if (env.IsDevelopment())
            {
                IsDeveloppementMode = true;
            }
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
            string adminLogin = authSection["AdminLogin"];
            string AdminPassword = authSection["AdminPassword"];

            //if ((username == adminLogin) && (password == AdminPassword))
            //{

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


            if (username != null)
            {

                //    //var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                   var user = await _context.Accounts.FirstOrDefaultAsync(u => u.Username == username); 

                //    //   if (user != null && VerifyPassword(password, user.Password))

                //    // if (user != null &&(password == user.Password))

                 if (user != null && DecryptPassword(user.Password) == password)
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


        //private bool VerifyPassword(string inputPassword, string hashedPassword)
        //{
        //    // Vous devez implémenter la logique de vérification du mot de passe
        //    using (var sha = SHA256.Create())
        //    {
        //        // Récupérer le sel depuis le hachage stocké (si votre modèle de données le permet)
        //        byte[] salt = Convert.FromBase64String(hashedPassword.Split('$')[0]);

        //        // Concaténer le mot de passe fourni avec le sel
        //        byte[] combined = Encoding.UTF8.GetBytes(inputPassword + Convert.ToBase64String(salt));

        //        // Calculez le nouveau hachage
        //        byte[] hashedInputPassword = sha.ComputeHash(combined);

        //        // Comparez les deux hachages
        //       return hashedPassword.SequenceEqual(Convert.ToBase64String(hashedInputPassword));

        //    }
        //}

        private bool VerifyPassword(string inputPassword, string storedPassword)
        {
            using (var sha = SHA256.Create())
            {
                // Split pour obtenir le sel stocké
                string[] passwordParts = storedPassword.Split('$');
                if (passwordParts.Length != 2)
                {
                    // Gérer une situation incorrecte (par exemple, le format de hachage est invalide)
                    return false;
                }

                string storedSalt = passwordParts[0];
                string storedHash = passwordParts[1];

                // Convertir le sel stocké en tableau de bytes
                byte[] salt = Convert.FromBase64String(storedSalt);

                // Concaténer le mot de passe fourni avec le sel
                byte[] combined = Encoding.UTF8.GetBytes(inputPassword + Convert.ToBase64String(salt));

                // Calculer le hachage
                byte[] hashedInputPassword = sha.ComputeHash(combined);

                // Comparer le hachage calculé avec le hachage stocké
                return storedHash == Convert.ToBase64String(hashedInputPassword);
            }
        }

        public static string EncryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] storePassword = ASCIIEncoding.ASCII.GetBytes(password);
                string encryptedPassword = Convert.ToBase64String(storePassword);
                return encryptedPassword;
            }
        }

        public static string DecryptPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return null;
            }
            else
            {
                byte[] encryptedPassword = Convert.FromBase64String(password);
                string decryptedPassword = ASCIIEncoding.ASCII.GetString(encryptedPassword);
                return decryptedPassword;
            }
        }

        public async Task<RedirectResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/Admin");
        }
    }
}
