using AwesomeService.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
namespace AwesomeService {
    public class JobScheduler {
        private readonly IScheduler _scheduler;
        public JobScheduler() {
            NameValueCollection props = new NameValueCollection {
                { "quartz.serializer.type", "binary" },
                { "quartz.scheduler.instanceName", "MyScheduler" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" }
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(props);
            _scheduler = factory.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        public void Start() {
            ScheduleJobs();
            _scheduler.Start().ConfigureAwait(false).GetAwaiter().GetResult();
        }
        private void ScheduleJobs() {
            ScheduleJobWithCronSchedule<RepeatMeJob>(1);
        }
        private void ScheduleJobWithCronSchedule<T>(int intervalInMinutes) where T : IJob {
            var jobName = typeof(T).Name;
            var job = JobBuilder
                .Create<T>()
                .WithIdentity(jobName, $"{jobName}-Group")
                .Build();

            var cronTrigger = TriggerBuilder
                .Create()
                .ForJob(job)
                 .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(intervalInMinutes)
                    .RepeatForever())
                .WithIdentity($"{jobName}-Trigger")
                .StartNow()
                .Build();
 
            _scheduler.ScheduleJob(job, cronTrigger);
        }
        public void Stop() {
            _scheduler.Shutdown().ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}