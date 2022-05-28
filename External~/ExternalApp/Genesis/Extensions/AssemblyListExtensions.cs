using Newtonsoft.Json;
using System.IO;
using Genesis.Shared;

namespace Genesis.CLI
{
	/// <summary>
	/// Helper methods for <see cref="AssemblyList"/>.
	/// </summary>
	internal static class AssemblyListExtensions
	{
		/// <summary>
		/// Returns a <see cref="AssemblyList"/> instance from a base64-encoded Json <see cref="string"/>.
		/// </summary>
		public static AssemblyList LoadAssemblyListFromBase64(this string base64)
		{
			return base64.ConvertFromBase64().LoadAssemblyListFromJson();
		}

		/// <summary>
		/// Returns a <see cref="AssemblyList"/> instance from a Json <see cref="string"/>.
		/// </summary>
		public static AssemblyList LoadAssemblyListFromJson(this string jsonConfig)
		{
			return JsonConvert.DeserializeObject<AssemblyList>(jsonConfig);
		}

		/// <summary>
		/// Returns a <see cref="AssemblyList"/> instance from a Json file at <paramref name="filePath"/>.
		/// </summary>
		public static AssemblyList LoadAssemblyListFromFile(this string filePath)
		{
			var fileContents = File.ReadAllText(filePath);
			return fileContents.LoadAssemblyListFromJson();
		}

		/// <summary>
		/// Returns a Json string representing the serialized form of this config.
		/// </summary>
		public static string ConvertToJson(this AssemblyList assemblyList)
		{
			return JsonConvert.SerializeObject(assemblyList, Formatting.Indented);
		}
	}
}
