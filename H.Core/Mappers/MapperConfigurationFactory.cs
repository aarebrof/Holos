using System;
using AutoMapper;

namespace H.Core.Mappers
{
    /// <summary>
    /// Helper to create MapperConfiguration instances without requiring an ILoggerFactory.
    /// AutoMapper 16+ requires ILoggerFactory as a constructor parameter.
    /// </summary>
    public static class MapperConfigurationFactory
    {
        public static MapperConfiguration Create(Action<IMapperConfigurationExpression> configure)
        {
            return new MapperConfiguration(configure, null);
        }
    }
}
