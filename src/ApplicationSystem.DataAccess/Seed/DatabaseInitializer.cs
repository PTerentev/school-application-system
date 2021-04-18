using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ApplicationSystem.Domain.Entities;
using ApplicationSystem.Infrastructure.Common.Options;
using Extensions.Hosting.AsyncInitialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

[assembly: InternalsVisibleTo("ApplicationSystem.Web")]

namespace ApplicationSystem.DataAccess
{
    /// <summary>
    /// Database initializer.
    /// </summary>
    internal class DatabaseInitializer : IAsyncInitializer
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext dbContext;
        private readonly IHostingEnvironment environment;
        private readonly RoleManager<Role> roleManager;
        private readonly AdminAccountOptions adminOptions;

        /// <summary>
        /// Constructor. Performs migration and data seed.
        /// </summary>
        /// <param name="userManager">User manager.</param>
        /// <param name="roleManager">Role manager.</param>
        /// <param name="dbContext">Database context.</param>
        /// <param name="environment">Environment.</param>
        /// <param name="adminOptions">Admin options.</param>
        public DatabaseInitializer(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            ApplicationDbContext dbContext,
            IHostingEnvironment environment,
            IOptions<AdminAccountOptions> adminOptions)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.environment = environment;
            this.roleManager = roleManager;
            this.adminOptions = adminOptions.Value;
        }

        /// <inheritdoc/>
        public async Task InitializeAsync()
        {
            await dbContext.Database.EnsureCreatedAsync();
            await dbContext.Database.MigrateAsync();
            await AppDbContextSeeder.SeedUserRolesAsync(roleManager);
            if (environment.IsDevelopment())
            {
                await AppDbContextSeeder.SeedFakeAdminUserAsync(userManager);
            }
            else
            {
                await AppDbContextSeeder.SeedRealAdminUserAsync(adminOptions, userManager);
            }
        }
    }
}
