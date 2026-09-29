using CareerAdvisor.Data;
using CareerAdvisor.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlite("Data Source=career.db"));
builder.Services.AddScoped<RecommendationService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Run(db);
}

app.UseStaticFiles();
app.UseRouting();
app.MapDefaultControllerRoute();
app.Run();
