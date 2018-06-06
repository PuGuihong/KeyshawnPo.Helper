using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Demo.QuartzConsole
{
    public interface IExample
    {
        string Name
        {
            get;
        }

        void Run();
    }
}
