using Microsoft.AspNetCore.Identity;

namespace Ajj.Services
{
    public static class IdentityDataIntializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (!roleManager.RoleExistsAsync("candidate").Result)
            {
                roleManager.CreateAsync(new IdentityRole("candidate"));
            }
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                roleManager.CreateAsync(new IdentityRole("user"));
            }
            if (!roleManager.RoleExistsAsync("client").Result)
            {
                roleManager.CreateAsync(new IdentityRole("client"));
            }
        }
    }
}