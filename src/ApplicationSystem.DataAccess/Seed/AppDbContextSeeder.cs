using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Saritasa.Tools.Common.Utils;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Options;

namespace ApplicationSystem.DataAccess
{
    /// <summary>
    /// Contains methods for seeding data.
    /// </summary>
    internal static class AppDbContextSeeder
    {
        private const string AdminEmail = "admin@mail.com";
        private const string AdminPassword = "qwertyuiop";

        /// <summary>
        /// Creates test admin user in DB.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        public static async Task SeedFakeAdminUserAsync(UserManager<User> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync(AdminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    FirstName = "Test",
                    LastName = "Admin",
                    UserName = AdminEmail,
                    Email = AdminEmail,
                };

                await userManager.CreateAsync(adminUser, AdminPassword);
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin.ToString());
            }
        }

        /// <summary>
        /// Creates real admin user in DB.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="adminCredentials">Admin credentials.</param>
        public static async Task SeedRealAdminUserAsync(AdminAccountOptions adminCredentials, UserManager<User> userManager)
        {
            if (!await userManager.Users.AnyAsync())
            {
                var adminUser = new User
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    UserName = adminCredentials.Email,
                    Email = adminCredentials.Password,
                };

                await userManager.CreateAsync(adminUser, AdminPassword);
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin.ToString());
            }
        }

        /// <summary>
        /// Seed user roles.
        /// </summary>
        /// <param name="roleManager">Role manager.</param>
        public static async Task SeedUserRolesAsync(RoleManager<Role> roleManager)
        {
            var roles = Enum.GetNames<UserRoles>();

            var diff = CollectionUtils.Diff(await roleManager.Roles.Select(r => r.Name).ToListAsync(), roles);
            foreach (var addedRole in diff.Added)
            {
                await roleManager.CreateAsync(new Role(addedRole));
            }
            foreach (var removedRole in diff.Removed)
            {
                await roleManager.DeleteAsync(new Role(removedRole));
            }
        }
    }
}
