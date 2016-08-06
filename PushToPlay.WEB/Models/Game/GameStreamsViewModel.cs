using PushToPlay.Core.PlatformFactory.Twitch;
using PushToPlay.Model;
using PushToPlay.Model.TwitchTv;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class GameStreamsViewModel : GameHomeViewModel
    {
        public TwitchStreamList TwitchStreams { get; set; }

        public GameStreamsViewModel(GameDetail detail)
            : base(detail)
        {

        }

        public void GetStreamList()
        {
            using (TwitchFactory _factory = new TwitchFactory())
            {
                TwitchStreams = _factory.GetStreamListByGame(base.GameName);
            }            
        }
    }
}