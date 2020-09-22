using System;
using Microsoft.EntityFrameworkCore;
using Routine.Api.Entities;

namespace Routine.Api.Data
{
    public class RoutineDbContext : DbContext
    {
        public RoutineDbContext(DbContextOptions<RoutineDbContext> options)
            : base(options)
        {

        }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasIndex(x => x.Name)
                .IsUnique(); // 唯一键

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Company)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.CompanyId) // 外键
                .HasPrincipalKey(x => x.Id) 
                .OnDelete(DeleteBehavior.Cascade); // 级联删除

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = Guid.Parse("9b6ff3aa-d91d-4086-baea-5ec3564a31be"),
                    Name = "Microsoft",
                    Introduction = "Greate Company"
                },
                new Company
                {
                    Id = Guid.Parse("beeba841-f929-4f56-bfef-644bbc0e58d7"),
                    Name = "Google",
                    Introduction = "Don't be evil "
                },
                new Company
                {
                    Id = Guid.Parse("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"),
                    Name = "Alipapa",
                    Introduction = "Fubao Company"
                });

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.Parse("9b6ff3aa-d91d-4086-baea-5ec3564a31be"),
                    DateOfBirth = new DateTime(1976,1,2),
                    EmployeeNo = "MSFT231",
                    FirstName = "Nick",
                    LastName = "Carter",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.Parse("9b6ff3aa-d91d-4086-baea-5ec3564a31be"),
                    DateOfBirth = new DateTime(1998, 5, 12),
                    EmployeeNo = "MSFT001",
                    FirstName = "Lisa",
                    LastName = "Jame",
                    Gender = Gender.女
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.Parse("beeba841-f929-4f56-bfef-644bbc0e58d7"),
                    DateOfBirth = new DateTime(1988, 3, 2),
                    EmployeeNo = "G001",
                    FirstName = "Dick",
                    LastName = "Harden",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.Parse("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"),
                    DateOfBirth = new DateTime(1982, 9, 22),
                    EmployeeNo = "A001",
                    FirstName = "马",
                    LastName = "云",
                    Gender = Gender.男
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    CompanyId = Guid.Parse("9a2193e3-2c2d-4625-b346-fbc5e789f0ad"),
                    DateOfBirth = new DateTime(1986, 11, 22),
                    EmployeeNo = "A002",
                    FirstName = "王",
                    LastName = "尼玛",
                    Gender = Gender.女
                });
        }
    }
}
