using H.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace H.Core.Enumerations
{
    public static class CoverCropTerminationTypeExtensions
    {
        public static IEnumerable<CoverCropTerminationType> GetValidCoverCropTerminationTypes()
        {
            return new List<CoverCropTerminationType>()
            {
                CoverCropTerminationType.Chemical, CoverCropTerminationType.Mechanical
            }.OrderBy(terminationType => terminationType.GetDescription());
        }
    }
}