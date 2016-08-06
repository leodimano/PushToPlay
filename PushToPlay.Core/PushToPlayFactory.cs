using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;
using PushToPlay.Core.PlatformFactory.Steam;
using PushToPlay.Core.PlatformFactory.GiantBomb;

namespace PushToPlay.Core
{
    public class PushToPlayFactory : IDisposable
    {
        string _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "boxe");

        public void InitializeDBGenres()
        {
            Model.Genre _genre = null;
            PushToPlay.Model.GiantBomb.GenresResponse.GiantBombGenresResponse _genresResponse = null;

            using (GiantBombFactory _gbFactory = new GiantBombFactory())
            {
                _genresResponse = _gbFactory.GetGenreList();
            }

            if (_genresResponse != null)
            {
                foreach (PushToPlay.Model.GiantBomb.GenresResponse.Result _result in _genresResponse.results)
                {
                    _genre = Model.Genre.GetByGiantBombId(_result.id);

                    if (_genre == null)
                    {
                        _genre = new Model.Genre();
                        _genre.GiantBombId = _result.id;
                        _genre.Name = _result.name;
                        _genre.Description = _result.deck;
                        _genre.Add();
                    }
                    else
                    {
                        // UPDATE 
                    }
                }
            }
        }

        public void InitializeDBPlatform()
        {
            Model.Platform _platform = null;
            PushToPlay.Model.GiantBomb.PlatformResponse.GiantBombPlatformResponse _platformResponse = null;

            using (GiantBombFactory _gbFactory = new GiantBombFactory())
            {
                _platformResponse = _gbFactory.GetPlatformList();
            }

            if (_platformResponse != null)
            {
                foreach (PushToPlay.Model.GiantBomb.PlatformResponse.Result _result in _platformResponse.results)
                {
                    _platform = Model.Platform.GetPlatformByGiantBombId(_result.id);

                    if (_platform == null)
                    {
                        _platform = new Model.Platform();
                        _platform.GiantBombID = _result.id;
                        _platform.Name = _result.name;
                        _platform.Description = _result.deck;
                        _platform.Abbreviation = _result.abbreviation;
                        _platform.IconImage = _result.image.icon_url;
                        _platform.PlatformImage = _result.image.small_url;
                        _platform.Add();
                    }
                    else
                    {
                        // UPDATE 
                    }
                }
            }     
        }

