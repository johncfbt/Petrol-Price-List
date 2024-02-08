using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PetrolPrice.Areas.Identity.Data;
using PetrolPrice.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PetrolPriceContextConnection") ?? throw new InvalidOperationException("Connection string 'PetrolPriceContextConnection' not found.");

builder.Services.AddDbContext<PetrolPriceContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<PetrolPriceUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PetrolPriceContext>();

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

// Redirect root URL to "/PetrolStation"
app.MapGet("/", context =>
{
    context.Response.Redirect("/PetrolStation");
    return Task.CompletedTask;
});

app.MapRazorPages();

app.Run();
