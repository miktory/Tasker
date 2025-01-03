using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace Tasker.Application.Common.Mappings
{
	public class AssemblyMappingProfile : Profile
	{
		public AssemblyMappingProfile(Assembly assembly) =>
			ApplyMappingsFromAssembly(assembly);

		private void ApplyMappingsFromAssembly(Assembly assembly)
		{

			var types = assembly.GetExportedTypes()
				.Where(type => type.GetInterfaces()
				.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");
				methodInfo?.Invoke(instance, new object[] { this });
			}

			types = assembly.GetExportedTypes()
			   .Where(t => t.BaseType == typeof(Profile) && t != typeof(AssemblyMappingProfile))
			   .ToList();

			// Создает экземпляр каждого найденного класса 
			foreach (var type in types)
			{
				var instance = (Profile)Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");
				methodInfo?.Invoke(instance, new object[] { this });

			}
		}
	}
}
