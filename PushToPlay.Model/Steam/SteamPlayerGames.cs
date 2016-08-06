using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.Steam.SteamPlayerGame
{
    public class Game
    {
        public int appid { get; set; }
        public int playtime_forever { get; set; }
        public int? playtime_2weeks { get; set; }
    }

    public class Response
    {
        public int game_count { get; set; }
        public List<Game> games { get; set; }
    }

    public class SteamPlayerGameResponse
    {
        public Response response { get; set; }
    }
}
