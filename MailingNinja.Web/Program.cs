using MailingNinja.Contracts.Data;
using MailingNinja.Core;
using MailingNinja.Core.Contracts;
using MailingNinja.Infrastructure;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.Configure<MailServerConfig>(builder.Configuration.GetSection("MailServerConfig"));
services.AddSingleton<MailServerConfig>(x => x.GetRequiredService<IOptions<MailServerConfig>>().Value);

// add services from lower layers
services.AddCore();
services.AddInfrastructure();

// Add services to the container.
services.AddControllersWithViews();

var app = builder.Build();


// seeding DB with some sample data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    if (db.Ninjas.Count() == 0)
    {
        var nTypes = new string[] { "Flying", "Running", "Jumping", "Killing", "Master" };
        var colorCodes = new string[] { "Red", "Green", "Blue", "Yellow", "White", "Black" };

        for (int i = 1001; i < 1100; i++)
        {
            var nType = nTypes[i % 5];
            db.Ninjas.Add(new MailingNinja.Contracts.Data.Entities.Ninja
            {
                Bio = $"{nType} Ninja. #{i} is here.",
                Class = $"{nType} Ninjutsu",
                ColorCode = colorCodes[i % 5],
                Name = $"{nType}#{i}"
            });
        }
        await db.CommitAsync();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
