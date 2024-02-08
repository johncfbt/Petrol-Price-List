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
    public class DetailsModel : PageModel
    {
        private readonly PetrolPrice.Data.PetrolPriceContext _context;

        public DetailsModel(PetrolPrice.Data.PetrolPriceContext context)
        {
            _context = context;
        }

        public Models.PetrolStation PetrolStation { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PetrolStation == null)
            {
                return NotFound();
            }

            var petrolstation = await _context.PetrolStation
                .Include(p => p.PetrolPriceUser)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
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
    }
}
