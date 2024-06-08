using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<DBContext>(x =>
{
	x.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddIdentityCore<AppUser>(opt => {
//	opt.Password.RequireNonAlphanumeric = false;
//}).AddEntityFrameworkStores<DBContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>(opt => {
	
	opt.Lockout.AllowedForNewUsers = true;
	opt.Lockout.MaxFailedAccessAttempts = 3;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	opt.Password.RequireNonAlphanumeric = false;

}).AddEntityFrameworkStores<DBContext>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<DBContext>();
	var manager = services.GetRequiredService<UserManager<AppUser>>();
	await context.Database.MigrateAsync();
	await Seed.SeedData(manager);
}
catch(Exception ex)
{
	var logService = services.GetService<ILogger<Program>>();
	logService.LogError(ex, "Error occured during migration");
}


app.Run();
