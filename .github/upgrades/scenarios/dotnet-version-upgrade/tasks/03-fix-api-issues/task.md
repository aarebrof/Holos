# 03-fix-api-issues: Fix API compatibility issues and build errors

Address source-incompatible and binary-incompatible API changes:
- `TimeSpan.FromDays(double)` / `TimeSpan.FromSeconds(double)` — overload resolution changes (H.Core, H.Infrastructure)
- `System.Runtime.Caching.MemoryCache` — source incompatibility (H.Core)
- `System.Configuration.ApplicationSettingsBase` — source incompatibility (H.Core)
- Any additional build errors from the TFM change

**Done when**: Solution builds with zero errors for all three projects.
