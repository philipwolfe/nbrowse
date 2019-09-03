using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NBrowse.Reflection;

namespace NBrowse.CLI
{
	internal static class Help
	{
		public static void Write(TextWriter writer)
		{
			writer.WriteLine();
			writer.WriteLine("Entities available in queries are:");
			writer.WriteLine();

			var entities = new Queue<Type>();

			entities.Enqueue(typeof(IProject));

			var uniques = new HashSet<Type>(entities);

			while (entities.Count > 0)
			{
				var entity = entities.Dequeue();

				writer.WriteLine($"  {entity.Name}");

				if (entity.IsEnum)
					writer.WriteLine($"     {string.Join(" | ", Enum.GetNames(entity))}");
				else if (entity.IsClass || entity.IsValueType)
				{
					foreach (var property in entity.GetProperties(BindingFlags.Instance | BindingFlags.Public))
					{
						var propertyType = property.PropertyType;
						var targetType = Help.GetTargetType(property.PropertyType);

						writer.WriteLine($"    .{property.Name}: {Help.GetTypeName(propertyType)}");

						if (targetType.Namespace == entity.Namespace && uniques.Add(targetType))
							entities.Enqueue(targetType);
					}

					foreach (var method in entity.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public))
					{
						if (method.IsSpecialName)
							continue;

						writer.WriteLine($"    .{method.Name}({string.Join(", ", method.GetParameters().Select(p => $"{Help.GetTypeName(p.ParameterType)} {p.Name}"))}): {Help.GetTypeName(method.ReturnType)}");
					}
				}
			}
		}

		private static Type GetTargetType(Type type)
		{
			while (type.IsGenericType)
				type = type.GetGenericArguments()[0];

			return type;
		}

		private static string GetTypeName(Type type)
		{
			var typeName = type.Name;

			while (type.IsGenericType)
			{
				type = type.GetGenericArguments()[0];
				typeName = $"{typeName}<{type.Name}>";
			}

			return typeName;
		}
	}
}