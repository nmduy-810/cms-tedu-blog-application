using Microsoft.AspNetCore.Identity;
using TeduBlog.Core.Domains.Identity;

namespace TeduBlog.Data;

public class SeedData
{
    public async Task SeedAsync(TeduBlogDbContext context)
    {
        var passwordHasher = new PasswordHasher<AppUser>();

        var rootAdminRoleId = Guid.NewGuid();
        if (!context.Roles.Any())
        {
            await context.Roles.AddAsync(new AppRole()
            {
                Id = rootAdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                DisplayName = "Administrator"
            });

            await context.SaveChangesAsync();
        }

        if (!context.Users.Any())
        {
            var userId = Guid.NewGuid();
            var user = new AppUser()
            {
                Id = userId,
                FirstName = "Nguyen",
                LastName = "Duy",
                Email = "duynguyendev810@gmail.com",
                NormalizedEmail = "DUYNGUYENDEV810@GMAIL.COM",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                IsActive = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = false,
                CreatedDate = DateTime.Now
            };

            user.PasswordHash = passwordHasher.HashPassword(user, "123qwe");
            await context.Users.AddAsync(user);

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
            {
                RoleId = rootAdminRoleId,
                UserId = userId
            });

            await context.SaveChangesAsync();
        }
    }
}