using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PetrolPrice.Models;

namespace PetrolPrice.Areas.Identity.Data;

// Add profile data for application users by adding properties to the PetrolPriceUser class
public class PetrolPriceUser : IdentityUser
{
    public ICollection<PetrolStation> PetrolStation { get; set; }
}

