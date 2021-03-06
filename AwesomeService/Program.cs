using System;
using Topshelf;

namespace AwesomeService {
    class Program {
        static void Main(string[] args) {

            HostFactory.Run(x => {
                x.Service<JobScheduler>(s => {
                    s.ConstructUsing(name => new JobScheduler());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsNetworkService();
                x.SetDescription("AwesomeService , Windows service which is just awesome");
                x.SetDisplayName("AwesomeService");
                x.SetServiceName("AwesomeService");
                x.StartAutomatically();
            });
        }

    }
}