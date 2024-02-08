using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PetrolPrice.Areas.Identity.Data;
using PetrolPrice.Data;
using PetrolPrice.Models;

namespace PetrolPrice.Pages.PetrolStation
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly PetrolPrice.Data.PetrolPriceContext _context;
        private readonly UserManager<PetrolPriceUser> _userManager;


        public CreateModel(PetrolPrice.Data.PetrolPriceContext context, UserManager<PetrolPriceUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.PetrolStation PetrolStation { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the current logged-in user
            var currentUser = await _userManager.GetUserAsync(User);

            // Set the WebApplication1User property of the UserAddress to the current user
            PetrolStation.PetrolPriceUser = currentUser;

            _context.PetrolStation.Add(PetrolStation);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
