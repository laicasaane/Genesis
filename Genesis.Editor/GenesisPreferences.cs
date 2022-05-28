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
using Genesis.Shared;
using UnityEditor;
using UnityEditor.SettingsManagement;
using UnityEngine;

namespace JCMG.Genesis.Editor
{
    /// <summary>
    /// An editor class for managing project and user preferences for the Genesis library.
    /// </summary>
    public static class GenesisPreferences
    {
        /// <summary>
        /// Returns true if the code generation should execute as a dry run, otherwise false.
        /// </summary>
        public static bool ExecuteDryRun
        {
            get
            {
                if (!_executeDryRun.HasValue)
                {
                    _executeDryRun = EditorPrefs.GetBool(ENABLE_DRY_RUN_PREF, ENABLE_DRY_RUN_DEFAULT);
                }

                return _executeDryRun.Value;
            }
            set
            {
                _executeDryRun = value;
                EditorPrefs.SetBool(ENABLE_DRY_RUN_PREF, value);
            }
        }

        /// <summary>
        /// Returns true if the code generation should be executed with verbose logging, otherwise false.
        /// </summary>
        public static bool EnableVerboseLogging
        {
            get
            {
                if (!_enableVerboseLogging.HasValue)
                {
                    _enableVerboseLogging = EditorPrefs.GetBool(ENABLE_VERBOSE_LOGGING_PREF, ENABLE_VERBOSE_LOGGING_DEFAULT);
                }

                return _enableVerboseLogging.Value;
            }
            set
            {
                _enableVerboseLogging = value;
                EditorPrefs.SetBool(ENABLE_VERBOSE_LOGGING_PREF, value);
            }
        }

        /// <summary>
        /// Returns true if the code generation should be executed with verbose logging, otherwise false.
        /// </summary>
        public static bool EnableAutoGeneration
        {
            get
            {
                if (!_enableAutoGen.HasValue)
                {
                    _enableAutoGen = EditorPrefs.GetBool(ENABLE_AUTO_GEN_PREF, ENABLE_AUTO_GEN_DEFAULT);
                }

                return _enableAutoGen.Value;
            }
            set
            {
                _enableAutoGen = value;
                EditorPrefs.SetBool(ENABLE_AUTO_GEN_PREF, value);
            }
        }

        /// <summary>
        /// Returns true if code generation should force loading of unsafe plugins (out of date plugins), otherwise
        /// false.
        /// </summary>
        public static bool LoadUnsafePlugins
        {
            get
            {
                if (!_loadUnsafePlugins.HasValue)
                {
                    _loadUnsafePlugins = ProjectSettings.Get<bool>(LOAD_UNSAFE_PLUGINS_PREF);
                }

                return _loadUnsafePlugins.Value;
            }
            set
            {
                _loadUnsafePlugins = value;
                ProjectSettings.Set(LOAD_UNSAFE_PLUGINS_PREF, value);
            }
        }

        /// <summary>
        /// Returns true if code generation should loading of assemblies in <see cref="System.AppDomain.CurrentDomain"/>,
        /// otherwise false.
        /// </summary>
        public static bool LoadCurrentDomainAssemblies
        {
            get
            {
                if (!_loadCurrentDomainAssemblies.HasValue)
                {
                    _loadCurrentDomainAssemblies = ProjectSettings.Get<bool>(LOAD_CURRENT_DOMAIN_ASSEMBLIES_PREF);
                }

                return _loadCurrentDomainAssemblies.Value;
            }
            set
            {
                _loadCurrentDomainAssemblies = value;
                ProjectSettings.Set(LOAD_CURRENT_DOMAIN_ASSEMBLIES_PREF, value);
            }
        }

        public static string DefaultOutputDirectory
        {
            get
            {
                if (_defaultOutputDirectory == null)
                {
                    _defaultOutputDirectory = ProjectSettings.Get(
                        DEFAULT_OUTPUT_DIRECTORY_PREF,
                        SettingsScope.Project,
                        string.Empty);
                }

                return _defaultOutputDirectory.ToSafeDirectory();
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    _defaultOutputDirectory = DEFAULT_OUTPUT_DIRECTORY;
                else
                    _defaultOutputDirectory = value;

                ProjectSettings.Set(DEFAULT_OUTPUT_DIRECTORY_PREF, _defaultOutputDirectory.ToSafeDirectory());
            }
        }

        /// <summary>
        /// The project <see cref="Settings"/> instance for Genesis for this project.
        /// </summary>
        private static Settings ProjectSettings
        {
            get
            {
                if (_PROJECT_SETTINGS == null)
                {
                    _PROJECT_SETTINGS = new Settings(PACKAGE_NAME, SETTINGS_NAME);
                }

                return _PROJECT_SETTINGS;
            }
        }

        private static Settings _PROJECT_SETTINGS;

        // Project Settings
        private const string PACKAGE_NAME = "com.jeffcampbellmakesgames.genesis";
        private const string SETTINGS_NAME = "GenesisSettings";

