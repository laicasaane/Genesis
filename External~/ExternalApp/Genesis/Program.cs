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

using CommandLine;
using Genesis.Plugin;
using Genesis.Shared;
using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Genesis.CLI
{
	internal class Program
	{
		private static Logger _LOGGER;

		public static Task<int> Main(string[] args)
		{
			LoggerConfigurer.Configure(isVerbose: true, out _LOGGER);

			return Parser.Default.ParseArguments<GenerateOptions, ConfigOptions>(args: args)
				.MapResult(
					async (ConfigOptions configOptions) => await HandleConfigOptions(configOptions),
					async (GenerateOptions generateOptions) => await ExecuteCodeGeneration(generateOptions),
			notParsedFunc: HandleErrors);
		}

		private static async Task<int> HandleConfigOptions(ConfigOptions configOptions)
		{
			var result = 0;
			try
			{
				LoggerConfigurer.Configure(isVerbose: configOptions.IsVerbose, out _LOGGER);

				if (configOptions.DoCreate)
				{
					// Create config and populate it with all sub-configs
					var genesisConfig = new GenesisConfig();

					using (var assemblyLoader = new AssemblyLoader(configOptions.PluginPath))
					{
						// Load all assemblies that meet plugin constraints (unless forced)
						if (configOptions.DoLoadUnsafe)
						{
							assemblyLoader.ForceUnsafeAssemblyLoad();
						}

						assemblyLoader.AddPluginConstraint(Assembly.GetAssembly(typeof(ICodeGenerationPlugin)).GetName());
						assemblyLoader.AddPluginConstraint(Assembly.GetAssembly(typeof(IGenesisConfig)).GetName());

						if (configOptions.HasAssemblyPathsAsBase64() == false
							&& configOptions.HasAssemblyListFile() == false)
						{
							assemblyLoader.LoadAll(configOptions.PluginPath);
						}
						else
						{
							var assemblyList = new List<string>();

							if (configOptions.HasAssemblyPathsAsBase64())
							{
								_LOGGER.Verbose("Using Base64 Assembly List...");
								assemblyList.AddRange(configOptions.AssemblyPathsAsBase64.Select(x => x.ConvertFromBase64()));
							}

							if (configOptions.HasAssemblyListFile())
							{
								_LOGGER.Verbose("Using File Assembly List...");

								foreach (var assemblyFilePath in configOptions.AssemblyFilePaths)
								{
									if (File.Exists(assemblyFilePath))
									{
										var listFromFile = assemblyFilePath.LoadAssemblyListFromFile();
										assemblyList.AddRange(listFromFile.assemblies);
									}
									else
									{
										_LOGGER.Warning($"Could not find assembly list file at {assemblyFilePath}.");
									}
								}
							}

							assemblyLoader.LoadAll(assemblyList.Distinct());
						}

						_LOGGER.Verbose("Loaded {AssemblyCount} plugins assemblies.", assemblyLoader.Count);

						var codeGenerationPluginCache = new TypeCache();
						codeGenerationPluginCache.AddTypeWithInterface<object, ICodeGenerationPlugin>();

						foreach (var type in codeGenerationPluginCache)
						{
							_LOGGER.Verbose("Plugin loaded: {PluginName}", type.Name);
						}

						// Get all configs and populate with default settings
						var allConfigs = ReflectionTools.GetAllDerivedInstancesOfType<AbstractConfigurableConfig>();
						foreach (var config in allConfigs)
						{
							config.Configure(genesisConfig);
						}

						// Populate with all plugins and search paths
						var allPluginInstances = codeGenerationPluginCache
							.Select(Activator.CreateInstance)
							.Cast<ICodeGenerationPlugin>()
							.ToArray();

						var codeGeneratorConfig = new CodeGeneratorConfig();
						codeGeneratorConfig.Configure(genesisConfig);
						codeGeneratorConfig.AutoImportPlugins(allPluginInstances);

						_LOGGER.Verbose("Populated config with all defaults.");
					}

					var directory = Path.GetDirectoryName(configOptions.CreatePath);
					Directory.CreateDirectory(directory);

					// Write config to file.
					var fileContents = configOptions.UsePropertiesFile
						? genesisConfig.ConvertToPropertiesFile()
						: genesisConfig.ConvertToJson();

					var extension = configOptions.UsePropertiesFile
						? ".properties"
						: ".json";

					var filePath = configOptions.CreatePath;

					if (!filePath.EndsWith(extension))
					{
						filePath = Path.GetFullPath(filePath + extension);
					}

					await File.WriteAllTextAsync(filePath, fileContents);

					_LOGGER.Verbose("Config is written to {CreatePath}.", configOptions.CreatePath);
				}
			}
			catch (Exception ex)
			{
				_LOGGER.Error(ex, "An unexpected error occurred during code generation.");

				result = 1;
			}

			return result;
		}

		private static async Task<int> ExecuteCodeGeneration(GenerateOptions generateOptions)
		{
			var result = 0;
			try
			{
				LoggerConfigurer.Configure(isVerbose: generateOptions.IsVerbose, out _LOGGER);

				var resultTuple = await ParseSolution(generateOptions.SolutionPath);
				var solution = resultTuple.Item1;
				var allNamedTypeSymbols = resultTuple.Item2;

				// Load all plugin assemblies
				using (var assemblyLoader = new AssemblyLoader(generateOptions.PluginPath))
				{
					// Load all assemblies that meet plugin constraints (unless forced)
					if (generateOptions.DoLoadUnsafe)
					{
						assemblyLoader.ForceUnsafeAssemblyLoad();
					}
					assemblyLoader.AddPluginConstraint(Assembly.GetAssembly(typeof(ICodeGenerationPlugin)).GetName());
					assemblyLoader.AddPluginConstraint(Assembly.GetAssembly(typeof(IGenesisConfig)).GetName());

					if (generateOptions.HasAssemblyPathsAsBase64() == false
						&& generateOptions.HasAssemblyListFile() == false)
					{
						assemblyLoader.LoadAll(generateOptions.PluginPath);
					}
					else
					{
						var assemblyList = new List<string>();

						if (generateOptions.HasAssemblyPathsAsBase64())
						{
							_LOGGER.Verbose("Using Base64 Assembly List...");
							assemblyList.AddRange(generateOptions.AssemblyPathsAsBase64.Select(x => x.ConvertFromBase64()));
						}

						if (generateOptions.HasAssemblyListFile())
						{
							_LOGGER.Verbose("Using File Assembly List...");

							foreach (var assemblyFilePath in generateOptions.AssemblyFilePaths)
							{
								if (File.Exists(assemblyFilePath))
								{
									var listFromFile = assemblyFilePath.LoadAssemblyListFromFile();
									assemblyList.AddRange(listFromFile.assemblies);
								}
								else
								{
									_LOGGER.Warning($"Could not find assembly list file at {assemblyFilePath}.");
								}
							}
						}

						assemblyLoader.LoadAll(assemblyList.Distinct());
					}

					_LOGGER.Verbose("Loaded {PluginAssemblyCount} plugins assemblies.", assemblyLoader.Count);

					var codeGenerationPluginCache = new TypeCache();
					codeGenerationPluginCache.AddTypeWithInterface<object, ICodeGenerationPlugin>();

					foreach (var type in codeGenerationPluginCache)
					{
						_LOGGER.Verbose("Plugin loaded: {PluginName}.", type.Name);
					}

					// Add any configs from base64, file configs
					var configs = new List<IGenesisConfig>();
					if (generateOptions.HasConfigsAsBase64())
					{
						_LOGGER.Verbose("Using Base64 Configs...");

						foreach (var base64Config in generateOptions.ConfigsAsBase64)
						{
							configs.Add(base64Config.LoadGenesisConfigFromBase64());
						}
					}

					if (generateOptions.HasFileConfigs())
					{
						_LOGGER.Verbose("Using File Configs...");

						foreach (var configFilePath in generateOptions.ConfigFilePaths)
						{
							if (File.Exists(configFilePath))
							{
								_LOGGER.Verbose("Attempting to load file config from {ConfigFilePath}.", configFilePath);

								configs.Add(configFilePath.LoadGenesisConfigFromFile());
							}
							else
							{
								_LOGGER.Warning("Could not find config file at {ConfigFilePath}.", configFilePath);
							}
						}
					}

					_LOGGER.Verbose("Loaded {ConfigCount} GenesisConfigs. Starting code generation.", configs.Count);

					try
					{
						for (var i = 0; i < configs.Count; i++)
						{
							// Perform any per-config initialization
							PrepareConfig(generateOptions, configs[i]);

							// Create a memory cache per config code run and populate it with Roslyn info from solution
							var memoryCache = new MemoryCache();
							memoryCache.AddSolution(solution);
							memoryCache.AddNamedTypeSymbols(allNamedTypeSymbols);

							_LOGGER.Verbose("Generating code from {ConfigName}.", configs[i].Name);

							// Create a code-gen runner per config and execute.
							var codeGenerator = CodeGeneratorRunnerTools.CodeGeneratorFromPreferences(configs[i]);
							codeGenerator.OnProgress += (title, info, progress) =>
							{
								_LOGGER.Verbose("{Title} {Info} {Progress}%",
									title,
									info,
									$"{progress:P}");
							};

							if (generateOptions.IsDryRun)
							{
								codeGenerator.DryRun(memoryCache, _LOGGER);
							}

							codeGenerator.Generate(memoryCache, _LOGGER);
						}
					}
					catch (Exception ex)
					{
						_LOGGER.Error(ex, "An unexpected error occured during code generation, stopping now...");

						result = 1;
					}
				}
			}
			catch (Exception ex)
			{
				_LOGGER.Error(ex, "An unexpected error occurred during code generation.");

				result = 1;
			}

			return result;
		}

		/// <summary>
		/// Performs any preparations for each <see cref="IGenesisConfig"/> instance prior to code-generation.
		/// </summary>
		private static void PrepareConfig(GenerateOptions generateOptions, IGenesisConfig genesisConfig)
		{
			// Set the absolute project path per config
			var targetDirectoryConfig = genesisConfig.CreateAndConfigure<TargetDirectoryConfig>();
			var relativeOutputPath = targetDirectoryConfig.TargetDirectory;
			var absoluteOutputPath =
				Path.GetFullPath(Path.Combine(generateOptions.ProjectPath.Trim('"'), relativeOutputPath));
			targetDirectoryConfig.TargetDirectory = absoluteOutputPath;

			_LOGGER.Verbose("Setting absolute output path to {OutputPath}.", targetDirectoryConfig.TargetDirectory);
		}

		/// <summary>
		/// Attempts to parse the VS solution at <paramref name="solutionPath"/>; if present, the <see cref="Solution"/>
		/// and a read-only collection of <see cref="ICachedNamedTypeSymbol"/> instances.
		/// <param name="solutionPath">The absolute file path to the Visual Studio solution.</param>
		/// </summary>
		private static async Task<Tuple<Solution, IReadOnlyList<ICachedNamedTypeSymbol>>> ParseSolution(string solutionPath)
		{
			Solution solution = null;
			List<CachedNamedTypeSymbol> allNamedTypeSymbolInfo;

			// Create Roslyn workspace to parse solution, if present
			MSBuildLocator.RegisterDefaults();
			using (var workspace = MSBuildWorkspace.Create())
			{
				// Open the VS solution if present and find all types
				if (File.Exists(solutionPath))
				{
					_LOGGER.Verbose("Solution found at {SolutionPath}, attempting to load.", solutionPath);

					// Import solution for usage in code-generation
					solution = await workspace.OpenSolutionAsync(solutionPath);

					var allNamedTypeSymbols = await CodeAnalysisTools.FindAllTypes(solution);
					allNamedTypeSymbolInfo = allNamedTypeSymbols.Select(CachedNamedTypeSymbol.Create).ToList();

					_LOGGER.Verbose(
						"Solution loaded, {TypeSymbolsCount} TypeSymbols Discovered",
						allNamedTypeSymbols.Count);
				}
				else
				{
					_LOGGER.Verbose(
						"Skipping loading solution as none can be found at {SolutionPath}.",
						solutionPath);

					allNamedTypeSymbolInfo = new List<CachedNamedTypeSymbol>();
				}
			}

			return new Tuple<Solution, IReadOnlyList<ICachedNamedTypeSymbol>>(solution, allNamedTypeSymbolInfo);
		}

		#pragma warning disable CS1998
		private static async Task<int> HandleErrors(IEnumerable<Error> errors)
		{
			foreach (var error in errors)
			{
				switch (error)
				{
					// Do nothing if these are the errors
					case HelpRequestedError _:
					case VersionRequestedError _:
						// No-op
						break;

					// Otherwise log to the console.
					default:
						_LOGGER.Error(error.ToString());
						break;
				}
			}

			return 1;
		}
		#pragma warning restore CS1998
	}
}
