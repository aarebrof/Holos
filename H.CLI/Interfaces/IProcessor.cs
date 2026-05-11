using H.Core.Models;
using System.Collections.Generic;

namespace H.CLI.Interfaces
{
    public interface IProcessor
    {
        void ProcessComponent(Farm farm, List<ComponentBase> component, ApplicationData applicationData);
    }
}
