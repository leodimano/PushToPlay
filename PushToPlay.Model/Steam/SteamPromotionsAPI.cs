using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.Steam
{
    public class Item
    {
        public object id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public bool discounted { get; set; }
        public int discount_percent { get; set; }
        public int original_price { get; set; }
        public int final_price { get; set; }
        public string currency { get; set; }
        public string large_capsule_image { get; set; }
        public string small_capsule_image { get; set; }
        public int discount_expiration { get; set; }
        public string header_image { get; set; }
        public string headline { get; set; }
    }

    public class Specials
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Item> items { get; set; }
    }

    public class SteamPromotionApiResponse
    {
        public Specials specials { get; set; }
        public int status { get; set; }
    }
}
