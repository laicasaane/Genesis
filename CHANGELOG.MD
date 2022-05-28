# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## [Releases]
### [3.0.0] - 2022-05-28

#### Changed
* Completely restructured the project
    * Now conforms to UPM package format
    * `External~` contains `ExternalApp` and `Genesis.CLI` folders
* Upgraded to `Unity 2021.3`
* The `External~/Genesis.CLI/Genesis.CLI.dll` will be found and used automatically
    * Effectively deprecated this [installation instruction](https://github.com/jeffcampbellmakesgames/Genesis/wiki/Usage#installing-the-genesis-cli-and-configuring-genesis-project-settings=)
* Various changes to enable developing Genesis plugins inside a UPM package

#### Added
* Added `Auto Generation` option to `Genesis Settings`
* Added a post-processor to delete pre-generated files
* Added a Genesis `ILogger` interface so UPM packages won't need Serilog installed

## [Upstream Releases]
https://github.com/jeffcampbellmakesgames/Genesis/blob/develop/CHANGELOG.MD