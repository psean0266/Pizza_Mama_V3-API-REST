using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using pizza_mama.Data;
using pizza_mama_V2.Models;

namespace pizza_mama_V2.Pages.Admin.UsersManagement
{
    
    public class IndexModel : PageModel
    {
        private readonly pizza_mama.Data.DataContext _context;

        public IndexModel(pizza_mama.Data.DataContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get;set; }

        public async Task OnGetAsync()
        {
            Account = await _context.Accounts.ToListAsync();
        }
    }
}
