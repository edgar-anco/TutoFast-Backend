using Microsoft.EntityFrameworkCore;
using StudentHub_API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentHub_API.Domain.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        
        public DbSet<Career> Careers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Career
            builder.Entity<Career>().ToTable("Careers");
            builder.Entity<Career>().HasKey(career => career.Id); //Primary Key
            builder.Entity<Career>().Property(career => career.Id).IsRequired().ValueGeneratedOnAdd(); //Auto Generate a Primary Key
            builder.Entity<Career>().Property(career => career.Name).IsRequired().HasMaxLength(50);
            //Career One to many with Document
            builder.Entity<Career>()
               .HasMany(career => career.Documents)
               .WithOne(document => document.Career)
               .HasForeignKey(document => document.CareerId);


            //Course
            builder.Entity<Course>().ToTable("Courses");
            builder.Entity<Course>().HasKey(p => p.Id);
            builder.Entity<Course>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Course>().Property(p => p.Name).IsRequired().HasMaxLength(40);

            //Course One to many with Tutor
            builder.Entity<Course>()
               .HasMany(course => course.Tutors)
               .WithOne(tutor => tutor.Course)
               .HasForeignKey(tutor => tutor.CourseId);
            //Course One to many with Document
            builder.Entity<Course>()
               .HasMany(course => course.Documents)
               .WithOne(document => document.Course)
               .HasForeignKey(document => document.CourseId);


            //Document
            builder.Entity<Document>().ToTable("Documents");
            builder.Entity<Document>().HasKey(p => p.Id);
            builder.Entity<Document>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Document>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Entity<Document>().Property(p => p.Description).IsRequired().HasMaxLength(120);
            


            //Schedule
            builder.Entity<Schedule>().ToTable("Schedules");
            builder.Entity<Schedule>().HasKey(p => p.Id);
            builder.Entity<Schedule>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Schedule>().Property(p => p.StarDate).IsRequired();
            builder.Entity<Schedule>().Property(p => p.EndDate).IsRequired();
            builder.Entity<Schedule>().Property(p => p.Date).IsRequired();

           

            //Session
            builder.Entity<Session>().ToTable("Sessions");
            builder.Entity<Session>().HasKey(p => p.Id);
            builder.Entity<Session>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Session>().Property(p => p.Name).IsRequired().HasMaxLength(40);



            //User
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(40);
            builder.Entity<User>().Property(p => p.Email).IsRequired();
            builder.Entity<User>().Property(p => p.Password).IsRequired();
            builder.Entity<User>().Property(p => p.Accepted);

            //User One to One with tutor
            builder.Entity<User>()
                .HasOne(user => user.Tutor)
                .WithOne(tutor => tutor.User)
                .HasForeignKey<Tutor>(tutor => tutor.UserId);
            //User One to many with Document
            builder.Entity<User>()
               .HasMany(user => user.Documents)
               .WithOne(document => document.User)
               .HasForeignKey(document => document.UserId);
            //User One to many with Session
            builder.Entity<User>()
               .HasMany(user => user.Sessions)
               .WithOne(session => session.User)
               .HasForeignKey(session => session.UserId);

            //Tutor
            builder.Entity<Tutor>().ToTable("Tutors");
            builder.Entity<Tutor>().HasKey(p => p.Id);
            builder.Entity<Tutor>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Tutor>().Property(p => p.Description).IsRequired().HasMaxLength(120);
            builder.Entity<Tutor>().Property(p => p.PricePerHour).IsRequired();
            builder.Entity<Tutor>().Property(p => p.PricePerHour).IsRequired();

            //Tutor One to many with Schedule
            builder.Entity<Tutor>()
               .HasMany(tutor => tutor.Schedules)
               .WithOne(schedule => schedule.Tutor)
               .HasForeignKey(schedule => schedule.TutorId);

            //Tutor One to many with Session
            builder.Entity<Tutor>()
               .HasMany(tutor => tutor.Sessions)
               .WithOne(session => session.Tutor)
               .HasForeignKey(session => session.TutorId);

        }
    }
}
