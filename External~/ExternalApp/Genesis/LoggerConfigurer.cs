using Serilog;

namespace Genesis.CLI
{
	public static class LoggerConfigurer
	{
		public static void Configure(bool isVerbose, out Plugin.Logger logger)
		{
			var config = new LoggerConfiguration()
				.MinimumLevel.Verbose()
				.Enrich.WithProperty(
					LoggingConstants.VERSION_PROPERTY,
					VersionConstants.ASSEMBLY_SEM_VER);

			if (isVerbose)
			{
				config = config
					.MinimumLevel.Verbose()
					.WriteTo.Console(outputTemplate: LoggingConstants.VERBOSE_LOGGING_TEMPLATE);
			}
			else
			{
				config = config
					.MinimumLevel.Information()
					.WriteTo.Console(outputTemplate: LoggingConstants.GENERAL_LOGGING_TEMPLATE);
			}

			config = config.WriteTo.File(
				LoggingConstants.LOG_FILENAME,
				rollingInterval: RollingInterval.Month,
				rollOnFileSizeLimit: true,
				fileSizeLimitBytes: 10000000);

			Log.Logger = config.CreateLogger();

			logger = new Plugin.Logger(Log.Logger);
		}
	}
}
