using H.Core.Emissions.Results;
using H.Core.Models;
using System.Collections.Generic;

namespace H.Core.Calculators.Infrastructure
{
    public interface IADCalculator
    {
        List<DigestorDailyOutput> CalculateResults(Farm farm, List<AnimalComponentEmissionsResults> animalComponentEmissionsResults);
    }
}