using H.Core.Enumerations;
using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using System;

namespace H.Core.Providers.Climate
{
    public interface IClimateProvider
    {
        void OutputDailyClimateData(Farm farm, string outputPath);
        double GetMeanTemperatureForDay(Farm farm, CropViewItem viewItem, DateTime dateTime);
        double GetMeanPrecipitationForDay(Farm farm, DateTime dateTime);
        double GetAnnualEvapotranspiration(Farm farm, DateTime dateTime);
        double GetAnnualPrecipitation(Farm farm, DateTime dateTime);
        double GetAnnualPrecipitation(Farm farm, CropViewItem viewItem);
        double GetGrowingSeasonPrecipitation(Farm farm, DateTime dateTime);
        double GetGrowingSeasonEvapotranspiration(Farm farm, DateTime dateTime);
        double GetAnnualPrecipitation(Farm farm, int year);
        double GetAnnualEvapotranspiration(Farm farm, int year);
        double GetAnnualEvapotranspiration(Farm farm, CropViewItem viewItem);
        double GetGrowingSeasonPrecipitation(Farm farm, int year);
        double GetGrowingSeasonPrecipitation(Farm farm, CropViewItem viewItem);
        double GetGrowingSeasonEvapotranspiration(Farm farm, int year);
        double GetGrowingSeasonEvapotranspiration(Farm farm, CropViewItem viewItem);
        ClimateData Get(double latitude, double longitude, TimeFrame climateNormalTimeFrame, Farm farm);
        ClimateData Get(string filepath, TimeFrame normalCalculationTimeFrame, Farm farm);
        ClimateData GetClimateData(int polygonId, TimeFrame timeFrame);
    }
}