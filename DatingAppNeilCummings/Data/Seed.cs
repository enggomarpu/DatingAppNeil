using DatingAppNeilCummings.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DatingAppNeilCummings.Data
{
	public class Seed
	{
		public static async Task SeedData(UserManager<AppUser> userManager)
		{

			if (await userManager.Users.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            foreach (var user in users)
            {
				//context.Users.Add(user);
				await userManager.CreateAsync(user, "Pa$$w0rd");

			}

			//await context.SaveChangesAsync();

        }
	}
}
