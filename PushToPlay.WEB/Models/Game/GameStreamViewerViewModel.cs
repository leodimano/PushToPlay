using PushToPlay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class GameStreamViewerViewModel : GameHomeViewModel
    {
        public TwitchStreamViewerViewModel TwitchStreamViewerModel { get; set; }
        public StreamCompactViewModel StreamViewModel { get; set; }

        public GameStreamViewerViewModel(GameDetail gameDetail_)
            : base(gameDetail_)
        {
            this.StreamViewModel = new StreamCompactViewModel();
            this.TwitchStreamViewerModel = new TwitchStreamViewerViewModel();
        }

        public GameStreamViewerViewModel(GameDetail gameDetail_, StreamCompactViewModel _streamViewModel_)
            : base(gameDetail_)
        {
            this.StreamViewModel = _streamViewModel_;

            this.TwitchStreamViewerModel = new TwitchStreamViewerViewModel();
            this.TwitchStreamViewerModel.StreamKey = _streamViewModel_.StreamName;

        }

    }
}