        // UI
        private const string PREFERENCES_TITLE_PATH = "Preferences/Genesis";
        private const string PROJECT_TITLE_PATH = "Project/Genesis";

        private const string USER_PREFERENCES_HEADER = "User Preferences";
        private const string PLUGIN_SETTINGS_HEADER = "Genesis Plugin Settings";

        // Labels
        private const string ENABLE_DRY_RUN_LABEL = "Enable Dry Run";
        private const string ENABLE_VERBOSE_LOGGING_LABEL = "Enable Verbose Logging";
        private const string ENABLE_AUTO_GEN_LABEL = "Enable Auto Generation";

        // Descriptions
        private const string ENABLE_DRY_RUN_DESCRIPTION
            = "If enabled, the code generation process when executed will not write any generated code to disk.";

        private const string ENABLE_VERBOSE_LOGGING_DESCRIPTION =
            "If enabled, additional information will be logged to the console.";

        private const string ENABLE_AUTO_GEN_DESCRIPTION =
            "If enabled, the code generation process will be executed when any code file is modified.";

        private const string DEFAULT_OUTPUT_DIRECTORY_SELECT_TITLE = "Select Output Folder";

        // Searchable Fields
        private static readonly string[] KEYWORDS =
        {
            "JCMG",
            "genesis",
            "code generation",
            "code gen",
            "code",
            "generation",
            "gen"
        };

        // Project Editor Preferences
        private const string GENESIS_INSTALLATION_HEADER = "Genesis Installation";
        private const string GENESIS_INSTALLATION_LOCATION = "../External~/Genesis.CLI";

        // User Editor Preferences
        private const string ENABLE_DRY_RUN_PREF = "Genesis.DryRun";
        private const string ENABLE_VERBOSE_LOGGING_PREF = "Genesis.IsVerbose";
        private const string ENABLE_AUTO_GEN_PREF = "Genesis.AutoGen";
        private const string LOAD_UNSAFE_PLUGINS_PREF = "Genesis.LoadUnsafePlugins";
        private const string LOAD_CURRENT_DOMAIN_ASSEMBLIES_PREF = "Genesis.LoadCurrentDomainAssemblies";
        private const string DEFAULT_OUTPUT_DIRECTORY_PREF = "Genesis.DefaultOutputDirectory";
        private const string DEFAULT_OUTPUT_DIRECTORY = "Assets/Generated";

        private const bool ENABLE_DRY_RUN_DEFAULT = false;
        private const bool ENABLE_VERBOSE_LOGGING_DEFAULT = true;
        private const bool ENABLE_AUTO_GEN_DEFAULT = false;

        // Cacheable Prefs
        private static bool? _executeDryRun;
        private static bool? _enableVerboseLogging;
        private static bool? _enableAutoGen;
        private static bool? _loadUnsafePlugins;
        private static bool? _loadCurrentDomainAssemblies;
        private static string _defaultOutputDirectory;

        #region SettingsProvider and EditorGUI

        [SettingsProvider]
        private static SettingsProvider CreatePersonalPreferenceSettingsProvider()
        {
            return new SettingsProvider(PREFERENCES_TITLE_PATH, SettingsScope.User)
            {
                guiHandler = DrawPersonalPrefsGUI,
                keywords = KEYWORDS
            };
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            return new SettingsProvider(PROJECT_TITLE_PATH, SettingsScope.Project)
            {
                guiHandler = DrawProjectPrefsGUI,
                keywords = KEYWORDS
            };
        }

        /// <summary>
        /// Opens the window for the Genesis Preferences.
        /// </summary>
        public static void OpenPreferences()
        {
            SettingsService.OpenUserPreferences(PREFERENCES_TITLE_PATH);
        }

        /// <summary>
        /// Opens the window for the Genesis Project Settings.
        /// </summary>
        public static void OpenProjectSettings()
        {
            SettingsService.OpenProjectSettings(PROJECT_TITLE_PATH);
        }

        private static void DrawPersonalPrefsGUI(string value = "")
        {
            EditorGUILayout.LabelField(USER_PREFERENCES_HEADER, EditorStyles.boldLabel);

            // Enable Dry Run
            EditorGUILayout.HelpBox(ENABLE_DRY_RUN_DESCRIPTION, MessageType.Info);
            using(var scope = new EditorGUI.ChangeCheckScope())
            {
                var newValue = EditorGUILayout.Toggle(ENABLE_DRY_RUN_LABEL, ExecuteDryRun);
                if(scope.changed)
                {
                    ExecuteDryRun = newValue;
                }
            }

            // Enable Verbose Logging
            EditorGUILayout.HelpBox(ENABLE_VERBOSE_LOGGING_DESCRIPTION, MessageType.Info);
            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                var newValue = EditorGUILayout.Toggle(ENABLE_VERBOSE_LOGGING_LABEL, EnableVerboseLogging);
                if (scope.changed)
                {
                    EnableVerboseLogging = newValue;
                }
            }

