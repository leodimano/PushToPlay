using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class StreamCompactViewModel : PushToPlayBaseViewModel
    {
        public int ObjectId { get; set; }

        public string ObjectName { get; set; }
        
        public string StreamName { get; set; }

        public string ChannelName { get; set; }

        public string ChannelDescription { get; set; }

        public string ChannelImage { get; set; }

        public int ChannelFollowers { get; set; }

        public int StreamViewers { get; set; }

        public StreamCompactViewModel()
        {

        }

        public StreamCompactViewModel(PushToPlay.Model.TwitchTv.Stream stream_)
        {
            this.StreamName = stream_.channel.name;
            this.ChannelName = stream_.channel.display_name;
            this.ChannelDescription = stream_.channel.status;
            this.ChannelImage = stream_.channel.logo;
            this.ChannelFollowers = stream_.channel.followers;
            this.StreamViewers = stream_.viewers;
        }
    }
}