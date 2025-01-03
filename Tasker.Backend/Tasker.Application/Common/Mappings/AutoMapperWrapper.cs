using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace Tasker.Application.Common.Mappings
{

	public class AutoMapperWrapper : ICustomMapper
	{
		private readonly IMapper _mapper;

		public AutoMapperWrapper(IMapper mapper)
		{
			_mapper = mapper;
		}

		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return _mapper.Map<TDestination>(source);
		}

		public TDestination Map<TDestination>(object source)
		{
			return _mapper.Map<TDestination>(source);
		}

		public async Task<TDestination> MapAsync<TSource, TDestination>(TSource source)
		{
			return await Task.FromResult(_mapper.Map<TDestination>(source));
		}
	}
}
