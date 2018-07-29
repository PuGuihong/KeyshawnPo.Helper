using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Infrastructure.Domain.Base
{
    public abstract class EntityBase
    {
        private object key;

        protected EntityBase()
        {

        }

        protected EntityBase(object key)
        {
            this.key = key;
        }

        public object Key
        {
            get { return this.key; }
        }
    }
}
