using Microsoft.EntityFrameworkCore;

namespace AppointmentRx.DataAccess.Entitites
{
    public class PortalDbContext : DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) {}

        public DbSet<PortalUser> PortalUsers { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Chamber> Chambers { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<FavouriteDoctor> FavouriteDoctors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder){}
    }
}
