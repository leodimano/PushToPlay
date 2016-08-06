using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;
using PushToPlay.Model.GiantBomb.PlatformResponse;
using PushToPlay.Model.GiantBomb.GameResponse;
using PushToPlay.Model.GiantBomb.GenresResponse;
using PushToPlay.Model.GiantBomb.GameDetailResponse;
using PushToPlay.Model.GiantBomb.ReleasesResponse;

namespace PushToPlay.Core.PlatformFactory.GiantBomb
{
    public class GiantBombFactory : IDisposable
    {
        public const string API_URL_KEY = "GiantBombAPIUrl";
        public const string GAMES_API_URL_KEY = "GiantBombGamesAPI";
        public const string PLATFORMS_API_URL_KEY = "GiantBombPlatformAPI";
        public const string GENRES_API_URL_KEY = "GiantBombGenreAPI";
        public const string RELEASE_API_URL_KEY = "GiantBombReleaseApi";

        public const string API_SPECIFIC_URL_KEY = "GiantBombSpecificAPIUrl";
        public const string GAMEDETAIL_API_URL_KEY = "GiantbombAPIGameDetailCode";

        public const string PLATFORMS_ID_KEY = "GiantBombPlatformQueryID";
        public const string API_KEY = "GiantBombAPIKey";

        public string DevKey
        {
            get{ return ConfigurationManager.AppSettings[API_KEY]; }
        }

        public string GamesApiUrl
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings[API_URL_KEY],
                                     ConfigurationManager.AppSettings[GAMES_API_URL_KEY]);
            }
        }

        public string PlatformsApiUrl
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings[API_URL_KEY],
                    ConfigurationManager.AppSettings[PLATFORMS_API_URL_KEY]);
            }
        }

        public string GenresApiUrl
        {
            get
            {
                return string.Format(ConfigurationManager.AppSettings[API_URL_KEY],
                    ConfigurationManager.AppSettings[GENRES_API_URL_KEY]);
            }
        }

        public string GetGameDetailApiUrl(string giantBombGameId)
        {
            return string.Format(
                ConfigurationManager.AppSettings[API_SPECIFIC_URL_KEY],
                ConfigurationManager.AppSettings[GAMEDETAIL_API_URL_KEY],
                giantBombGameId
                );
        }

        public string GetReleaseDetailApiUrl
        {
            get
            {
                return string.Format(
                    ConfigurationManager.AppSettings[API_URL_KEY],
                    ConfigurationManager.AppSettings[RELEASE_API_URL_KEY]);
            }
        }

        public string[] PlatformIds
        {
            get { return ConfigurationManager.AppSettings[PLATFORMS_ID_KEY].Split('|'); }
        }

        public string PlatformRaw
        {
            get { return ConfigurationManager.AppSettings[PLATFORMS_ID_KEY]; }
        }

        public GiantBombReleasesResponse GetReleaseDetail(int platform_, int giantBombId_)
        {
            string _response;

            GiantBombReleasesResponse _giantBombReleaseResponse = null;

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("api_key", this.DevKey);
                _request.ParamList.Add("format", "json");
                _request.ParamList.Add("filter", string.Concat("game:", giantBombId_, ",platform:", platform_, ",region:1"));

                if (_request.ExecuteRequest(this.GetReleaseDetailApiUrl, out _response))
                {
                    JObject _jobect = JObject.Parse(_response);
                    _giantBombReleaseResponse = _jobect.ToObject<GiantBombReleasesResponse>();
                }
            }

            return _giantBombReleaseResponse;
        }

        public GiantBombGameDetailResponse GetGameDetail(string giantBombId_)
        {
            string _response;

            GiantBombGameDetailResponse _giantBombGameDetailResponse = null;

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("api_key", this.DevKey);
                _request.ParamList.Add("format", "json");
                _request.ParamList.Add("filter", string.Concat("platform:", this.PlatformRaw));

                if (_request.ExecuteRequest(this.GetGameDetailApiUrl(giantBombId_), out _response))
                {
                    JObject _jobect = JObject.Parse(_response);
                    _giantBombGameDetailResponse = _jobect.ToObject<GiantBombGameDetailResponse>();
                }
            }

            return _giantBombGameDetailResponse;
        }

        public GiantBombGenresResponse GetGenreList()
        {
            string _response;

            GiantBombGenresResponse _giantBombGenresResponse = null;

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("api_key", this.DevKey);
                _request.ParamList.Add("format", "json");

                if (_request.ExecuteRequest(this.GenresApiUrl, out _response))
                {
                    JObject _jobject = JObject.Parse(_response);
                    _giantBombGenresResponse = _jobject.ToObject<GiantBombGenresResponse>();
                }
            }

            return _giantBombGenresResponse;
        }

        public GiantBombPlatformResponse GetPlatformList()
        {
            string response;

            List<PushToPlay.Model.GiantBomb.PlatformResponse.Result> _resultList =
                new List<PushToPlay.Model.GiantBomb.PlatformResponse.Result>();

            GiantBombPlatformResponse _response = null;            

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("api_key", this.DevKey);
                _request.ParamList.Add("format", "json");
                _request.ParamList.Add("filter", string.Concat("id:", this.PlatformRaw));

                response = null;

                if (_request.ExecuteRequest(this.PlatformsApiUrl, out response))
                {
                    JObject _jobject = JObject.Parse(response);
                    _response = _jobject.ToObject<GiantBombPlatformResponse>();
                }
            }

            return _response;
        }

        public GiantBombGamesResponse GetGameList(string platform_ = "")
        {
            int backOffSet;
            int offSet = 0;
            int results_count = 0;
            string response;
            string[] _platformIds = this.PlatformIds;

            List<PushToPlay.Model.GiantBomb.GameResponse.Result> _resultList =
                new List<Model.GiantBomb.GameResponse.Result>();

            GiantBombGamesResponse _response = null;

            do
            {
                _response = null;

                using (RequestFactory _request = new RequestFactory())
                {
                    _request.ParamList.Add("api_key", this.DevKey);
                    _request.ParamList.Add("format", "json");
                    if (string.IsNullOrWhiteSpace(platform_))
                        _request.ParamList.Add("filter", string.Concat("platforms:", this.PlatformRaw));
                    else
                        _request.ParamList.Add("filter", string.Concat("platforms:", platform_));

                    _request.ParamList.Add("offset", offSet);

                    response = null;

                    if (_request.ExecuteRequest(this.GamesApiUrl, out response))
                    {
                        JObject _jobject = JObject.Parse(response);
                        _response = _jobject.ToObject<GiantBombGamesResponse>();
                        results_count = Convert.ToInt32(_response.number_of_total_results);

                        if (results_count > 0)
                        {
                            _resultList.AddRange(_response.results);
                        }
                    }
                }

                backOffSet = offSet;
                offSet += 100;

            } while (backOffSet <= results_count);

            _response.results = _resultList;

            return _response;
        }

        public void Dispose()
        {

        }
    }
}
