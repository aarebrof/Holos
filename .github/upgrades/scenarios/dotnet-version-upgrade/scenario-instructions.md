# Scenario Instructions

## Parameters
- **Target Framework**: net10.0 (.NET 10.0 LTS)
- **Source Branch**: fa/dotnet10
- **Working Branch**: upgrade-to-NET10-1
- **Scope**: H.Core, H.Content, H.Infrastructure only
- **Solution**: C:\AgExpert\repos\Holos\Holos.sln

## Preferences

### Flow Mode
**Automatic** — Run end-to-end, only pause when blocked.

### Technical Preferences
- (none yet)

### Execution Style
- (none yet)

### Custom Instructions
- (none yet)

## Strategy
**Selected**: All-At-Once
**Rationale**: 3 projects, all net48, all low difficulty, straightforward upgrade.

### Execution Constraints
- Single atomic upgrade — all projects updated together
- Validate full solution build after upgrade
- Convert to SDK-style first, then update TFM and packages
- Address API fixes after TFM change

### Commit Strategy
After Each Task

## Key Decisions Log
- All-At-Once strategy selected — low complexity, 3 projects, clear dependency structure
