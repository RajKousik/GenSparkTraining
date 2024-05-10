using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestTrackerModelLibrary
{
    public class RequestTrackerContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=823CBX3\DEMOINSTANCE;Integrated Security=True;Initial Catalog=dbEmployeeTrackerCF;");
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestSolution> RequestSolutions { get; set; }
        public DbSet<SolutionFeedback> SolutionFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
            (
                new Employee { Id = 101, Name = "Ramu", Password = "ramu123", Role = "Admin" },
                new Employee { Id = 102, Name = "Somu", Password = "somu123", Role = "Admin" },
                new Employee { Id = 103, Name = "Bimu", Password = "bimu123", Role = "User" }
            );

            modelBuilder.Entity<SolutionFeedback>().HasKey(r => r.FeedbackId);

            modelBuilder.Entity<Request>()
               .HasOne(r => r.RaisedByEmployee)
               .WithMany(e => e.RequestsRaised)
               .HasForeignKey(r => r.RequestRaisedBy)
               .OnDelete(DeleteBehavior.Restrict)
               .IsRequired();

            modelBuilder.Entity<Request>()
               .HasOne(r => r.RequestClosedByEmployee)
               .WithMany(e => e.RequestsClosed)
               .HasForeignKey(r => r.RequestClosedBy)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestSolution>()
                .HasOne(rs => rs.RequestRaised)
                .WithMany(r => r.RequestSolutions)
                .HasForeignKey(rs => rs.RequestId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<RequestSolution>()
                .HasOne(rs => rs.SolvedByEmployee)
                .WithMany(e => e.SolutionsProvided)
                .HasForeignKey(rs => rs.SolvedBy)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<SolutionFeedback>()
                .HasOne(sf => sf.Solution)
                .WithMany(s => s.Feedbacks)
                .HasForeignKey(sf => sf.SolutionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            modelBuilder.Entity<SolutionFeedback>()
              .HasOne(sf => sf.FeedbackByEmployee)
              .WithMany(e => e.FeedbacksGiven)
              .HasForeignKey(sf => sf.FeedbackBy)
              .OnDelete(DeleteBehavior.Restrict)
              .IsRequired();
        }
    }
}
