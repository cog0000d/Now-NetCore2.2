using Common.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Now.Entities.Models.Schedule;
using Now.Entities.Models.Time;
using Type = Now.Entities.Models.Time.Type;

namespace Now.Data.DataContexts
{
    public class NowDbContext : DbContext
    {
        private static readonly bool[] _migrated = {false};

        public NowDbContext(DbContextOptions<NowDbContext> options)
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

        public DbSet<Log> Logs { get; set; }
        public DbSet<LogDetail> LogDetails { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Type> Types { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Time");

            modelBuilder.Ignore<IIdentifiableEntity>();

            modelBuilder.Entity<Log>()
                .Ignore(e => e.EntityId);


            modelBuilder.Entity<LogDetail>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<Source>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<Type>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<Shift>()
                .Ignore(e => e.EntityId);

            modelBuilder.Entity<ShiftDetail>()
                .Ignore(e => e.EntityId);

            base.OnModelCreating(modelBuilder);
        }
    }
}