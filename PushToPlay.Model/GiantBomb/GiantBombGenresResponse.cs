using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.GiantBomb.GenresResponse
{
    public class Result
    {
        public string api_detail_url { get; set; }
        public string date_added { get; set; }
        public string date_last_updated { get; set; }
        public string deck { get; set; }
        public object description { get; set; }
        public int id { get; set; }
        public object image { get; set; }
        public string name { get; set; }
        public string site_detail_url { get; set; }
    }

    public class GiantBombGenresResponse
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
