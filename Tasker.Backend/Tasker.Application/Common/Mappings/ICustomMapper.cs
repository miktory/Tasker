using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasker.Application.Common.Mappings
{
	public interface ICustomMapper
	{
		TDestination Map<TSource, TDestination>(TSource source);
		Task<TDestination> MapAsync<TSource, TDestination>(TSource source);

		TDestination Map<TDestination>(object source);
	}
}
