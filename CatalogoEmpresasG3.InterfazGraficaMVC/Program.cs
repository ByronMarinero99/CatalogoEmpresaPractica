using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//------------------------------------------------------------------------
//Configuracion de la autentucacion del usuario
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie((Options) =>
{
    Options.LoginPath = new PathString("/Usuario/Login");
    Options.ExpireTimeSpan = TimeSpan.FromHours(8);
    Options.SlidingExpiration = true;
});

var app = builder.Build();

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


//------------------------------------------------------------------------
// Habilitar la autenticacion del usuario
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Contacto}/{action=Index}/{id?}");

app.Run();