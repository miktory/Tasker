using System;
using Tasker.Application;
using Microsoft.EntityFrameworkCore;
using Tasker.Application.Interfaces;
using Tasker.Domain;
using Notes.Persistence.EntityTypeConfigurations;

namespace Tasker.Persistence
{
	public class ApplicationDbContext : DbContext,  IApplicationDbContext
	{
		public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ParametrizedTask> ParametrizedTasks { get; set; }
		public DbSet<ParametrizedTaskResult> ParametrizedTasksResults { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParametrizedTaskConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