            // Enable Auto Generation
            EditorGUILayout.HelpBox(ENABLE_AUTO_GEN_DESCRIPTION, MessageType.Info);
            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                var newValue = EditorGUILayout.Toggle(ENABLE_AUTO_GEN_LABEL, EnableAutoGeneration);
                if (scope.changed)
                {
                    EnableAutoGeneration = newValue;
                }
            }
        }

        private static void DrawProjectPrefsGUI(string value = "")
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(GENESIS_INSTALLATION_HEADER, EditorStyles.boldLabel);
            EditorGUILayout.TextField(GetWorkingPath());

            // Plugins Section
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(PLUGIN_SETTINGS_HEADER, EditorStyles.boldLabel);

            const float PLUGINS_WIDTH = 240f;

            EditorGUILayout.HelpBox(EditorConstants.LOAD_UNSAFE_PLUGINS_DESCRIPTION, MessageType.Info);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(EditorConstants.LOAD_UNSAFE_PLUGINS_TOGGLE_TEXT, GUILayout.MaxWidth(PLUGINS_WIDTH));
                using (var scope = new EditorGUI.ChangeCheckScope())
                {
                    var newValue = EditorGUILayout.Toggle(LoadUnsafePlugins);
                    if (scope.changed)
                    {
                        LoadUnsafePlugins = newValue;
                        ProjectSettings.Save();
                    }
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(EditorConstants.LOAD_CURRENT_DOMAIN_ASSEMBLIES_DESCRIPTION, MessageType.Info);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(EditorConstants.LOAD_CURRENT_DOMAIN_ASSEMBLIES_TOGGLE_TEXT, GUILayout.MaxWidth(PLUGINS_WIDTH));
                using (var scope = new EditorGUI.ChangeCheckScope())
                {
                    var newValue = EditorGUILayout.Toggle(LoadCurrentDomainAssemblies);
                    if (scope.changed)
                    {
                        LoadCurrentDomainAssemblies = newValue;
                        ProjectSettings.Save();
                    }
                }
            }

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(EditorConstants.DEFAULT_OUTPUT_DIRECTORY_DESCRIPTION, MessageType.Info);
            using (new EditorGUILayout.HorizontalScope())
            {
                EditorGUILayout.LabelField(EditorConstants.DEFAULT_OUTPUT_DIRECTORY_TEXT, GUILayout.MaxWidth(PLUGINS_WIDTH));
                using (var scope = new EditorGUI.ChangeCheckScope())
                {
                    var newValue = EditorGUILayout.TextField(DefaultOutputDirectory);
                    if (scope.changed)
                    {
                        DefaultOutputDirectory = newValue;
                        ProjectSettings.Save();
                    }
                }

                var currentFolder = DefaultOutputDirectory;

                if (GUILayoutTools.DrawFolderPickerLayout(ref currentFolder, DEFAULT_OUTPUT_DIRECTORY_SELECT_TITLE))
                {
                    DefaultOutputDirectory = currentFolder;
                }
            }
        }

        #endregion

        #region Command-line

        /// <summary>
        /// Returns the absolute path to the <see cref="EditorConstants.GENESIS_EXECUTABLE"/> executable.
        /// </summary>
        public static string GetExecutablePath()
        {
            var path = GetWorkingPath();

            if (string.IsNullOrEmpty(path))
            {
                return string.Empty;
            }
            else
            {
                return Path.Combine(path, EditorConstants.GENESIS_EXECUTABLE);
            }
        }

        /// <summary>
        /// Returns the absolute path to the <see cref="EditorConstants.ASSEMBLY_LIST_FILE_PATH"/> executable.
        /// </summary>
        public static string GetAssemblyListFilePath()
        {
            var path = GetWorkingPath();

            if (string.IsNullOrEmpty(path))
            {
                return EditorConstants.ASSEMBLY_LIST_FILE_PATH;
            }
            else
            {
                return Path.Combine(path, EditorConstants.ASSEMBLY_LIST_FILE_PATH);
            }
        }

        /// <summary>
        /// Returns the absolute path template to the <see cref="EditorConstants.CONFIG_FILE_PATH_TEMPLATE"/> executable.
        /// </summary>
        public static string GetConfigFilePathTemplate()
        {
            var path = GetWorkingPath();

            if (string.IsNullOrEmpty(path))
            {
                return EditorConstants.CONFIG_FILE_PATH_TEMPLATE;
            }
            else
            {
                return Path.Combine(path, EditorConstants.CONFIG_FILE_PATH_TEMPLATE);
            }
        }

        /// <summary>
        /// Returns the absolute path to the working folder the the <see cref="EditorConstants.GENESIS_EXECUTABLE"/>.
        /// </summary>
        public static string GetWorkingPath()
        {
            return Path.Combine(Path.GetDirectoryName(typeof(GenesisConfig).Assembly.Location), GENESIS_INSTALLATION_LOCATION);
        }

        #endregion
    }
}
