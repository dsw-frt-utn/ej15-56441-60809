using Dsw2026Ej15.Domain.Entities; // Ajustá este using si tus entidades están en otra carpet
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
    }
}