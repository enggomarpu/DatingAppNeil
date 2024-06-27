using DatingAppNeilCummings.Data;
using DatingAppNeilCummings.Entities;
using DatingAppNeilCummings.Extension;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
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