        public void InitializeDBGames(string platforms_ = "")
        {
            bool _defaultIconImageUsed = false;
            bool _defaultMediumUsed = false;
            string _defaultIconImageName = string.Empty;
            string _defaultMediumImageName = string.Empty;
            string _iconImageName = string.Empty;
            string _mediumImageName = string.Empty;

            Model.Game _game = null;
            Model.Genre _genre = null;
            Model.GameDetail _gameDetail = null;
            Model.GiantBomb.GameResponse.GiantBombGamesResponse _gameListResponse = null;
            Model.GiantBomb.GameDetailResponse.GiantBombGameDetailResponse _gameDetailResponse = null;
            Model.GiantBomb.ReleasesResponse.GiantBombReleasesResponse _gameReleaseResponse = null;

            using (GiantBombFactory _gbFactory = new GiantBombFactory())
            {
                _gameListResponse = _gbFactory.GetGameList(platforms_);

                foreach (Model.GiantBomb.GameResponse.Result _result in _gameListResponse.results)                
                {
                    _gameDetailResponse = _gbFactory.GetGameDetail(_result.id.ToString());
                    
                    _game = Model.Game.GetGameByGiantBombId(_result.id);

                    _defaultIconImageUsed = false;
                    _defaultIconImageName = null;

                    _defaultMediumUsed = false;
                    _defaultMediumImageName = null;

                    if (_game == null)
                    {
                        _game = new Model.Game();                        
                        _game.Name = _result.name;
                        _game.Description = _result.deck;
                        _game.GiantBombID = _result.id;
                        _game.Aliases = _result.aliases;

                        /* Get Image */
                        if (_result.image != null)
                        {
                            _defaultIconImageName = string.Concat(_game.GiantBombID, "_icon", ".jpg");
                            _defaultMediumImageName = string.Concat(_game.GiantBombID, "_medium", ".jpg");

                            if (!File.Exists(Path.Combine(_path, _defaultIconImageName)))
                            {
                                if (_result.image.thumb_url != null)
                                {
                                    if (!Helpers.NetHelper.DownloadPicture(_result.image.thumb_url, Path.Combine(_path, _defaultIconImageName)))
                                        _defaultIconImageName = null;
                                }
                                else
                                    _defaultIconImageName = null;
                            }

                            if (!File.Exists(Path.Combine(_path, _defaultMediumImageName)))
                            {
                                if (_result.image.medium_url != null)
                                {
                                    if (!Helpers.NetHelper.DownloadPicture(_result.image.medium_url, Path.Combine(_path, _defaultMediumImageName)))
                                        _defaultMediumImageName = null;
                                }
                                else
                                    _defaultMediumImageName = null;
                            }
                        }
                        else
                        {
                            _defaultIconImageName = null;
                            _defaultMediumImageName = null;
                        }

                        if (_gameDetailResponse != null)
                        {
                            /* Get GameDetail using api */
                            if (_gameDetailResponse.number_of_total_results > 0)
                            {

                                /* Get Developers */
                                if (_gameDetailResponse.results.developers != null &&
                                    _gameDetailResponse.results.developers.Count > 0)
                                    _game.Developer = _gameDetailResponse.results.developers[0].name;

                                /* Get Publishers */
                                if (_gameDetailResponse.results.publishers != null &&
                                    _gameDetailResponse.results.publishers.Count > 0)
                                    _game.Publisher = _gameDetailResponse.results.publishers[0].name;

                                /* Genres */
                                if (_gameDetailResponse.results.genres != null)
                                {
                                    foreach (var _item in _gameDetailResponse.results.genres)
                                    {
                                        _genre = Model.Genre.GetByGiantBombId(_item.id);

                                        if (_genre != null)
                                            _game.Genres.Add(_genre);
                                    }
                                }

                                /* Platforms */
                                foreach (var _item in _gameDetailResponse.results.platforms)
                                {
                                    if (_gbFactory.PlatformIds.Contains(_item.id.ToString()))
                                    {
                                        _gameDetail = new Model.GameDetail();
                                        _gameDetail.Platform = Model.Platform.GetPlatformByGiantBombId(_item.id);
                                        _gameDetail.PlatformId = _gameDetail.Platform.Id;

                                        _iconImageName = null;
                                        _mediumImageName = null;

                                        /* Get releases by platforms and region code using api */
                                        _gameReleaseResponse = _gbFactory.GetReleaseDetail(_item.id, _result.id);
                                        if (_gameReleaseResponse != null)
                                        {
                                            foreach (var _itemRelease in _gameReleaseResponse.results)
                                            {
                                                if (_itemRelease.name == _game.Name)
                                                {
                                                    if (_itemRelease.release_date != null)
                                                    {
                                                            _gameDetail.ReleaseDate = new DateTime(Convert.ToInt32(_itemRelease.release_date.Substring(0, 4)),
                                                                                                    Convert.ToInt32(_itemRelease.release_date.Substring(5, 2)),
                                                                                                    Convert.ToInt32(_itemRelease.release_date.Substring(8, 2)));
                                                    }
                                                    else
                                                    {
                                                        if (_result.original_release_date != null)
                                                            try
                                                            {
                                                                _gameDetail.ReleaseDate = new DateTime(Convert.ToInt32(((string)_result.original_release_date).Substring(0, 4)),
                                                                                                       Convert.ToInt32(((string)_result.original_release_date).Substring(5, 2)),
                                                                                                       Convert.ToInt32(((string)_result.original_release_date).Substring(8, 2)));
                                                            }
                                                            catch { }
                                                    }
                                                }

                                                if (_itemRelease.name == _game.Name &&
                                                    _itemRelease.image != null)
                                                {

                                                    /* Validate game Name */
                                                    if (_itemRelease.name == _game.Name)
                                                    {
                                                        _iconImageName = string.Concat(_game.GiantBombID, "_", Model.Platform.TranslatePlatform(_item.id), "_icon", ".jpg");

                                                        if (_itemRelease.image != null)
                                                        {
                                                            if (!File.Exists(Path.Combine(_path, _iconImageName)))
                                                            {
                                                                if (_itemRelease.image.thumb_url != null)
                                                                {
                                                                    if (!Helpers.NetHelper.DownloadPicture(_itemRelease.image.thumb_url, Path.Combine(_path, _iconImageName)))
                                                                        _iconImageName = null;
                                                                }
                                                                else
                                                                    _iconImageName = null;
                                                            }

                                                            _mediumImageName = string.Concat(_game.GiantBombID, "_", Model.Platform.TranslatePlatform(_item.id), "_medium", ".jpg");

                                                            if (!File.Exists(Path.Combine(_path, _mediumImageName)))
                                                            {
                                                                if (_itemRelease.image.medium_url != null)
                                                                {
                                                                    if (!Helpers.NetHelper.DownloadPicture(_itemRelease.image.medium_url, Path.Combine(_path, _mediumImageName)))
                                                                        _mediumImageName = null;
                                                                }
                                                                else
                                                                    _mediumImageName = null;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            _iconImageName = null;
                                                            _mediumImageName = null;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }

                                        if (string.IsNullOrWhiteSpace(_iconImageName) &&
                                            !string.IsNullOrWhiteSpace(_defaultIconImageName))
                                        {
                                            _iconImageName = _defaultIconImageName;
                                            _defaultIconImageUsed = true;
                                        }

                                        if (string.IsNullOrWhiteSpace(_mediumImageName) &&
                                            !string.IsNullOrWhiteSpace(_defaultMediumImageName))
                                        {
                                            _mediumImageName = _defaultMediumImageName;
                                            _defaultMediumUsed = true;
                                        }

                                        _gameDetail.IconImage = _iconImageName;
                                        _gameDetail.GameImage = _mediumImageName;

                                        bool _alreadyIn = false;
                                        foreach (var detail in _game.GameDetails)
                                        {
                                            if (detail.PlatformId == _gameDetail.PlatformId)
                                                _alreadyIn = true;
                                        }

                                        if (!_alreadyIn)
                                            _game.GameDetails.Add(_gameDetail);
                                    }
                                }
                            }

                            _game.Add();
                        }
                    }
                    else
                    {
                        /* Platforms */
                        foreach (var _item in _gameDetailResponse.results.platforms)
                        {
                            if (_gbFactory.PlatformIds.Contains(_item.id.ToString()))
                            {
                                _defaultIconImageName = null;
                                _defaultMediumImageName = null;

                                _gameDetail = Model.GameDetail.GetGameDetailByGiantBombPlatform(_game.Id, _item.id);

                                if (_gameDetail == null)
                                {
                                    /* Get Image */
                                    if (_result.image != null)
                                    {
                                        _defaultIconImageName = string.Concat(_game.GiantBombID, "_icon", ".jpg");
                                        _defaultMediumImageName = string.Concat(_game.GiantBombID, "_medium", ".jpg");

                                        if (!File.Exists(Path.Combine(_path, _defaultIconImageName)))
                                        {
                                            if (_result.image.thumb_url != null)
                                            {
                                                if (!Helpers.NetHelper.DownloadPicture(_result.image.thumb_url, Path.Combine(_path, _defaultIconImageName)))
                                                    _defaultIconImageName = null;
                                            }
                                            else
                                                _defaultIconImageName = null;
                                        }

                                        if (!File.Exists(Path.Combine(_path, _defaultMediumImageName)))
                                        {
                                            if (_result.image.medium_url != null)
                                            {
                                                if (!Helpers.NetHelper.DownloadPicture(_result.image.medium_url, Path.Combine(_path, _defaultMediumImageName)))
                                                    _defaultMediumImageName = null;
                                            }
                                            else
                                                _defaultMediumImageName = null;
                                        }
                                    }
                                    else
                                    {
                                        _defaultIconImageName = null;
                                        _defaultMediumImageName = null;
                                    }

                                    _gameDetail = new Model.GameDetail();
                                    _gameDetail.Platform = Model.Platform.GetPlatformByGiantBombId(_item.id);
                                    _gameDetail.PlatformId = _gameDetail.Platform.Id;

                                    _iconImageName = null;
                                    _mediumImageName = null;

                                    /* Get releases by platforms and region code using api */
                                    _gameReleaseResponse = _gbFactory.GetReleaseDetail(_item.id, _result.id);
                                    if (_gameReleaseResponse != null)
                                    {
                                        foreach (var _itemRelease in _gameReleaseResponse.results)
                                        {
                                            if (_itemRelease.name == _game.Name)
                                            {
                                                if (_itemRelease.release_date != null)
                                                {
                                                        _gameDetail.ReleaseDate = new DateTime(Convert.ToInt32(_itemRelease.release_date.Substring(0, 4)),
                                                                                                Convert.ToInt32(_itemRelease.release_date.Substring(5, 2)),
                                                                                                Convert.ToInt32(_itemRelease.release_date.Substring(8, 2)));
                                                }
                                                else
                                                {
                                                    if (_result.original_release_date != null)
                                                        try
                                                        {
                                                            _gameDetail.ReleaseDate = new DateTime(Convert.ToInt32(((string)_result.original_release_date).Substring(0, 4)),
                                                                                                   Convert.ToInt32(((string)_result.original_release_date).Substring(5, 2)),
                                                                                                   Convert.ToInt32(((string)_result.original_release_date).Substring(8, 2)));
                                                        }
                                                        catch { }
                                                }
                                            }

                                            if (_itemRelease.name == _game.Name &&
                                                _itemRelease.image != null)
                                            {
                                                /* Validate game Name */
                                                if (_itemRelease.name == _game.Name)
                                                {
                                                    _iconImageName = string.Concat(_game.GiantBombID, "_", Model.Platform.TranslatePlatform(_item.id), "_icon", ".jpg");

                                                    if (_itemRelease.image != null)
                                                    {
                                                        if (!File.Exists(Path.Combine(_path, _iconImageName)))
                                                        {
                                                            if (_itemRelease.image.thumb_url != null)
                                                            {
                                                                if (!Helpers.NetHelper.DownloadPicture(_itemRelease.image.thumb_url, Path.Combine(_path, _iconImageName)))
                                                                    _iconImageName = null;
                                                            }
                                                            else
                                                                _iconImageName = null;
                                                        }

                                                        _mediumImageName = string.Concat(_game.GiantBombID, "_", Model.Platform.TranslatePlatform(_item.id), "_medium", ".jpg");

                                                        if (!File.Exists(Path.Combine(_path, _mediumImageName)))
                                                        {
                                                            if (_itemRelease.image.medium_url != null)
                                                            {
                                                                if (!Helpers.NetHelper.DownloadPicture(_itemRelease.image.medium_url, Path.Combine(_path, _mediumImageName)))
                                                                    _mediumImageName = null;
                                                            }
                                                            else
                                                                _mediumImageName = null;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        _iconImageName = null;
                                                        _mediumImageName = null;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                    }

                                    if (string.IsNullOrWhiteSpace(_iconImageName) &&
                                        !string.IsNullOrWhiteSpace(_defaultIconImageName))
                                    {
                                        _iconImageName = _defaultIconImageName;
                                        _defaultIconImageUsed = true;
                                    }

                                    if (string.IsNullOrWhiteSpace(_mediumImageName) &&
                                        !string.IsNullOrWhiteSpace(_defaultMediumImageName))
                                    {
                                        _mediumImageName = _defaultMediumImageName;
                                        _defaultMediumUsed = true;
                                    }

                                    _gameDetail.Game = _game;
                                    _gameDetail.GameId = _game.Id;
                                    _gameDetail.IconImage = _iconImageName;
                                    _gameDetail.GameImage = _mediumImageName;
                                    _gameDetail.Add();
                                    _gameDetail = null;
                                }
                                else
                                {
                                    /* UPDATE GAME DETAIL */
                                }        
                            }
                        }
                    }


                    if (!_defaultIconImageUsed){
                        if (!string.IsNullOrWhiteSpace(_defaultIconImageName))
                        {
                            if (File.Exists(Path.Combine(_path, _defaultIconImageName)))
                                File.Delete(Path.Combine(_path, _defaultIconImageName));
                        }
                    }

                    if (!_defaultMediumUsed){
                        if (!string.IsNullOrWhiteSpace(_defaultMediumImageName))
                        {
                            if (File.Exists(Path.Combine(_path, _defaultMediumImageName)))
                                File.Delete(Path.Combine(_path, _defaultMediumImageName));
                        }
                    }
                }
            }
        }

        private void BindAppSteam(Model.Game _game, Model.Steam.SteamAppDetailAPI _steamAppDetail)
        {
            string _fileName = string.Empty;
            bool _steamImageUsed = false;
            Model.GameDetail _detail = null;

            _game.SteamAppId = Convert.ToInt32(_steamAppDetail.steam_appid);
            _game.SteamAppUrl = string.Format(ConfigurationManager.AppSettings[SteamFactory.STEAM_COMMUNITY_URL_KEY], _steamAppDetail.steam_appid);

            if (!string.IsNullOrWhiteSpace(_steamAppDetail.about_the_game) &&
                !_steamAppDetail.about_the_game.StartsWith("#app_"))
                _game.Description = _steamAppDetail.about_the_game;

            if (_steamAppDetail.metacritic != null)
            {
                _game.MetacriticScore = _steamAppDetail.metacritic.score;
                _game.MetacriticUrl = _steamAppDetail.metacritic.url;
            }
            if (_steamAppDetail.platforms != null)
            {
                _steamImageUsed = false;

                if (_steamAppDetail.header_image != null)
                {
                    _fileName = string.Format("{0}_steam.jpg", _game.GiantBombID);

                    if (!Helpers.NetHelper.DownloadPicture(_steamAppDetail.header_image, Path.Combine(_path, _fileName)))
                        _fileName = null;
                }

                if (_steamAppDetail.platforms.linux)
                {
                    _detail = Model.GameDetail.GetGameDetailByGiantBombPlatform(_game.Id, (int)Model.GiantBomb.GiantBombPlatformEnum.LIN);

                    if (_detail != null)
                    {
                        if (_steamAppDetail.release_date != null)
                            _detail.ReleaseDate = Helpers.FactoryHelper.GetSteamDate(_steamAppDetail.release_date.date);

                        if (_fileName != null)
                        {
                            _detail.IconImage = _fileName;
                            _detail.GameImage = _fileName;

                            _steamImageUsed = true;
                        }

                        _detail.Update();
                    }
                }

                if (_steamAppDetail.platforms.mac)
                {
                    _detail = Model.GameDetail.GetGameDetailByGiantBombPlatform(_game.Id, (int)Model.GiantBomb.GiantBombPlatformEnum.MAC);

                    if (_detail != null)
                    {
                        if (_steamAppDetail.release_date != null)
                            _detail.ReleaseDate = Helpers.FactoryHelper.GetSteamDate(_steamAppDetail.release_date.date);

                        if (_fileName != null)
                        {
                            _detail.IconImage = _fileName;
                            _detail.GameImage = _fileName;

                            _steamImageUsed = true;
                        }

                        _detail.Update();
                    }
                }

                if (_steamAppDetail.platforms.windows)
                {
                    _detail = Model.GameDetail.GetGameDetailByGiantBombPlatform(_game.Id, (int)Model.GiantBomb.GiantBombPlatformEnum.PC);

                    if (_detail != null)
                    {
                        if (_steamAppDetail.release_date != null)
                            _detail.ReleaseDate = Helpers.FactoryHelper.GetSteamDate(_steamAppDetail.release_date.date);

                        if (_fileName != null)
                        {
                            _detail.IconImage = _fileName;
                            _detail.GameImage = _fileName;

                            _steamImageUsed = true;
                        }

                        _detail.Update();
                    }
                }

                if (!_steamImageUsed)
                {
                    if (!string.IsNullOrWhiteSpace(_fileName))
                    {
                        if (File.Exists(Path.Combine(_path, _fileName)))
                            File.Delete(Path.Combine(_path, _fileName));
                    }
                }
            }

            _game.Update();
        }

        public void InitializeDBSteamGames()
        {
            int _matchName = 0;
            bool _hasDeveloper;
            bool _foundDeveloper;
            bool _hasPublisher;
            bool _foundPublisher;

            List<Model.Game> _listGames = Model.Game.GetGamesWithouSteamOnly();

            if (_listGames != null &&
                _listGames.Count > 0)
            {
                using (SteamFactory _steamFactory = new SteamFactory())
                {
                    List<Model.Steam.SteamAppAPI> _listSteamApp = _steamFactory.GetCompactAppListAPI();

                    foreach (var _game in _listGames)
                    {
                        var _steamAppList = _listSteamApp.FindAll(i => i.Name.ToUpper().Trim().Contains(_game.Name.ToUpper().Trim()));

                        if (_steamAppList != null &&
                            _steamAppList.Count > 0)
                        {
                            foreach (var _steamApp in _steamAppList)
                            {
                                if (Model.Game.GetGameBySteamAppId(Convert.ToInt32(_steamApp.AppID)) != null)
                                    continue;

                                var _steamAppDetail = _steamFactory.GetAppDetailAPI(_steamApp.AppID);

                                if (_steamAppDetail != null && (_steamAppDetail.type.ToUpper().Trim().Equals("GAME") || _steamAppDetail.type.ToUpper().Trim().Equals("ADVERTISING")))
                                {
                                    List<Model.Game> _listByName = Model.Game.GetGameByName(_steamAppDetail.name);

                                    if (_listByName != null &&
                                        _listByName.Count > 0)
                                    {
                                        if (_listByName.Find(gn => gn.Id == _game.Id) == null)
                                            continue;
                                    }
                                    else
                                    {
                                        _listByName = Model.Game.GetGameByName(Helpers.FactoryHelper.AdjustGameNameNumberToRoman(_steamAppDetail.name));

                                        if (_listByName != null &&
                                            _listByName.Count > 0)
                                        {
                                            if (_listByName.Find(gn => gn.Id == _game.Id) == null)
                                                continue;
                                        }
                                        else
                                        {
                                            _listByName = Model.Game.GetGameByName(Helpers.FactoryHelper.AdjustGameNameRomanToNumber(_steamAppDetail.name));

                                            if (_listByName != null &&
                                                _listByName.Count > 0)
                                            {
                                                if (_listByName.Find(gn => gn.Id == _game.Id) == null)
                                                    continue;
                                            }
                                        }
                                    }

                                    _hasDeveloper = false;
                                    _foundDeveloper = false;
                                    _hasPublisher = false;
                                    _foundPublisher = false;

                                    StringBuilder fullGameName = new StringBuilder();
                                    foreach (char c in _game.Name)
                                    {
                                        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                                        {
                                            fullGameName.Append(c);
                                        }
                                    }
                                    fullGameName.ToString();

                                    StringBuilder fullSteamGameName = new StringBuilder();
                                    foreach (char c in _steamAppDetail.name)
                                    {
                                        if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                                        {
                                            fullSteamGameName.Append(c);
                                        }
                                    }
                                    fullSteamGameName.ToString();


                                    _matchName = string.Compare(fullGameName.ToString(), fullSteamGameName.ToString(), true);

                                    if (!string.IsNullOrWhiteSpace(_game.Developer) &&
                                        _steamAppDetail.developers != null &&
                                        _steamAppDetail.developers.Count > 0)
                                    {
                                        _hasDeveloper = true;
                                        foreach (var _developer in _steamAppDetail.developers)
                                        {
                                            if (!string.IsNullOrWhiteSpace(_developer))
                                            {
                                                if (_game.Developer.ToUpper().Trim().Contains(_developer.ToUpper().Trim()) ||
                                                    _developer.ToUpper().Trim().Contains(_game.Developer.ToUpper().Trim()))
                                                {
                                                    _foundDeveloper = true;
                                                }
                                            }
                                        }
                                    }

                                    if (!string.IsNullOrWhiteSpace(_game.Publisher) &&
                                        _steamAppDetail.publishers != null &&
                                        _steamAppDetail.publishers.Count > 0)
                                    {
                                        _hasPublisher = true;
                                        foreach (var _publisher in _steamAppDetail.publishers)
                                        {
                                            if (!string.IsNullOrWhiteSpace(_publisher))
                                            {
                                                if (_game.Publisher.ToUpper().Trim().Contains(_publisher.ToUpper().Trim()) ||
                                                    _publisher.ToUpper().Trim().Contains(_game.Publisher.ToUpper().Trim()))
                                                {
                                                    _foundPublisher = true;
                                                }
                                            }
                                        }
                                    }

                                    if (_matchName == 0)
                                    {
                                        BindAppSteam(_game, _steamAppDetail);
                                        break;
                                    }
                                    else if (_matchName == -1)
                                    {
                                        if (_hasDeveloper ||
                                            _hasPublisher)
                                        {
                                            if (_foundDeveloper ||
                                                _foundPublisher)
                                            {
                                                BindAppSteam(_game, _steamAppDetail);
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            BindAppSteam(_game, _steamAppDetail);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }                        
        }

        public void DeleteUnusedImage()
        {
            string _fileName = string.Empty;
            string[] files = Directory.GetFiles(_path);

            foreach (string _file in files)
            {
                _fileName = Path.GetFileName(_file);
                if (!Model.GameDetail.FindGameDetailByImageFile(_fileName))
                    File.Delete(_file);
            }
        }

        public void ImportSteamProfile(string steamId_)
        {
            using (SteamFactory _steamFactory = new SteamFactory())
            {
                var _steamPlayer = _steamFactory.GetPlayerSummaryAPI(steamId_);

                if (_steamPlayer != null)
                {
                    var _steamGames = _steamFactory.GetPlayerGames(_steamPlayer.SteamID);

                    if (_steamGames != null &&
                        _steamGames.response != null &&
                        _steamGames.response.game_count > 0)
                    {
                        foreach (var _steamGame in _steamGames.response.games)
                        {
                            var _game = Model.Game.GetGameBySteamAppId(_steamGame.appid);

                            if (_game != null)
                            {
                                string test = string.Empty;
                            }
                            else
                            {
                                string test = string.Empty;
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
        }
    }
}
