﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Timelog.Model.Conventions;

namespace Timelog.Model
{
    public class TimeLogContext : DbContext
    {
        public TimeLogContext() : base()
        {
            Database.SetInitializer(new TimeLogContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.AddBefore<ForeignKeyIndexConvention>(new ForeignKeyNamingConvention());

            modelBuilder.Conventions.Add(new EnumRenamingConvention());

            modelBuilder.Properties<int>().Where(x => x.Name == "Id").Configure(x => x.IsKey().HasColumnOrder(1));
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TimeEntry> TimeEntries { get; set; }

        public DbSet<TagTree> TagTrees { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<BookingCode> BookingCodes { get; set; }

    }
}