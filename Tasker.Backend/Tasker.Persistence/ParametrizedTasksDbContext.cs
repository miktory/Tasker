using System;
using Tasker.Application;
using Microsoft.EntityFrameworkCore;
using Tasker.Application.Interfaces;
using Tasker.Domain;
using Notes.Persistence.EntityTypeConfigurations;

namespace Tasker.Persistence
{
	public class ParametrizedTasksDbContext : DbContext,  IParametrizedTasksDbContext
	{
		public ParametrizedTasksDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ParametrizedTask> ParametrizedTasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParametrizedTaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
