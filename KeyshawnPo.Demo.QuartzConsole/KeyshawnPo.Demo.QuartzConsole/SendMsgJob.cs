using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KeyshawnPo.Demo.QuartzConsole
{
    public class SendMsgJob : IJob
    {
        private static ILog _log = LogManager.GetLogger(typeof(SendMsgJob));

        public SendMsgJob() { }


        public void Execute(IJobExecutionContext context)
        {
            _log.Info(string.Format("执行具体的方法! - {0}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ffff")));
        }
    }
}
