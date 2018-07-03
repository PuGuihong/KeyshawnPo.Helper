using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao.EntityFramework
{
    public class Class1
    {
        Entities _entity = new Entities();
        public void Save()
        {
            _entity.Tmplete.ToList();
        }
    }
}
