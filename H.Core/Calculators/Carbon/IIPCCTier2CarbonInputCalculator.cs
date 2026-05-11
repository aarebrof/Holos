using H.Core.Emissions.Results;
using H.Core.Models;
using H.Core.Models.LandManagement.Fields;
using System.Collections.Generic;

namespace H.Core.Calculators.Carbon
{
    public interface IIPCCTier2CarbonInputCalculator : ICarbonInputCalculator
    {
        bool CanCalculateInputsForCrop(CropViewItem cropViewItem);
        void AssignInputs(CropViewItem viewItem, Farm farm, List<AnimalComponentEmissionsResults> animalResults);

        void AssignManureCarbonInputs(CropViewItem viewItem, Farm farm,
            List<AnimalComponentEmissionsResults> animalComponentEmissionsResults);
    }
}