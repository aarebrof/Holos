# 02-update-tfm-and-packages: Update target frameworks and NuGet packages

Change TargetFramework to net10.0 for all three projects. Update packages:
- AutoMapper 9.0.0 → 16.1.1 (security vulnerability)
- Newtonsoft.Json 13.0.3 → 13.0.4
- System.Configuration.ConfigurationManager 8.0.0 → 10.0.7
- System.Runtime.Caching 9.0.4 → 10.0.7
- Remove NETStandard.Library and System.Memory (included in framework)

**Done when**: All three projects target net10.0, all packages updated, `dotnet restore` succeeds.
