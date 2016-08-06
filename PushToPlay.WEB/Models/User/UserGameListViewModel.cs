using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PushToPlay.Model;

namespace PushToPlay.WEB.Models
{
    public class UserGameListViewModel : PushToPlayBaseViewModel
    {
        public List<GameCompactViewModel> ListGameCompactViewModel = null;

        public UserGameListViewModel(string currentUserName_, int pageNumber_ = 1)
            : base()
        {
            int _pageNumber = pageNumber_ <= 0 ? 1 : pageNumber_;
            base.CurrentUser = Model.User.GetUserByUserName(currentUserName_);

            if (base.CurrentUser != null)
            {
                base.InitializeRelationWith();

                var _gameListByUser = GameDetail.GetGameDetailByUser(base.CurrentUser.Id, _pageNumber);

                if (_gameListByUser != null && _gameListByUser.TotalObject > 0)
                {
                    base.CurrentPage = _pageNumber;
                    base.TotalPage = _gameListByUser.TotalPages;
                    base.ObjectCount = _gameListByUser.TotalObject;

                    ListGameCompactViewModel = new List<GameCompactViewModel>();

                    foreach (var _gameDetail in _gameListByUser.Objects)
                    {
                        ListGameCompactViewModel.Add(new GameCompactViewModel(_gameDetail, true));
                    }
                }
            }
        }
    }
}