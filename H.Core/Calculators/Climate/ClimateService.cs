using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using H.Core.Services.LandManagement;
using System;
using System.Diagnostics;
using System.Linq;

namespace H.Core.Calculators.Climate
{
    public class ClimateService : IClimateService
    {
        #region Fields

        private readonly IrrigationService _irrigationService;
        private readonly IClimateParameterCalculator _climateParameterCalculator;

        #endregion

        #region Constructors

        public ClimateService()
        {
            _irrigationService = new IrrigationService();
            _climateParameterCalculator = new ClimateParameterCalculator();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate climate parameter. Will use custom climate data if it exists for the farm, otherwise will use SLC normals
        /// for climate data. Uses field-level climate data when available.
        /// </summary>
        public double CalculateClimateParameter(CropViewItem viewItem, Farm farm)
        {
            var climateData = farm.GetPreferredClimateData(viewItem);

            if (climateData.DailyClimateData.Any())
            {
                var climateDataGroupedByYear = climateData.DailyClimateData.GroupBy(userClimateData => userClimateData.Year);
                var climateDataForYear = climateDataGroupedByYear.SingleOrDefault(groupingByYear => groupingByYear.Key == viewItem.Year);
                var climateParameter = 0d;

                if (climateDataForYear != null)
                {
                    // Use daily climate data
                    var precipitationList = climateDataForYear.OrderBy(cd => cd.JulianDay).Select(cd => cd.MeanDailyPrecipitation).ToList();

                    // Add irrigation amounts to daily precipitations
                    var totalPrecipitationList = _irrigationService.AddIrrigationToDailyPrecipitations(precipitationList, farm, viewItem);

                    var temperatureList = climateDataForYear.OrderBy(cd => cd.JulianDay).Select(cd => cd.MeanDailyAirTemperature).ToList();
                    var evapotranspirationList = climateDataForYear.OrderBy(cd => cd.JulianDay).Select(cd => cd.MeanDailyPET).ToList();
                    var minimumTemperatureList = climateDataForYear.OrderBy(cd => cd.JulianDay).Select(cd => cd.MinimumAirTemperature).ToList();
                    var maximumTemperatureList = climateDataForYear.OrderBy(cd => cd.JulianDay).Select(cd => cd.MaximumAirTemperature).ToList();

                    climateParameter = _climateParameterCalculator.CalculateClimateParameterForYear(
                        farm: farm,
                        cropViewItem: viewItem,
                        evapotranspirations: evapotranspirationList,
                        precipitations: totalPrecipitationList,
                        temperatures: temperatureList, 
                        dailyMinimumTemperatures: minimumTemperatureList, 
                        dailyMaximumTemperatures: maximumTemperatureList);
                }
                else
                {
                    // If user has entered custom climate data but their input file has no data for a particular year, then use normals for that particular year

                    // Add irrigation amounts to daily precipitations
                    var totalPrecipitationList = _irrigationService.AddIrrigationToDailyPrecipitations(climateData.PrecipitationData.GetAveragedYearlyValues(), farm, viewItem);

                    climateParameter = _climateParameterCalculator.CalculateClimateParameterForYear(
                        farm: farm,
                        cropViewItem: viewItem,
                        evapotranspirations: climateData.EvapotranspirationData.GetAveragedYearlyValues(),
                        precipitations: totalPrecipitationList,
                        temperatures: climateData.TemperatureData.GetAveragedYearlyValues(), 
                        dailyMinimumTemperatures: climateData.TemperatureData.GetAveragedYearlyValues(), 
                        dailyMaximumTemperatures: climateData.TemperatureData.GetAveragedYearlyValues());
                }

                return Math.Round(climateParameter, CoreConstants.DefaultNumberOfDecimalPlaces);
            }
            else
            {
                // Add irrigation amounts to daily precipitations
                var totalPrecipitationList = _irrigationService.AddIrrigationToDailyPrecipitations(climateData.PrecipitationData.GetAveragedYearlyValues(), farm, viewItem);

                // Use SLC normals when there is no custom user climate data
                Trace.TraceWarning($"{nameof(FieldResultsService)}: No custom daily climate data exists for this farm. Defaulting to SLC climate normals (and averaged daily values)");

                var result = _climateParameterCalculator.CalculateClimateParameterForYear(
                    farm: farm,
                    cropViewItem: viewItem,
                    evapotranspirations: climateData.EvapotranspirationData.GetAveragedYearlyValues(),
                    precipitations: totalPrecipitationList,
                    temperatures: climateData.TemperatureData.GetAveragedYearlyValues(), 
                    dailyMinimumTemperatures: climateData.TemperatureData.GetAveragedYearlyValues(), 
                    dailyMaximumTemperatures: climateData.TemperatureData.GetAveragedYearlyValues());

                return Math.Round(result, CoreConstants.DefaultNumberOfDecimalPlaces);
            }
        }

        #endregion
    }
}