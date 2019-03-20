using Common.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Now.Entities.Models.Schedule;

namespace Now.Data.DataContexts.Schedule
{
    public class ScheduleDbContext : DbContext
    {
        //private static readonly bool[] _migrated = { false };

        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
            : base(options)
        {
            //if (!_migrated[0])
            //    lock (_migrated)
            //        if (!_migrated[0])
            //        {
            //            Database.Migrate();
            //            _migrated[0] = true;
            //        }
        }

        public DbSet<Entities.Models.Schedule.Schedule> Schedules { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Schedule");

            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Entities.Models.Schedule.Schedule>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<Shift>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<ShiftDetail>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<ShiftDetailType>()
                .Ignore(e => e.EntityId);


            base.OnModelCreating(modelBuilder);
        }

        public class ConfigurationContextFactory : IDesignTimeDbContextFactory<ScheduleDbContext>
        {
            public ScheduleDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ScheduleDbContext>();
                optionsBuilder.UseSqlServer(
                "Server=.\\;Database=Now-Test;Trusted_Connection=True;MultipleActiveResultSets=True"
                );

                return new ScheduleDbContext(optionsBuilder.Options);
            }
        }

    }

}
