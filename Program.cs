using Gallery.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyContext>();
builder.Services.AddSession(Options => {
    Options.IdleTimeout = TimeSpan.FromMinutes(60);
    Options.Cookie.HttpOnly = true; // Oturum çerezinin yalnızca HTTP üzerinden iletilmesi sağlandı
    Options.Cookie.IsEssential = true; // Çerez, GDPR uyumluluğu için gerekli olarak işaretlendi

});

var app = builder.Build();
app.UseSession();
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
