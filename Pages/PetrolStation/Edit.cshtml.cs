using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetrolPrice.Data;
using PetrolPrice.Models;

namespace PetrolPrice.Pages.PetrolStation
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly PetrolPrice.Data.PetrolPriceContext _context;

        public EditModel(PetrolPrice.Data.PetrolPriceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.PetrolStation PetrolStation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.PetrolStation == null)
            {
                return NotFound();
            }

            var petrolstation =  await _context.PetrolStation.FirstOrDefaultAsync(m => m.Id == id);
            if (petrolstation == null)
            {
                return NotFound();
            }
            PetrolStation = petrolstation;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PetrolStation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetrolStationExists(PetrolStation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PetrolStationExists(int id)
        {
          return _context.PetrolStation.Any(e => e.Id == id);
        }
    }
}
