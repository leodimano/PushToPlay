using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.GiantBomb.GameDetailResponse
{
    public class Platform
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
        public string abbreviation { get; set; }
    }

    public class Developer
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class Genre
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class Publisher
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class Release
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class SimilarGame
    {
        public string api_detail_url { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class Results
    {
        public object aliases { get; set; }
        public string api_detail_url { get; set; }
        public string date_added { get; set; }
        public string date_last_updated { get; set; }
        public object deck { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public List<Platform> platforms { get; set; }
        public List<Developer> developers { get; set; }
        public List<Genre> genres { get; set; }
        public List<Publisher> publishers { get; set; }
        public List<Release> releases { get; set; }
        public List<SimilarGame> similar_games { get; set; }
    }

    public class GiantBombGameDetailResponse
    {
        public string error { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public int number_of_page_results { get; set; }
        public int number_of_total_results { get; set; }
        public int status_code { get; set; }
        public Results results { get; set; }
        public string version { get; set; }
    }
}
