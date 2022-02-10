using System;

namespace AwesomeService.Repository {
    public class NewsFeed {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Abstract { get; set; }
        public DateTime Published_Date { get; set; }
    }
}