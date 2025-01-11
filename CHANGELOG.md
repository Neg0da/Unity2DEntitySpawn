# Changelog
All notable changes to this project will be documented in this file.

## [0.1.3.4] - 2025-01-11
### Added
- Added a configurable `spawnDuration` variable to control the maximum time (in seconds) for the spawning process.
- Modified `SpawnRoutine` to stop spawning objects after `spawnDuration` has elapsed.
- Added debug logs to indicate when spawning stops.

## [0.1.3.3] - 2025-01-09
### Changed
- README.md file now contains a table.

## [0.1.3.2] - 2025-01-09
### Changed
- Random rotation can now be enabled with random size set to 0, 0.

## [0.1.3.1] - 2025-01-09
### Added
- Added random rotation.

## [0.1.3] - 2025-01-04
### Added
- Added changelog.
### Changed
- Random size is now assigned during pool initialization, instead of each cycle loop.
- Logs now only work in Unity Editor and will not be included in the project build.
- Refactored code structure to follow a more step-by-step approach.
