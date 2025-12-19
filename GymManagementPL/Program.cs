using GymManagementBL;
using GymManagementBL.Service.Classes;
using GymManagementBL.Service.Interfaces;
using GymManagementDL.Data.Context;
using GymManagementDL.Data.Dataseeding;
using GymManagementDL.Repository.Classes;
using GymManagementDL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GymDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
});
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISessionRepository, SessionRepository>();
builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();

var app = builder.Build();
#region DataSeeding
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
//var pendingMigration = dbContext.Database.GetPendingMigrations();
//if (pendingMigration?.Any() ?? false) dbContext.Database.Migrate();
GymDBContextSeeding.SeedData(dbContext, app.Environment.ContentRootPath);
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
