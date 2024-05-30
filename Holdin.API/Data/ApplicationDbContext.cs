using Holdin.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Holdin.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
       
    }

    public DbSet<AppUser> AppUsers { get; set; }
}
