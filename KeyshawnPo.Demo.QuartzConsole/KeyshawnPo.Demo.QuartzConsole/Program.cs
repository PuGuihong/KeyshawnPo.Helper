using Common.Logging;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyshawnPo.Demo.QuartzConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = LogManager.GetLogger(typeof(Program));
            try
            {
                IExample example = ObjectUtils.InstantiateType<IExample>(typeof(SendMsgAction));
                example.Run();
                Console.WriteLine("任务成功执行.");
            }
            catch (Exception ex)
            {
                log.Info(string.Format("--------------{0} -------------------", ex.Message));
            }

            Console.ReadLine();

        }
    }
}
