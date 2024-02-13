using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PetrolPrice.Data;
using PetrolPrice.Models;

namespace PetrolPrice.Pages.PetrolStation
{
    public class DeleteModel : PageModel
    {
        private readonly PetrolPrice.Data.PetrolPriceContext _context;
        public DeleteModel(PetrolPrice.Data.PetrolPriceContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Models.PetrolStation PetrolStation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PetrolStation == null)
            {
                return NotFound();
            }

            var petrolstation = await _context.PetrolStation.FirstOrDefaultAsync(m => m.Id == id);

            if (petrolstation == null)
            {
                return NotFound();
            }
            else 
            {
                PetrolStation = petrolstation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.PetrolStation == null)
            {
                return NotFound();
            }
            var petrolstation = await _context.PetrolStation.FindAsync(id);

            if (petrolstation != null)
            {
                PetrolStation = petrolstation;
                _context.PetrolStation.Remove(PetrolStation);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
