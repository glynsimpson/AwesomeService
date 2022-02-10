using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AwesomeService.Repository {
    public class NewsFeedContext : DbContext {
        public NewsFeedContext() { }
        public NewsFeedContext(DbContextOptions<NewsFeedContext> options) : base(options) { }
        public DbSet<NewsFeed> NewsFeeds { get; set; }
        #region OnConfiguring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseInMemoryDatabase(databaseName: "AwesomeService");
            }
        }
        #endregion
    }
}