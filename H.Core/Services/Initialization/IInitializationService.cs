using H.Core.Models;
using H.Core.Services.Initialization.Animals;
using H.Core.Services.Initialization.Crops;
using System.Collections.Generic;

namespace H.Core.Services.Initialization
{
    public interface IInitializationService : ICropInitializationService, IAnimalInitializationService
    {
        void ReInitializeFarms(IEnumerable<Farm> farms);
    }
}