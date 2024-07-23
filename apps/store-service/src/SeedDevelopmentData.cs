using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreService.Infrastructure;
using StoreService.Infrastructure.Models;

namespace StoreService;

public class SeedDevelopmentData
{
    public static async Task SeedDevUser(
        IServiceProvider serviceProvider,
        IConfiguration configuration
    )
    {
        var context = serviceProvider.GetRequiredService<StoreServiceDbContext>();
        var amplicationRoles = configuration
            .GetSection("AmplicationRoles")
            .AsEnumerable()
            .Where(x => x.Value != null)
            .Select(x => x.Value.ToString())
            .ToArray();

        var usernameValue = "test@email.com";
        var passwordValue = "P@ssw0rd!";
        var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        if (await userManager.Users.AnyAsync(x => x.UserName == usernameValue))
        {
            return;
        }

        var user = new IdentityUser
        {
            Email = usernameValue,
            UserName = usernameValue,
            NormalizedUserName = usernameValue.ToUpperInvariant(),
            NormalizedEmail = usernameValue.ToUpperInvariant(),
        };

        var password = new PasswordHasher<IdentityUser>();
        var hashed = password.HashPassword(user, passwordValue);
        user.PasswordHash = hashed;
        await userManager.CreateAsync(user);
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in amplicationRoles)
        {
            await userManager.AddToRoleAsync(user, roleManager.NormalizeKey(role));
        }

        await context.SaveChangesAsync();
    }
}
