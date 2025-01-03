using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Tasker.Domain;

namespace Notes.Persistence.EntityTypeConfigurations
{
    internal class ParametrizedTaskResultConfiguration : IEntityTypeConfiguration<ParametrizedTaskResult>
    {
        public void Configure(EntityTypeBuilder<ParametrizedTaskResult> builder)
        {
			builder.HasKey(tr => tr.Id);
			builder.Property(u => u.Id).ValueGeneratedOnAdd();
			builder.HasIndex(parametrizedTaskResult => parametrizedTaskResult.Id).IsUnique();
			builder.Property(parametrizedTaskResult => parametrizedTaskResult.Result).HasMaxLength(1000);
			builder.HasOne<ParametrizedTask>()
				.WithMany()
			.HasForeignKey(r => r.ParametrizedTaskId); 

		}
    }
}
