using System;
using System.Configuration;

namespace KeyshawnPo.Infrastructure.EntityFramework.Configuration
{
    public class EntitySettings : ConfigurationSection
    {
        [ConfigurationProperty(EntityMappingConstants.ConfigurationPropertyName
            , IsDefaultCollection = true)]
        public EntityMappingCollection EntityMappings
        {
            get
            {
                return (EntityMappingCollection)base[EntityMappingConstants.ConfigurationPropertyName];
            }
        }
    }
}
