
## [2026-05-11 13:46] 01-convert-sdk-style

Converted H.Infrastructure, H.Content, and H.Core to SDK-style format using topological order. Fixed AssemblyVersion wildcard errors in all 3 AssemblyInfo.cs files (4.0.* → 4.0.0.0). Fixed package version downgrades in H.Core (System.Configuration.ConfigurationManager 8.0.0→9.0.4, System.Runtime.CompilerServices.Unsafe 6.0.0→6.1.2). All 3 projects build successfully.


## [2026-05-11 13:50] 02-update-tfm-and-packages

Updated all 3 projects to net10.0-windows (H.Content initially set to net10.0 but changed to net10.0-windows for compatibility with H.Infrastructure). Updated AutoMapper 9.0.0→16.1.1 (security fix), Newtonsoft.Json 13.0.3→13.0.4, System.Configuration.ConfigurationManager→10.0.7, System.Runtime.Caching→10.0.7. Removed NETStandard.Library, System.Memory, and System.Runtime.CompilerServices.Unsafe (included in framework). Restore succeeds.


## [2026-05-11 13:58] 03-fix-api-issues

Fixed all build errors caused by AutoMapper 16.x breaking change — MapperConfiguration constructor now requires ILoggerFactory parameter. Created MapperConfigurationFactory helper class that passes null for the logger. Updated 18 files replacing `new MapperConfiguration(...)` with `MapperConfigurationFactory.Create(...)`. All 3 projects (H.Infrastructure, H.Content, H.Core) build with zero errors.

