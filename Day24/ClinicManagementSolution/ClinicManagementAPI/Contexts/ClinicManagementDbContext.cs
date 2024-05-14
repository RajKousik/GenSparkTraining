using ClinicManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace ClinicManagementAPI.Contexts
{
    public class ClinicManagementDbContext : DbContext
    {
        public ClinicManagementDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor() { Id = 101, Name = "Raj", Age = 30, Phone = "9876543321", Speciality="Orthopedic", Experience=5},
                new Doctor() { Id = 102, Name = "Emilia", Age = 29, Phone = "9988776655", Speciality="General", Experience = 6}
                );
        }
    }
}
