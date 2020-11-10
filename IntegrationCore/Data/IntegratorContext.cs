using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IntegrationCore.Models.DB;

namespace IntegrationCore.Data
{
    public class IntegratorContext : DbContext
    {
        public DbSet<SystemDefinition> SystemDefinition { get; set; }
        public DbSet<ConnectionFieldDefinition> ConnectionFieldDefinition { get; set; }
        public DbSet<ConnectionFieldValue> ConnectionFieldValue { get; set; }
        public DbSet<FieldConnection> FieldConnection { get; set; }
        public DbSet<FieldDefinition> FieldDefinition { get; set; }
        public DbSet<Integration> Integration { get; set; }
        public DbSet<TypeDefinition> TypeDefinition { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        public IntegratorContext(DbContextOptions<IntegratorContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SystemDefinition>()
                .HasMany(el=>el.ConnectionFields)
                .WithOne(el=>el.SystemDefinition)
                .HasForeignKey(el=>el.SystemId);

            modelBuilder.Entity<SystemDefinition>()
                .HasMany(el => el.TypeDefinitions)
                .WithOne(el => el.SystemDefinition)
                .HasForeignKey(el => el.SystemId);

            modelBuilder.Entity<TypeDefinition>()
                .HasMany(el => el.Fields)
                .WithOne(el => el.Type)
                .HasForeignKey(el => el.TypeId);

            modelBuilder.Entity<ConnectionFieldValue>()
                .HasOne(el => el.Integration)
                .WithMany(el => el.ConnectionFieldValues)
                .HasForeignKey(el => el.IntegrationId);

            modelBuilder.Entity<ConnectionFieldValue>()
                .HasOne(el => el.ConnectionField)
                .WithMany(el => el.ConnectionFieldValues)
                .HasForeignKey(el => el.ConnectionFieldId);

            modelBuilder.Entity<FieldConnection>()
                .HasOne(el => el.FirstField)
                .WithOne()
                .HasForeignKey<FieldConnection>(el => el.FirstFieldId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<FieldConnection>().HasIndex(el => el.FirstFieldId).IsUnique(false);
            modelBuilder.Entity<FieldConnection>().HasIndex(el => el.SecondFieldId).IsUnique(false);

            modelBuilder.Entity<FieldConnection>()
                .HasOne(el => el.SecondField)
                .WithOne()
                .HasForeignKey<FieldConnection>(el => el.SecondFieldId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Integration>()
                .HasOne(el => el.TypeFrom)
                .WithMany()
                .HasForeignKey(el => el.TypeFromId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Integration>()
                .HasOne(el => el.TypeTo)
                .WithMany()
                .HasForeignKey(el => el.TypeToId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Integration>()
                .HasMany(el => el.FieldConnections)
                .WithOne(el=>el.Integration)
                .HasForeignKey(el => el.IntegrationId);

            modelBuilder.Entity<Transaction>()
                .HasOne(el => el.Integration)
                .WithMany()
                .HasForeignKey(el => el.IntegrationId);
        }
    }
}
