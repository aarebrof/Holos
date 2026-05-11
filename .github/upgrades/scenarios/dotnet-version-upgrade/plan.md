# .NET Version Upgrade Plan

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 3 in-scope projects (H.Core, H.Content, H.Infrastructure), all on net48, low difficulty ratings, straightforward upgrade.

## Overview

**Target**: Upgrade H.Core, H.Content, and H.Infrastructure from .NET Framework 4.8 to .NET 10.0
**Scope**: 3 projects, ~173k total LOC in solution. All require SDK-style conversion, TFM change, and package updates. H.Core has the most work (security vuln in AutoMapper, 4 package upgrades, 16 API issues).

## Tasks

### 01-convert-sdk-style: Convert projects to SDK-style format

All three projects (H.Infrastructure, H.Content, H.Core) use classic .csproj format and must be converted to SDK-style before the TFM can be changed. Convert all three simultaneously.

**Done when**: All three .csproj files are SDK-style format and the solution builds successfully.

---

### 02-update-tfm-and-packages: Update target frameworks and NuGet packages

Change TargetFramework to net10.0 for all three projects. Update packages:
- AutoMapper 9.0.0 → 16.1.1 (security vulnerability)
- Newtonsoft.Json 13.0.3 → 13.0.4
- System.Configuration.ConfigurationManager 8.0.0 → 10.0.7
- System.Runtime.Caching 9.0.4 → 10.0.7
- Remove NETStandard.Library and System.Memory (included in framework)

**Done when**: All three projects target net10.0, all packages updated, `dotnet restore` succeeds.

---

### 03-fix-api-issues: Fix API compatibility issues and build errors

Address source-incompatible and binary-incompatible API changes:
- `TimeSpan.FromDays(double)` / `TimeSpan.FromSeconds(double)` — overload resolution changes (H.Core, H.Infrastructure)
- `System.Runtime.Caching.MemoryCache` — source incompatibility (H.Core)
- `System.Configuration.ApplicationSettingsBase` — source incompatibility (H.Core)
- Any additional build errors from the TFM change

**Done when**: Solution builds with zero errors for all three projects.

---

### 04-validate-tests: Run and fix tests

Run all tests for H.Core.Test and H.Infrastructure.Test to verify the upgrade didn't break functionality. Fix any test failures.

**Done when**: All tests in H.Core.Test and H.Infrastructure.Test pass.
