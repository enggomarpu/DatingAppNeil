using DatingAppNeilCummings.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DatingAppNeilCummings.Data
{
	public class Seed
	{
		public static async Task SeedData(DBContext context)
		{

			if (await context.AppUsers.AnyAsync()) return;

			var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
			var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
			var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);

            foreach (var item in users)
            {
                context.AppUsers.Add(item);
            }

			await context.SaveChangesAsync();

        }
	}
}
