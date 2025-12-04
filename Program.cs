using PharmaClinic.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC қосу
builder.Services.AddControllersWithViews();

// AI сервисін тіркеу
builder.Services.AddSingleton<AiService>();

var app = builder.Build();

// Production режимінде қате беті
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// НЕГІЗГІ МАРШРУТ: / → Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
