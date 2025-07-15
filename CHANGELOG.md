# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

-   Rimworld 1.6 support.

### Changed

-   Grouped jobs by mod/DLC in the job list in the settings.

## [0.4.2] - 2024-10-04

### Fixed

-   Actually load Achtung integration build artifacts in release.

## [0.4.1] - 2024-10-04

### Fixed

-   Actually include Achtung integration build artifacts in release.

## [0.4.0] - 2024-06-24

### Added

-   Added hotkey to tactics mode gizmo. The hotkey is configurable under the keyboard bindings settings menu.

### Changed

-   Made the tactics mode gizmo always come immediately after the draft/undraft gizmo for consistency and to have high priority for its hotkey.

## [0.3.0] - 2024-06-19

### Added

-   Added not pausing when a colonist is performing Achtung!'s forced work. This behavior can be turned off in the settings.

### Fixed

-   `JobDef.modContentPack` could be null, which would cause the job list in the settings to be empty

## [0.2.1] - 2024-06-18

### Fixed

-   Added error handling to job report gathering so it doesn't crash the process if it goes wrong.

## [0.2.0] - 2024-06-18

### Added

-   Added a setting for turning off the pan-and-select of a colonist on tactical pause.
-   Added showing a message when a colonist goes on a tactical pause. This can be turned off in the settings.

## [0.1.0] - 2024-06-17

### Added

-   Added settings for which jobs to always pause after, as well as how long of a time to wait for after any job before pausing.

[Unreleased]: https://github.com/ilyvion/TacticsModeRedux/compare/v0.4.2...HEAD
[0.4.2]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.4.1...v0.4.2
[0.4.1]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.4.0...v0.4.1
[0.4.0]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.3.0...v0.4.0
[0.3.0]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.2.1...v0.3.0
[0.2.1]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.2.0...v0.2.1
[0.2.0]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.1.0...v0.2.0
[0.1.0]: https://github.com/ilyvion/TacticsModeRedux/releases/tag/v0.1.0
