using AwesomeService.Repository;
using Quartz;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;

namespace AwesomeService.Jobs {
    public class RepeatMeJob : IJob {
        private NewsFeedContext _context;
        public RepeatMeJob() {
            _context = new NewsFeedContext();
        }
        public Task Execute(IJobExecutionContext context) {
            Console.WriteLine($"Fetching news feed now {DateTime.Now}");
            NewsFeed newsFeed = ExtractNewsFeed();
            Console.WriteLine($"Dispatched news feed - Title: {newsFeed.Title}");
            return Task.CompletedTask;
        }
        public NewsFeed ExtractNewsFeed() {
            int offsetValue = 1;
            do {
                //Fetch the data from the defaultApiUrl
                //      Convert the json data into NewsFeed object, never used AutoMapper, so using Newtonsoft.Json
                Message newsItem = JsonConvert.DeserializeObject<Message>((new WebClient()).DownloadString(string.Format(ConfigurationManager.AppSettings["AwesomeNewsAPI"], 0) + offsetValue.ToString()).ToString());

                NewsFeedRepository repo = new NewsFeedRepository(_context);
                // Try adding, if this newsItem exists, skip to the next
                if (repo.Add(newsItem.results[0]) == false) {
                    offsetValue += 1;
                } else {
                    return newsItem.results[0];
                }
            // Only cycle through 50 newsItems at most - as the API only gets the latest newsItem, 
            //      the loop is redundent anyway as, if the service has been running 
            //      if the latest newsItem already exists in the database,
            //      there's no point going any deeper as we'll already have the older newsItems
            } while (offsetValue <= 50);
            return null;
        }

        /// <summary>
        /// This opens the default browser with the URL
        /// </summary>
        /// <param name="url"></param>
        private void OpenBrowser(string url) {
            try {
                Process.Start(url);
            } catch {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                    Process.Start("xdg-open", url);
                } else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                    Process.Start("open", url);
                } else {
                    throw;
                }
            }
        }

        public class Message {
            // Model of the wrapper class
            // Would prove useful information in production as it holds status information which we can use to see if the service is working
            public string status { get; set; }
            public string copyright { get; set; }
            public string num_results { get; set; }
            public List<NewsFeed> results { get; set; }
        }

    
    
    
    }
}