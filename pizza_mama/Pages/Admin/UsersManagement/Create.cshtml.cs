using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pizza_mama.Data;
using pizza_mama_V2.Models;
using pizza_mama.Pages.Admin;


namespace pizza_mama_V2.Pages.Admin.UsersManagement
{
  //  [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext _context;

        public CreateModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            
            //if (!User.IsInRole("Administrator"))
            //{
            //    // Redirection vers la page "Index" si l'utilisateur n'a pas le rôle "Admin"
            //    return RedirectToPage("/Admin/UsersManagement/Index");
            //}
            return Page();
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
        [BindProperty]
        public Account Account { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //  Account.Password = HashPassword(Account.Password);

            //     Account.Password = EncryptPassword(Account.Password);

            //Account.Password = pizza_mama.Pages.Admin.IndexModel.EncryptPassword(Account.Password);

            Account.Password = EncryptPassword(Account.Password);



            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }


        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                // Générer un sel aléatoire
                byte[] salt = GenerateSalt();

                // Concaténer le mot de passe avec le sel
                byte[] combined = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt));

                // Calculer le hachage
                byte[] hashedPassword = sha.ComputeHash(combined);

                // Retourner le résultat haché
                return Convert.ToBase64String(hashedPassword);
            }
        }

        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

    }
}
