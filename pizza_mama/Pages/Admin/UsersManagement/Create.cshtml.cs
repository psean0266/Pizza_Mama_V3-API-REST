using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using pizza_mama.Data;
using pizza_mama_V2.Models;

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
            
            if (!User.IsInRole("Administrator"))
            {
                // Redirection vers la page "Index" si l'utilisateur n'a pas le rôle "Admin"
                return RedirectToPage("/Admin/UsersManagement/Index");
            }
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

            _context.Accounts.Add(Account);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
