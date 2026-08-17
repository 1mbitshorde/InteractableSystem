# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [5.3.1] - 2026-08-17
### Fixed
- Check for interactor when enter AreaTrigger

## [5.3.0] - 2026-08-12
### Added
- AreaTrigger global OnAnyEntered/OnAnyExited events

## [5.2.1] - 2026-08-10
### Fixed
- Call OnAnyAvailabilityChanged when InteractableHandler is destroyed

## [5.2.0] - 2026-08-03
### Added
- Max collisions into AbstractInteractor

## [5.1.0] - 2026-08-03
### Changed
- Unseal AreaTrigger and AreaTriggerUnityEvent

## [5.0.0] - 2026-07-31
### Added
- AreaTriggerUnityEvent component
- AreaTrigger component
- Attributes package dependency
- CollisionableDetector component
- InteractableHandler.OnAnyAvailabilityChanged event

### Changed
- Simplify AbstractInteractor, removing multiple collisions detection

### Removed
- SimpleInteractor

## [4.1.0] - 2026-06-23
### Added
- CollisionableEvent component

## [4.0.0] - 2026-06-05
### Changed
- Add interactor param into IInteractable.Interact function
- Rename InteractableUnityEvent -> InteractableHandler

## [3.2.0] - 2026-06-02
### Added
- SimpleInteractor component

## [3.1.1] - 2026-03-31
### Fix
- Interactor exit collision checking

## [3.1.0] - 2026-03-26
### Added
- ICollisionable.gameObject field
- Interactor OnCollisionEntered event

## [3.0.0] - 2026-03-26
### Added
- ICollisionable.CanCollide function

### Changed
- Check if can collide before Interactor enter in collision with ICollecable instance
- Check Interactor collisons only when game is not paused

## [2.1.0] - 2026-03-21
### Changed
- Update package dependencies to use 1M Bits Horde

## [2.0.0] - 2026-03-10
### Added
- ICollisionable interface
- BoxInteractor component
- SphereInteractor component
- CollisionableUnityEvent component

### Changed
- Rename UnityEventInteractable into InteractableUnityEvent
- Refact IInteractable to implement ICollisionable

### Removed
- Interactor component. Use any AbstractInteractor implementation (BoxInteractor or SphereInteractor)

## [1.0.0] - 2025-10-30
### Added
- UnityEventInteractable component
- Interactor component
- IInteractable interface
- Shapes 1.2.0 package dependency
- CHANGELOG
- Package file
- README
- gitignore
- Initial commit

[Unreleased]: https://github.com/1mbitshorde/InteractableSystem/compare/5.3.1...main
[5.3.1]: https://github.com/1mbitshorde/InteractableSystem/tree/5.3.1/
[5.3.0]: https://github.com/1mbitshorde/InteractableSystem/tree/5.3.0/
[5.2.1]: https://github.com/1mbitshorde/InteractableSystem/tree/5.2.1/
[5.2.0]: https://github.com/1mbitshorde/InteractableSystem/tree/5.2.0/
[5.1.0]: https://github.com/1mbitshorde/InteractableSystem/tree/5.1.0/
[5.0.0]: https://github.com/1mbitshorde/InteractableSystem/tree/5.0.0/
[4.1.0]: https://github.com/1mbitshorde/InteractableSystem/tree/4.1.0/
[4.0.0]: https://github.com/1mbitshorde/InteractableSystem/tree/4.0.0/
[3.2.0]: https://github.com/1mbitshorde/InteractableSystem/tree/3.2.0/
[3.1.1]: https://github.com/1mbitshorde/InteractableSystem/tree/3.1.1/
[3.1.0]: https://github.com/1mbitshorde/InteractableSystem/tree/3.1.0/
[3.0.0]: https://github.com/1mbitshorde/InteractableSystem/tree/3.0.0/
[2.1.0]: https://github.com/1mbitshorde/InteractableSystem/tree/2.1.0/
[2.0.0]: https://github.com/1mbitshorde/InteractableSystem/tree/2.0.0/
[1.0.0]: https://github.com/1mbitshorde/InteractableSystem/tree/1.0.0/