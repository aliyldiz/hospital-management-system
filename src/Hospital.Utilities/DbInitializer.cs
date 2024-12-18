using System.Xml;
using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Utilities;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _context;
    
    public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _context = context;
    }
    
    public void Initialize()
    {
        try
        {
            if (_context.Database.GetPendingMigrations().Count()>0)
            {
                _context.Database.Migrate();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        Console.WriteLine(!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult());
        if (!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Patient)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(WebSiteRoles.WebSite_Doctor)).GetAwaiter().GetResult();

            var a = _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@gmail.com"
            }, "Admin@123").GetAwaiter().GetResult();
            a.Errors.ToList().ForEach(x => Console.WriteLine(x.Description));
            
            var appuser = _context.ApplicationUsers.FirstOrDefault(x => x.Email == "admin@gmail.com");
            if (appuser != null)
            {
                _userManager.AddToRoleAsync(appuser, WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult();
            }
        }
    }
}