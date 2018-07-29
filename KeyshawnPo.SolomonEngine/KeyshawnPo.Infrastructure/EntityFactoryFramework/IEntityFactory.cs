using KeyshawnPo.Infrastructure.DomainBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure.EntityFactoryFramework
{
    public interface IEntityFactory<T> where T : EntityBase
    {
        T BuildEntity(IDataReader reader);
    }
}
