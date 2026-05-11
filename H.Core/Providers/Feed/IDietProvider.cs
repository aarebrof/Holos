using H.Core.Enumerations;
using System.Collections.Generic;

namespace H.Core.Providers.Feed
{
    public interface IDietProvider
    {
        List<Diet> GetDiets();
        Diet GetNoDiet();
        List<AnimalType> GetValidAnimalDietTypes(AnimalType animalType);
    }
}