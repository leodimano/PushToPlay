using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.GiantBomb.ReleasesResponse
{
    public class Image
    {
        public string icon_url { get; set; }
        public string medium_url { get; set; }
        public string screen_url { get; set; }
        public string small_url { get; set; }
        public string super_url { get; set; }
        public string thumb_url { get; set; }
        public string tiny_url { get; set; }
    }

    public class Region
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public Image image { get; set; }
        public Region region { get; set; }
        public string release_date { get; set; }
        public Platform platform { get; set; }
    }

    public class Platform
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    public class GiantBombReleasesResponse
    {
        public string error { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int status_code { get; set; }
        public List<Result> results { get; set; }
        public string version { get; set; }
    }
}
