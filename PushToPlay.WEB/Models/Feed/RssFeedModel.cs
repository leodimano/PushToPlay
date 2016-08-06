using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ServiceModel.Syndication;

namespace PushToPlay.WEB.Models
{
    public enum FeedType
    {
        NewsFeed = 0,
        SteamPromoFeed = 1,
        NuuvemPromoFeed = 2
    }

    public class RssFeedModel
    {
        public RssFeedModel()
        {
            FeedItems = new List<RssFeedModelItem>();
        }

        public List<RssFeedModelItem> FeedItems { get; set; }

        public string FeedTitle { get; set; }

        public FeedType FeedType { get; set; }

        public string FeedHeight { get; set; }
    }

    public class RssFeedModelItem
    {
        public string ImagePath { get; set; }

        public DateTime PublishDate { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }

        public string DiscountPrice { get; set; }
    }
}