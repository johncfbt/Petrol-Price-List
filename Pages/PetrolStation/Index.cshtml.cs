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
    public class IndexModel : PageModel
    {
        private readonly PetrolPrice.Data.PetrolPriceContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(PetrolPrice.Data.PetrolPriceContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }
        public decimal AveragePrice { get; set; }
        public int LastDigit {  get; set; }
        public string MainDigits { get; set; }
        public int Count { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Models.PetrolStation> PetrolStations { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            PriceSort = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Models.PetrolStation> petrolStationsIQ = from s in _context.PetrolStation
                                                                .Include(p => p.PetrolPriceUser)
                                                                select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                petrolStationsIQ = petrolStationsIQ.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "price_desc":
                    petrolStationsIQ = petrolStationsIQ.OrderByDescending(s => s.Price);
                    break;
                default:
                    petrolStationsIQ = petrolStationsIQ.OrderBy(s => s.Price);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 10);


            // Count the number of members inside petrolStationsIQ
            Count = await petrolStationsIQ.CountAsync();

            // Calculate the average price and show MainDigits and LastDigit in stats result
            AveragePrice = await petrolStationsIQ.AverageAsync(s => s.Price);
            MainDigits = (Math.Floor(AveragePrice)/100).ToString("N2");
            //MainDigits = Math.Truncate(AveragePrice / 100).ToString("N2");
            LastDigit = (int)((AveragePrice - Math.Truncate(AveragePrice))*10);

            PetrolStations = await PaginatedList<Models.PetrolStation>.CreateAsync(
                petrolStationsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
        /*
        public async Task OnGetAsync()
        {
            if (_context.PetrolStation != null)
            {
                PetrolStation = await _context.PetrolStation
                    .Include(p => p.PetrolPriceUser)
                    .AsNoTracking()
                    .ToListAsync();
            }
        }*/
    }
}
