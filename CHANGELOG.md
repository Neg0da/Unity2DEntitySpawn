# Changelog
All notable changes to this project will be documented in this file.

## [0.1.4.1] - 2025-01-19
### Fixed
- Fixed problems with private functions.

## [0.1.4] - 2025-01-12
### Changed
- Optimized the object pool management by pre-instantiating objects at the start, reducing the need for object instantiation during runtime.
- Refined object spawning logic to prevent duplicate objects being spawned within the `outerRadius` and ensure efficient repositioning.
- Enhanced velocity handling for objects, resetting velocity when `keepMomentum` is disabled.
- Refactored spawn position logic to be more flexible and adjusted the use of random values for size and rotation.
- Added improved checks for pool size, spawn radius validation, and critical errors at the start of the spawn process.

### Fixed
- Fixed object positioning bug where objects would not be repositioned correctly when they were within the outerRadius but not active.
- Corrected some minor issues with inconsistent random scaling and rotation behavior.

### Removed
- Removed redundant checks and logging statements to improve performance, especially in production builds.

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
