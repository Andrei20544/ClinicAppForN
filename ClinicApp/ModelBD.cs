using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClinicApp
{
    public partial class ModelBD : DbContext
    {
        public ModelBD()
            : base("name=ModelBD")
        {
        }

        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Doctor> Doctor { get; set; }
        public virtual DbSet<Medication_consumption> Medication_consumption { get; set; }
        public virtual DbSet<Pills> Pills { get; set; }
        public virtual DbSet<Register> Register { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Services> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.phone)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.email)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.type_policy)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.policy_number)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .Property(e => e.passport_data)
                .IsUnicode(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Register)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.surname)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.middle_name)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .Property(e => e.gender)
                .IsUnicode(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Register)
                .WithRequired(e => e.Doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doctor>()
                .HasMany(e => e.Schedule)
                .WithRequired(e => e.Doctor)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Pills>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Pills>()
                .Property(e => e.manufacturer)
                .IsUnicode(false);

            modelBuilder.Entity<Pills>()
                .Property(e => e.condition)
                .IsUnicode(false);

            modelBuilder.Entity<Pills>()
                .HasMany(e => e.Medication_consumption)
                .WithRequired(e => e.Pills)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.momday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.tuesday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.wednesday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.thursday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.friday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.saturday)
                .IsUnicode(false);

            modelBuilder.Entity<Schedule>()
                .Property(e => e.sunday)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.value)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.Medication_consumption)
                .WithRequired(e => e.Services)
                .HasForeignKey(e => e.id_services)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Services>()
                .HasMany(e => e.Register)
                .WithRequired(e => e.Services)
                .WillCascadeOnDelete(false);
        }
    }
}
