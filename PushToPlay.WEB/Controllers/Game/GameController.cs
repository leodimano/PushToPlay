using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PushToPlay.Model;
using PushToPlay.WEB.Models;
using PushToPlay.Core.PlatformFactory.Twitch;
using PushToPlay.Model.TwitchTv;
using PushToPlay.Helpers;

namespace PushToPlay.WEB.Controllers.Game
{
    public class GameController : Controller
    {
        #region Home Actions

        public ActionResult Home(string id, string gamename, string page)
        {
            int _id;
            
            if (!string.IsNullOrWhiteSpace(id) && Int32.TryParse(id, out _id))
            {
                var _gameDetail = GameDetail.GetGameDetailById(Convert.ToInt32(id));

                if (_gameDetail != null)
                {
                    int _pageNumber;
                    Int32.TryParse(page, out _pageNumber);
                    if (_pageNumber <= 0)
                        _pageNumber = 1;

                    var _gameHomeViewModel = new GameHomeViewModel(_gameDetail);
                    _gameHomeViewModel.InitilizeMessages(_pageNumber);

                    return View(_gameHomeViewModel);
                }
            }

            return RedirectToRoute(Constants.ROUTE_HOME);
        }

        public ActionResult HomeAdd(string id)
        {
            Model.GameDetail _detail = Model.GameDetail.GetGameDetailById(Convert.ToInt32(id));
            this.AddGame(_detail.GameId, _detail.Id, WebHelper.GetLoggedUserId());
            return RedirectToRoute(Constants.ROUTE_GAME_HOME, new { id = _detail.Id, gamename = WebHelper.ProcessUrlEnconding(_detail.Game.Name) });
        }

        public ActionResult HomeRemove(string id)
        {
            Model.GameDetail _detail = Model.GameDetail.GetGameDetailById(Convert.ToInt32(id));
            this.RemoveGame(_detail.Id, WebHelper.GetLoggedUserId());
            return RedirectToRoute(Constants.ROUTE_GAME_HOME, new { id = _detail.Id, gamename = WebHelper.ProcessUrlEnconding(_detail.Game.Name) });
        }

        #endregion

        #region Search Actions

        [HttpGet]
        public ActionResult Search(string name, string platform, string page)
        {
            return View(this.SearchGames(name, platform, page));
        }

        private GameSearchViewModel SearchGames(string gameName_, string platforms_, string page_)
        {
            GameSearchViewModel _gameSearch = new GameSearchViewModel();

            int _pageNumber;
            List<int> _selectedPlatform = null;

            if (string.IsNullOrWhiteSpace(gameName_))
            {
                gameName_ = string.Empty;
            }

            Int32.TryParse(page_, out _pageNumber);

            if (!string.IsNullOrWhiteSpace(platforms_))
            {
                _selectedPlatform = _gameSearch.GetSelectedPlatforms(platforms_.Split(','));
            }

            var _listGame = PushToPlay.Model.GameDetail.GetGameDetailByLikeName(gameName_, _selectedPlatform, _pageNumber);

            if (_listGame != null)
            {
                _gameSearch.CurrentPage = _pageNumber;
                _gameSearch.SetGameList(_listGame.Objects);
                _gameSearch.TotalPage = _listGame.TotalPages;
                _gameSearch.ObjectCount = _listGame.TotalObject;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(_gameSearch.GameName))
                {
                    _gameSearch.ResultText = string.Format("Nenhum jogo foi encontrado para '{0}'", _gameSearch.GameName);
                }
                else
                {
                    _gameSearch.ResultText = string.Format("Não foi encontrado nenhum jogo com os filtros informados");
                }
            }

            return _gameSearch;
        }

        #endregion

        #region Partial Game Card Actions

        [HttpPost]
        public ActionResult Add(string id)
        {
            Model.GameDetail _detail = Model.GameDetail.GetGameDetailById(Convert.ToInt32(id));
            this.AddGame(_detail.GameId, _detail.Id, WebHelper.GetLoggedUserId());
            GameCompactViewModel _gameCompactView = new GameCompactViewModel(_detail, true);

            return PartialView("GameCompactView", _gameCompactView);
        }

        [HttpPost]
        public ActionResult Remove(string id)
        {
            Model.GameDetail _detail = Model.GameDetail.GetGameDetailById(Convert.ToInt32(id));
            this.RemoveGame(_detail.Id, WebHelper.GetLoggedUserId());
            GameCompactViewModel _gameCompactView = new GameCompactViewModel(_detail, true);

            return PartialView("GameCompactView", _gameCompactView);
        }

        #endregion

        #region Private Methods

        private void AddGame(int gameId_, int gameDetailId_, int userId_)
        {
            Model.UserGame _userGame = new UserGame();
            _userGame.UserId = userId_;
            _userGame.GameId = gameId_;
            _userGame.GameDetailId = gameDetailId_;
            _userGame.AssociateUserGameDetail();
        }

        private void RemoveGame(int gameDetailId_, int userId_)
        {
            Model.UserGame.DeleteAssociation(WebHelper.GetLoggedUserId(), gameDetailId_);
        }

        #endregion

        #region TwitchTv

        public ActionResult Streams(string id, string gamename)
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var _gameDetail = GameDetail.GetGameDetailById(Convert.ToInt32(id));

                if (_gameDetail != null)
                {
                    var _gameStreamViewModel = new GameStreamsViewModel(_gameDetail);
                    _gameStreamViewModel.GetStreamList();
                    return View(_gameStreamViewModel);
                }
            }

            return RedirectToRoute(Constants.ROUTE_GAME_HOME, new { id = id, gamename = gamename });
        }

        public ActionResult Watch(string id, string gamename, string streamid)
        {
            if (!string.IsNullOrWhiteSpace(id) &&
                !string.IsNullOrEmpty(streamid))
            {
                GameDetail _gameDetail = GameDetail.GetGameDetailById(Convert.ToInt32(id));
                TwitchStream _streamList = null;

                using (TwitchFactory _factory = new TwitchFactory())
                {
                    _streamList = _factory.GetStreamListByChannelName(streamid);
                }

                if (_gameDetail != null &&
                    _streamList != null &&
                    _streamList.stream != null)
                {
                    return View("StreamViewer", new GameStreamViewerViewModel(_gameDetail, new StreamCompactViewModel(_streamList.stream)));
                }
            }

            return RedirectToRoute(Constants.ROUTE_GAME_HOME, new { id = id, gamename = gamename });
        }

        #endregion
    }
}
