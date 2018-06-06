using Common.Logging;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading;


namespace KeyshawnPo.Demo.QuartzConsole
{
    public class SendMsgAction : IExample
    {
        public string Name
        {
            get { throw new NotImplementedException(); }
        }

        public void Run()
        {
            ILog log = LogManager.GetLogger(typeof(SendMsgAction));

            log.Info("------- 任务初始化 ----------------------");

            // First we must get a reference to a scheduler
            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = sf.GetScheduler();

            log.Info("------- 任务初始化完成 -----------");


            // computer a time that is on the next round minute
            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);

            log.Info("------- 任务调度  -------------------");

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<SendMsgJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run on the next round minute
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartAt(runTime)
                .Build();

            // Tell quartz to schedule the job using our trigger
            sched.ScheduleJob(job, trigger);
            log.Info(string.Format("{0} 将在 {1} 执行！", job.Key, runTime.ToString("yyyy-MM-dd HH:mm:ss,ffff")));

            // Start up the scheduler (nothing can actually run until the 
            // scheduler has been started)
            sched.Start();
            log.Info("------- 开始调度 -----------------");

            // wait long enough so that the scheduler as an opportunity to 
            // run the job!
            log.Info("------- 等待12秒钟... -------------");

            // wait 65 seconds to show jobs
            Thread.Sleep(TimeSpan.FromSeconds(12));

            // shut down the scheduler
            log.Info("------- 任务调度关闭 ---------------------");
            sched.Shutdown(true);
            log.Info("------- 任务调度关闭完成! -----------------");
        }
    }
}
