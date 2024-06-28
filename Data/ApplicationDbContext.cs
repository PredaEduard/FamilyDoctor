using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FamilyDoctor.Models.Appointment> Appointments { get; set; }
    public DbSet<FamilyDoctor.Models.PharmaceuticalProduct> PharmaceuticalProducts { get; set; }
    public DbSet<FamilyDoctor.Models.Review> Reviews { get; set; }
    public DbSet<FamilyDoctor.Models.Notification> Notifications { get; set; }
}
