using BL;
using DL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//INYECCIÓN DE DEPENDENCIAS:
    //1. Agregar la cadena de conexión en el JSON de PL

//2. Agregar la cadena de conexión, que previmente colocamos en el JSON
builder.Services.AddDbContext<RestauranteCoreContext>(options 
    => options.UseSqlServer(builder.Configuration.GetConnectionString("RestauranteCore")));

//3. Apuntamos a la interfaz y a la clase de BL. (paso 4 en Interfaz BL)
builder.Services.AddScoped<BL.IRestaurante, BL.Restaurante>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
