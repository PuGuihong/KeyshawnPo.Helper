using KeyshawnPo.Infrastructure.RepositoryFramework.Configuration;
using System.Configuration;

namespace KeyshawnPo.Infrastructure.EntityFramework.Configuration
{
    public class EntityMappingElement : ConfigurationElement
    {
        [ConfigurationProperty(EntityMappingConstants.InterfaceShortTypeNameAttributeName, IsKey = true, IsRequired = true)]
        public string InterfaceShortTypeName
        {
            get
            {
                return (string)this[EntityMappingConstants.InterfaceShortTypeNameAttributeName];
            }
            set
            {
                this[EntityMappingConstants.InterfaceShortTypeNameAttributeName] = value;
            }
        }

        [ConfigurationProperty(EntityMappingConstants.EntityFullTypeNameAttributeName, IsRequired = true)]
        public string EntityFullTypeName
        {
            get
            {
                return (string)this[EntityMappingConstants.EntityFullTypeNameAttributeName];
            }
            set
            {
                this[EntityMappingConstants.EntityFullTypeNameAttributeName] = value;
            }
        }
    }

}
