using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AwesomeService.Repository {
    public class NewsFeedRepository {
        private NewsFeedContext _context;
        public NewsFeedRepository(NewsFeedContext context) {
            _context = context;
        }

        public bool Add(NewsFeed newsFeed) {
            // Check if the newsItem exists based on URL
            if (Exists(newsFeed.Url) == true) return false;
            
            // It doesn't so add it
            newsFeed.Id = Guid.NewGuid();
            _context.NewsFeeds.Add(newsFeed);
            _context.SaveChanges();
            return true;
        }

        public IQueryable<NewsFeed> Find(string url) {
            // IQueryable applies filters to database, not a local copy of all the data
            return _context.NewsFeeds
                .Where(u => u.Url == url);
        }
        private bool Exists(string url) {
            if (Find(url).Count() > 0) return true;
            return false;
        }
    }
}