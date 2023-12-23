using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pizza_mama.Data;
using pizza_mama_V2.Models;

namespace pizza_mama_V2.Pages.UsersManagement
{
    public class CreateModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext _context;

        public CreateModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
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
            //Account.AccountId = GetHashedAccountId();

            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        //private string GetHashedAccountId()
        //{
        //    // Vous pouvez utiliser une fonction de hachage ici, par exemple, avec Guid.NewGuid()
        //    return Guid.NewGuid().ToString();
        //}
    }
}
