
## [2026-05-11 13:46] 01-convert-sdk-style

Converted H.Infrastructure, H.Content, and H.Core to SDK-style format using topological order. Fixed AssemblyVersion wildcard errors in all 3 AssemblyInfo.cs files (4.0.* → 4.0.0.0). Fixed package version downgrades in H.Core (System.Configuration.ConfigurationManager 8.0.0→9.0.4, System.Runtime.CompilerServices.Unsafe 6.0.0→6.1.2). All 3 projects build successfully.

