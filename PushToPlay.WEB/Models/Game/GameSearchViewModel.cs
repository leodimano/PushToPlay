using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class GameSearchViewModel : PushToPlayBaseViewModel
    {
        public string GameName { get; set; }

        public string ResultText { get; set; }

        public List<GameCompactViewModel> ListGameCompactViewModel { get; set; }

        public void SetGameList(List<GameDetail> gameDetailList_)
        {
            this.ListGameCompactViewModel = new List<GameCompactViewModel>();

            if (gameDetailList_ != null)
            {
                foreach (var _detail in gameDetailList_)
                {
                    this.ListGameCompactViewModel.Add(new GameCompactViewModel(_detail, true));
                }
            }
        }

        public List<int> GetSelectedPlatforms(string[] platforms_)
        {
            List<int> _listSelected = new List<int>();

            if (platforms_.Contains("MAC"))
                _listSelected.Add(1);

            if (platforms_.Contains("X360"))
                _listSelected.Add(2);

            if (platforms_.Contains("PS3"))
                _listSelected.Add(3);

            if (platforms_.Contains("WII"))
                _listSelected.Add(4);

            if (platforms_.Contains("PC"))
                _listSelected.Add(5);

            if (platforms_.Contains("WIIU"))
                _listSelected.Add(6);

            if (platforms_.Contains("XONE"))
                _listSelected.Add(7);

            if (platforms_.Contains("PS4"))
                _listSelected.Add(8);

            if (platforms_.Contains("LINUX"))
                _listSelected.Add(9);

            return _listSelected;
        }

    }
}