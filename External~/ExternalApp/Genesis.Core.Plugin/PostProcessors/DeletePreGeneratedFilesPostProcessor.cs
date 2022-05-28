/*

MIT License

Copyright (c) Jeff Campbell

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/

using System.IO;
using Genesis.Plugin;
using Genesis.Shared;
using Serilog;

namespace Genesis.Core.Plugin
{
	/// <summary>
	/// An <see cref="IPostProcessor"/> for deleting only the old version of generated files from the output folder.
	/// </summary>
	internal sealed class DeletePreGeneratedFilesPostProcessor : IPostProcessor,
															     IConfigurable
	{
		public string Name => NAME;

		public int Priority => 93;

		public bool RunInDryMode => false;

		private readonly ILogger _logger = Log.ForContext<DeletePreGeneratedFilesPostProcessor>();
		private readonly TargetDirectoryConfig _targetDirectoryConfig = new TargetDirectoryConfig();

		private const string NAME = "Delete Pre-generated Files";

		public void Configure(IGenesisConfig genesisConfig)
		{
			_targetDirectoryConfig.Configure(genesisConfig);
		}

		public CodeGenFile[] PostProcess(CodeGenFile[] files)
		{
			DeleteFiles(files);

			return files;
		}

		private void DeleteFiles(CodeGenFile[] files)
		{
			var targetDirectory = _targetDirectoryConfig.TargetDirectory;

			if (Directory.Exists(targetDirectory) == false)
			{
				Directory.CreateDirectory(_targetDirectoryConfig.TargetDirectory);
				return;
			}

			foreach (var file in files)
			{
				if (file == null)
					continue;

				var filePath = Path.Combine(targetDirectory, file.FileName);

				if (File.Exists(filePath) == false)
					continue;

				try
				{
					File.Delete(filePath);
				}
				catch
				{
					_logger.Error($"Could not delete file {filePath}");
				}
			}
		}
	}
}
