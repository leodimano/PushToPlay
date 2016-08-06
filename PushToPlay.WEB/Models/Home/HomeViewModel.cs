using PushToPlay.WEB.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class HomeViewModel
    {
        public RssFeedModel GameNewsFeed { get; set; }

        public RssFeedModel SteamPromoFeeds { get; set; }

        public RssFeedModel NuuvemPromoFeeds { get; set; }

        public void LoadHomeFeeds()
        {
            GameNewsFeed = FeedReader.GetGameNewsFeeds();
            SteamPromoFeeds = FeedReader.GetSteamPromoFeeds();
            //NuuvemPromoFeeds = FeedReader.GetNuuvemPromoFeeds();
        }
    }
}