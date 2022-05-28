<img src="https://img.shields.io/npm/v/com.laicasaane.genesis?label=openupm&amp;registry_uri=https://package.openupm.com" /></a>
<img alt="GitHub issues" src="https://img.shields.io/github/issues/laicasaane/Genesis?style=flat-square">[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

# Genesis

## About
Genesis is a general-purpose, plugin-extensible code generation framework for Unity.

## Overview

Genesis is architected as a .NET Core console application that leverages Roslyn code analysis to inspect a target C# codebase and generate code files where developers can build custom code generators via an extensible plugin framework. While Genesis v2 is largely engine-agnostic, it has a first-class integration into the Unity game engine.

## This fork

* The structure of Genesis repo has been changed to be compatible with UPM package format.
* Installing Genesis package will be much simpler, no external `Genesis.CLI` will be installed anymore.
* Genesis plugins can now be developed inside a Unity project or any UPM package.

### Notice

* This error message will appear on the Console window, but it is actually a **harmless error**.

```
Unloading broken assembly Packages/com.laicasaane.genesis/Plugins/Microsoft.CodeAnalysis.CSharp.dll, this assembly can cause crashes in the runtime
```

## Minimum Requirements
* Unity 2021.3.X
* **Scripting Runtime:** .NET Standard 2.1
* **Genesis.CLI Runtime:** .NET 6.0

## Installing Genesis
Using this library in your project can be done in two ways:

### Install via OpenUPM
The package is available on the [openupm registry](https://openupm.com/). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.laicasaane.genesis
```

### Install via GIT URL
Using the native Unity Package Manager introduced in 2017.2, you can add this library as a package by modifying your `manifest.json` file found at `/ProjectName/Packages/manifest.json` to include it as a dependency. See the example below on how to reference it.

```
{
	"dependencies": {
		...
		"com.laicasaane.genesis" : "https://github.com/laicasaane/genesis.git#upm",
		...
	}
}
```

You will need to have Git installed and available in your system's `PATH`.


## Usage

### General usage

To learn more about how to use JCMG Genesis, see the wiki [here](https://github.com/jeffcampbellmakesgames/Genesis/wiki/Usage) for more information.

### Genesis plugin as a UPM package

[Work in progress]


## Support the original author

If this is useful to you and/or you’d like to see future development and more tools in the future, please consider supporting [**Jeff Campbell**](https://github.com/jeffcampbellmakesgames).

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/I3I2W7GX)


## Contributing

For information on how to contribute and code style guidelines, please visit [here](CONTRIBUTING.md).