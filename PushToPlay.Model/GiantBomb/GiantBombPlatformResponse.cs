using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.GiantBomb.PlatformResponse
{
    public class Company
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

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

    public class Result
    {
        public string aliases { get; set; }
        public string abbreviation { get; set; }
        public string api_detail_url { get; set; }
        public Company company { get; set; }
        public string date_added { get; set; }
        public string date_last_updated { get; set; }
        public string deck { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public Image image { get; set; }
        public string install_base { get; set; }
        public string name { get; set; }
        public bool online_support { get; set; }
        public string original_price { get; set; }
        public string release_date { get; set; }
        public string site_detail_url { get; set; }
    }

    public class GiantBombPlatformResponse
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
