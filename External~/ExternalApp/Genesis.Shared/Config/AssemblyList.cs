using System;
using System.Collections.Generic;

namespace Genesis.Shared
{
	[Serializable]
	internal class AssemblyList
	{
		public List<string> assemblies;

		public AssemblyList()
		{
			assemblies = new List<string>();
		}
	}
}
