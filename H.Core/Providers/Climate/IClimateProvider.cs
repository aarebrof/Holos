using H.Core.Enumerations;
using H.Core.Models;

namespace H.Core.Providers.Climate
{
    public interface IClimateProvider
    {
        void OutputDailyClimateData(Farm farm, string outputPath);
        ClimateData Get(double latitude, double longitude, TimeFrame climateNormalTimeFrame, Farm farm);
        ClimateData Get(string filepath, TimeFrame normalCalculationTimeFrame, Farm farm);
        ClimateData GetClimateData(int polygonId, TimeFrame timeFrame);
    }
}