// Copyright 2013-2015 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

#nullable enable

namespace Genesis.Shared.Logger
{
	/// <summary>
	/// The core logging API, used for interfacing with Serilog ILogger.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		
		void Write(LogEventLevel level, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, messageTemplate, NoPropertyValues);
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		
		void Write<T>(LogEventLevel level, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, messageTemplate, new object?[] { propertyValue });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		
		void Write<T0, T1>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, messageTemplate, new object?[] { propertyValue0, propertyValue1 });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		
		void Write<T0, T1, T2>(LogEventLevel level, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, messageTemplate, new object?[] { propertyValue0, propertyValue1, propertyValue2 });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="messageTemplate"></param>
		/// <param name="propertyValues"></param>
		
		void Write(LogEventLevel level, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(level, (Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		
		void Write(LogEventLevel level, Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, exception, messageTemplate, NoPropertyValues);
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		
		void Write<T>(LogEventLevel level, Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, exception, messageTemplate, new object?[] { propertyValue });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		
		void Write<T0, T1>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, exception, messageTemplate, new object?[] { propertyValue0, propertyValue1 });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		
		void Write<T0, T1, T2>(LogEventLevel level, Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
		{
			// Avoid the array allocation and any boxing allocations when the level isn't enabled
			if (IsEnabled(level))
			{
				Write(level, exception, messageTemplate, new object?[] { propertyValue0, propertyValue1, propertyValue2 });
			}
		}
#else
		;
#endif

		/// <summary>
		/// Write a log event with the specified level and associated exception.
		/// </summary>
		/// <param name="level">The level of the event.</param>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		
		void Write(LogEventLevel level, Exception? exception, string messageTemplate, params object?[]? propertyValues);

		/// <summary>
		/// Determine if events at the specified level will be passed through
		/// to the log sinks.
		/// </summary>
		/// <param name="level">Level to check.</param>
		/// <returns>True if the level is enabled; otherwise, false.</returns>
#if FEATURE_DEFAULT_INTERFACE
		[CustomDefaultMethodImplementation]
#endif
		bool IsEnabled(LogEventLevel level)
#if FEATURE_DEFAULT_INTERFACE
			=> true
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </code></example>
		
		void Verbose(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </code></example>
		
		void Verbose<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </code></example>
		
		void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </code></example>
		
		void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose("Staring into space, wondering if we're alone.");
		/// </code></example>
		
		void Verbose(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Verbose((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </code></example>
		
		void Verbose(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </code></example>
		
		void Verbose<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </code></example>
		
		void Verbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </code></example>
		
		void Verbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Verbose"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Verbose(ex, "Staring into space, wondering where this comet came from.");
		/// </code></example>
		
		void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Verbose, exception, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </code></example>
		
		void Debug(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </code></example>
		
		void Debug<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </code></example>
		
		void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </code></example>
		
		void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug("Starting up at {StartedAt}.", DateTime.Now);
		/// </code></example>
		
		void Debug(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Debug((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </code></example>
		
		void Debug(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </code></example>
		
		void Debug<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </code></example>
		
		void Debug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </code></example>
		
		void Debug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Debug"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Debug(ex, "Swallowing a mundane exception.");
		/// </code></example>
		
		void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Debug, exception, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information("Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Information((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Information"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Information(ex, "Processed {RecordCount} records in {TimeMS}.", records.Length, sw.ElapsedMilliseconds);
		/// </code></example>
		
		void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Information, exception, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning("Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Warning((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Warning"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Warning(ex, "Skipped {SkipCount} records.", skippedRecords.Length);
		/// </code></example>
		
		void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Warning, exception, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error("Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Error((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Error"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Error(ex, "Failed {ErrorCount} records.", brokenRecords.Length);
		/// </code></example>
		
		void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Error, exception, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Fatal("Process terminating.");
		/// </code></example>
		
		void Fatal(string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal("Process terminating.");
		/// </code></example>
		
		void Fatal<T>(string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal("Process terminating.");
		/// </code></example>
		
		void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal("Process terminating.");
		/// </code></example>
		
		void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal("Process terminating.");
		/// </code></example>
		
		void Fatal(string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Fatal((Exception?)null, messageTemplate, propertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <example><code>
		/// Log.Fatal(ex, "Process terminating.");
		/// </code></example>
		
		void Fatal(Exception? exception, string messageTemplate)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, exception, messageTemplate, NoPropertyValues)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal(ex, "Process terminating.");
		/// </code></example>
		
		void Fatal<T>(Exception? exception, string messageTemplate, T propertyValue)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal(ex, "Process terminating.");
		/// </code></example>
		
		void Fatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValue0">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue1">Object positionally formatted into the message template.</param>
		/// <param name="propertyValue2">Object positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal(ex, "Process terminating.");
		/// </code></example>
		
		void Fatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2)
#endif
			;

		/// <summary>
		/// Write a log event with the <see cref="LogEventLevel.Fatal"/> level and associated exception.
		/// </summary>
		/// <param name="exception">Exception related to the event.</param>
		/// <param name="messageTemplate">Message template describing the event.</param>
		/// <param name="propertyValues">Objects positionally formatted into the message template.</param>
		/// <example><code>
		/// Log.Fatal(ex, "Process terminating.");
		/// </code></example>
		
		void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues)
#if FEATURE_DEFAULT_INTERFACE
			=> Write(LogEventLevel.Fatal, exception, messageTemplate, propertyValues)
#endif
			;
	}
}
