using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeService.Repository
{
    public class NewsFeedRepository
    {
        private NewsFeedContext _context;

        public NewsFeedRepository(NewsFeedContext context)
        {
            _context = context;
        }

        public void Add(NewsFeed newsFeed)
        {
            newsFeed.Id = Guid.NewGuid();
            _context.NewsFeeds.Add(newsFeed);
            _context.SaveChanges();
        }

        public IEnumerable<NewsFeed> Find(string url)
        {
            throw new NotImplementedException();
            //TODO:
            //Add Implementation to get Newsfeed based on the URL
        }
    }
}
