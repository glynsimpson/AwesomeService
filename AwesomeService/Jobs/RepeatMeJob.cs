using AwesomeService.Repository;
using Quartz;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AwesomeService.Jobs
{
    public class RepeatMeJob : IJob
    {
        public RepeatMeJob()
        {
        }

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine($"Fetching News Feed Now {DateTime.Now}");
            var newsFeed = ExtractNewsFeed();
            Console.WriteLine($"Dispactched News Feed - Title: {newsFeed.Title}");
            return Task.CompletedTask;
        }

        public NewsFeed ExtractNewsFeed()
        {
            var apiUrl = ConfigurationManager.AppSettings["AwesomeNewsAPI"];
            var defaultApiUrl = string.Format(apiUrl, 0); // this is with the default offset of 0, which means it takes the first article from the news feed. if u need the 2nd article you may have to change this to 1, if 3rd article then 2 and so on.

            //TODO:
            //1. Fetch the data from the defaultApiUrl.
            //2. Convert the json data into NewsFeed object. Use Automapper is preferrable :)
            //3. Check if this NewsFeed object is already present in the database, based on the Url.
            //      a. If the Url is present , that means this news was already read, so get the next data from the feed. You can do this by increasing the offset in the url to 1 (similary to defaultApiUrl).
            //      b. Continue the steps from 1 to 3 untill u get a new NewsFeed Url.
            //4. If the NewsFeed object is a new one, Save the data into the table NewsFeed
            //5. Call the OpenBrowser method with the Url once the data is saved.
            //PLEASE NOTE : ALL THE RETREIVAL AND SAVING TO DATABASE SHOULD GO VIA NewsFeedRepository.cs

            throw new NotImplementedException();
        }

        /// <summary>
        /// This opens the default browser with the URL
        /// </summary>
        /// <param name="url"></param>
        private void OpenBrowser(string url)
        {
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }
    }
}