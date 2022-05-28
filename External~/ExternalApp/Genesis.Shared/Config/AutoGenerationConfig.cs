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

namespace Genesis.Shared
{
	/// <summary>
	/// A configuration definition for automatic generation.
	/// </summary>
	public class AutoGenerationConfig : AbstractConfigurableConfig
	{
		public bool AutoGeneration
		{
			get
			{
				var value = _genesisConfig.GetOrSetValue(AUTO_GENERATION_KEY, DEFAULT_VALUE).ToLower();

				// If for some reason we can't parse this bool, default to false for automatic generation.
				if (!bool.TryParse(value, out var result))
				{
					result = false;
				}

				return result;
			}
			set => _genesisConfig.SetValue(AUTO_GENERATION_KEY, value.ToString().ToLower());
		}

		private const string AUTO_GENERATION_KEY = "Genesis.AutoGeneration";
		private const string DEFAULT_VALUE = "false";

		/// <summary>
		/// Attempts to set defaults for the <paramref name="genesisConfig"/> and initializes any local state.
		/// </summary>
		public override void Configure(IGenesisConfig genesisConfig)
		{
			base.Configure(genesisConfig);

			genesisConfig.SetIfNotPresent(AUTO_GENERATION_KEY, DEFAULT_VALUE);
		}
	}
}
