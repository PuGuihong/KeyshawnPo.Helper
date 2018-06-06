using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Demo.Log4Net
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger(typeof(Program));
            try
            {
                short.Parse(null);
            }
            catch (Exception ex)
            {
                log.Info(string.Format("--------------{0} -------------------", ex.Message));
            }


            Console.ReadLine();
        }
    }
}
