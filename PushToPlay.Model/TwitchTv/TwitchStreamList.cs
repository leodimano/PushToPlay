﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.TwitchTv
{

    public class Links
    {
        public string self { get; set; }
        public string featured { get; set; }
        public string summary { get; set; }
        public string followed { get; set; }
        public string next { get; set; }
        public string channel { get; set; }
    }

    public class Links2
    {
        public string self { get; set; }
    }

    public class Links3
    {
        public string self { get; set; }
        public string follows { get; set; }
        public string commercial { get; set; }
        public string stream_key { get; set; }
        public string chat { get; set; }
        public string features { get; set; }
        public string subscriptions { get; set; }
        public string editors { get; set; }
        public string teams { get; set; }
        public string videos { get; set; }
    }

    public class Preview
    {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string template { get; set; }
    }

    public class Channel
    {
        public bool? mature { get; set; }
        public object abuse_reported { get; set; }
        public string status { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public int delay { get; set; }
        public int _id { get; set; }
        public string name { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public object primary_team_name { get; set; }
        public object primary_team_display_name { get; set; }
        public string logo { get; set; }
        public string banner { get; set; }
        public string video_banner { get; set; }
        public string background { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public string url { get; set; }
        public int views { get; set; }
        public int followers { get; set; }
        public Links3 _links { get; set; }
    }

    public class Stream
    {
        public object _id { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
        public Preview preview { get; set; }
        public Links2 _links { get; set; }
        public Channel channel { get; set; }
    }

    public class TwitchStreamList
    {
        public int _total { get; set; }
        public Links _links { get; set; }
        public List<Stream> streams { get; set; }
    }

    public class TwitchStream
    {
        public Links _links { get; set; }
        public Stream stream { get; set; }
    }
}
