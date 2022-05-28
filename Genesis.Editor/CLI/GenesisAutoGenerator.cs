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

using System.Collections.Generic;
using Genesis.Shared;
using UnityEditor;
using UnityEditor.Compilation;

namespace JCMG.Genesis.Editor
{
    [InitializeOnLoad]
    internal static class GenesisAutoGenerator
    {
        private const string AUTO_GEN_COUNT = nameof(AUTO_GEN_COUNT);

        private static readonly AutoGenerationConfig AutoGenConfig;

        static GenesisAutoGenerator()
        {
            AutoGenConfig = new AutoGenerationConfig();
            CompilationPipeline.compilationStarted += TryRunAutoGeneration;
        }

        private static void TryRunAutoGeneration(object obj)
        {
            // Cannot run more than once per session
            {
                var count = SessionState.GetInt(AUTO_GEN_COUNT, 0);

                if (count > 0)
                {
                    SessionState.SetInt(AUTO_GEN_COUNT, 0);
                    return;
                }

                SessionState.SetInt(AUTO_GEN_COUNT, 1);
            }

            RunCodeGeneration(GenesisPreferences.EnableAutoGeneration);
        }

        private static void RunCodeGeneration(bool autoGenAll)
        {
            var settings = GenesisSettings.GetAllSettings();

            if (autoGenAll)
            {
                GenesisCLIRunner.RunCodeGeneration(settings, false);
            }
            else
            {
                GenesisCLIRunner.RunCodeGeneration(GetAutoGenSettings(settings), false);
            }
        }

        private static IEnumerable<GenesisSettings> GetAutoGenSettings(GenesisSettings[] collection)
        {
            for (var i = 0; i < collection.Length; i++)
            {
                var settings = collection[i];
                AutoGenConfig.Configure(settings);

                if (AutoGenConfig.AutoGeneration)
                    yield return settings;
            }
        }
    }
}
