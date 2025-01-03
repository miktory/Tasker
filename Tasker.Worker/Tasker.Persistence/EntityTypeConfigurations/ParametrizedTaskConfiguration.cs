using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Notes.Persistence.EntityTypeConfigurations
{
    internal class ParametrizedTaskConfiguration : IEntityTypeConfiguration<ParametrizedTask>
    {
        public void Configure(EntityTypeBuilder<ParametrizedTask> builder)
        {
            builder.HasKey(parametrizedTask => parametrizedTask.Id);
            builder.HasIndex(parametrizedTask => parametrizedTask.Id).IsUnique();
            builder.Property(parametrizedTask => parametrizedTask.Parameters).HasMaxLength(1000);

        }
    }
}
