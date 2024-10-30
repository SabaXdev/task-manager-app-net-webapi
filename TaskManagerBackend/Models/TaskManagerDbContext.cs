using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TaskManagerBackend.Models
{
    public class TaskManagerDbContext : DbContext
    {
        private readonly DbSettings _dbSettings;

        public TaskManagerDbContext(IOptions<DbSettings> dbSettings)
        {
            this._dbSettings = dbSettings.Value;
        }

        public DbSet<MyTask> TaskManagers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_dbSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>()
            .ToTable("MyTasksNew")
            .HasKey(x => x.Id);
        }

    }
}
