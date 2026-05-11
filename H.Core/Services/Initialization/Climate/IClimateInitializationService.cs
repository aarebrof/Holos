using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using H.Core.Providers.Climate;
using System.Collections.Generic;

namespace H.Core.Services.Initialization.Climate
{
    public interface IClimateInitializationService
    {
        void InitializeClimate(Farm farm);
        void InitializeClimate(Farm farm, int startYear, int endYear);
        void InitializeClimate(Farm farm, IEnumerable<DailyClimateData> dailyClimateData);
        void SetClimateNormals(Farm farm, IEnumerable<DailyClimateData> climateForPeriod);
        void InitializeFieldLevelClimate(FieldSystemComponent fieldSystemComponent, List<DailyClimateData> climateData);
    }
}