using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using H.Core.Calculators.Climate;
using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using H.Core.Providers.Climate;

namespace H.Core.Services.Initialization.Climate
{
    public class ClimateInitializationService : IClimateInitializationService
    {
        #region Fields

        private readonly NasaClimateProvider _nasaClimateProvider = new NasaClimateProvider();
        private readonly CustomFileClimateDataProvider _customFileClimateProvider = new CustomFileClimateDataProvider();
        private readonly ClimateNormalCalculator _climateNormalCalculator = new ClimateNormalCalculator();

        #endregion

        #region Constructors

        #endregion

        #region Public Methods

        public void InitializeClimate(Farm farm)
        {
            var acquisition = farm.ClimateAcquisition;
            List<DailyClimateData> dailyClimateData;

            if (acquisition == Farm.ChosenClimateAcquisition.InputFile)
            {
                dailyClimateData = _customFileClimateProvider.GetDailyClimateData(farm.ClimateDataFileName);
            }
            else
            {
                dailyClimateData = _nasaClimateProvider.GetCustomClimateData(farm.Latitude, farm.Longitude);
            }

            this.InitializeClimate(farm, dailyClimateData);
        }

        public void InitializeClimate(Farm farm, int startYear, int endYear)
        {
            var dailyClimateData = _nasaClimateProvider.GetCustomClimateData(farm.Latitude, farm.Longitude);
            var climateForPeriod = dailyClimateData.Where(x => x.Date.Year >= startYear && x.Date.Year <= endYear).ToList();

            farm.ClimateData.DailyClimateData.AddRange(climateForPeriod);

            this.SetClimateNormals(farm, climateForPeriod);
        }

        public void InitializeClimate(Farm farm, IEnumerable<DailyClimateData> dailyData)
        {
            var dailyDataList = dailyData.ToList();

            farm.ClimateData.DailyClimateData.AddRange(dailyDataList);

            this.SetClimateNormals(farm, dailyDataList);
        }

        public void SetClimateNormals(Farm farm, IEnumerable<DailyClimateData> climateForPeriod)
        {
            var climateList = climateForPeriod.ToList();

            var startYear = climateList.Min(x => x.Date.Year);
            var endYear = climateList.Max(x => x.Date.Year);

            var temperatureNormals = _climateNormalCalculator.GetTemperatureDataByDailyValues(climateList, startYear, endYear);
            var precipitationNormals = _climateNormalCalculator.GetPrecipitationDataByDailyValues(climateList, startYear, endYear);
            var evapotranspirationNormals = _climateNormalCalculator.GetEvapotranspirationDataByDailyValues(climateList, startYear, endYear);

            farm.ClimateData.EvapotranspirationData = evapotranspirationNormals;
            farm.ClimateData.PrecipitationData = precipitationNormals;
            farm.ClimateData.TemperatureData = temperatureNormals;
        }

        /// <summary>
        /// Initializes climate data for a specific field using its own coordinates. If the field has valid coordinates
        /// and <see cref="FieldSystemComponent.UseFieldLevelClimateData"/> is enabled, climate data will be fetched for
        /// the field location. Otherwise, farm-level climate data is used.
        /// </summary>
        public void InitializeFieldLevelClimate(Farm farm, FieldSystemComponent fieldSystemComponent)
        {
            if (fieldSystemComponent == null || fieldSystemComponent.UseFieldLevelClimateData == false)
            {
                return;
            }

            if (fieldSystemComponent.Latitude == 0 && fieldSystemComponent.Longitude == 0)
            {
                return;
            }

            // Skip NASA call if field coordinates match the farm coordinates
            if (fieldSystemComponent.Latitude == farm.Latitude && fieldSystemComponent.Longitude == farm.Longitude)
            {
                fieldSystemComponent.ClimateData = farm.ClimateData;
                return;
            }

            var dailyClimateData = _nasaClimateProvider.GetCustomClimateData(fieldSystemComponent.Latitude, fieldSystemComponent.Longitude);
            if (dailyClimateData.Any())
            {
                fieldSystemComponent.ClimateData = new ClimateData(dailyClimateData);

                var startYear = dailyClimateData.Min(x => x.Date.Year);
                var endYear = dailyClimateData.Max(x => x.Date.Year);

                fieldSystemComponent.ClimateData.TemperatureData = _climateNormalCalculator.GetTemperatureDataByDailyValues(dailyClimateData, startYear, endYear);
                fieldSystemComponent.ClimateData.PrecipitationData = _climateNormalCalculator.GetPrecipitationDataByDailyValues(dailyClimateData, startYear, endYear);
                fieldSystemComponent.ClimateData.EvapotranspirationData = _climateNormalCalculator.GetEvapotranspirationDataByDailyValues(dailyClimateData, startYear, endYear);
            }
        }

        #endregion

        #region Private Methods



        #endregion
    }
}