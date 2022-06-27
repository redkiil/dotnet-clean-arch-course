using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void SeedRoles()
        {
            if(!_roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
            if(!_roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";

                IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers()
        {
            if(_userManager.FindByEmailAsync("usuario@localhost").Result == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();
                IdentityResult result = _userManager.CreateAsync(user, "Numsei#2021").Result;
                if(result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
             if(_userManager.FindByEmailAsync("admin@localhost").Result == null)
            {
                ApplicationUser adm = new ApplicationUser();
                adm.UserName = "admin@localhost";
                adm.Email = "admin@localhost";
                adm.NormalizedUserName = "ADMIN@LOCALHOST";
                adm.NormalizedEmail = "ADMIN@LOCALHOST";
                adm.EmailConfirmed = true;
                adm.LockoutEnabled = false;
                adm.SecurityStamp = Guid.NewGuid().ToString();
                IdentityResult resultAdm = _userManager.CreateAsync(adm, "Numsei#2021").Result;
                if(resultAdm.Succeeded)
                {
                    _userManager.AddToRoleAsync(adm, "Admin").Wait();
                }
            }

        }
    }
}