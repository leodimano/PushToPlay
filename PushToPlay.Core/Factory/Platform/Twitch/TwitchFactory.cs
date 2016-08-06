using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PushToPlay.Model.TwitchTv;
using PushToPlay.Core.PlatformFactory;
using Newtonsoft.Json.Linq;

namespace PushToPlay.Core.PlatformFactory.Twitch
{
    public class TwitchFactory : IDisposable
    {
        public TwitchFactory()
        {

        }

        public TwitchStreamList GetStreamListByGame(string gameName_, int limitPerPage_ = 25, string optionalUrl_ = "")
        {
            bool _requestOk = false;
            string _response = null;
            string _urlRequest = "api.twitch.tv/kraken/streams";

            TwitchStreamList _twitchStreamList = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                if (string.IsNullOrWhiteSpace(optionalUrl_))
                {
                    _factory.ParamList.Add("game", gameName_);
                    _factory.ParamList.Add("limit", limitPerPage_);
                    _factory.ParamList.Add("offset", 0);

                    _requestOk = _factory.ExecuteRequest(_urlRequest, out _response, true);
                }
                else
                {
                    optionalUrl_ = optionalUrl_.Replace("https://", string.Empty);
                    _requestOk = _factory.ExecuteRequest(optionalUrl_, out _response, true);
                }

                if (_requestOk)
                {
                    JObject _jobject = JObject.Parse(_response);
                    _twitchStreamList = _jobject.ToObject<TwitchStreamList>();
                }
                else
                {
                }
            }

            return _twitchStreamList;
        }

        public TwitchStream GetStreamListByChannelName(string channelName_)
        {
            bool _requestOk = false;
            string _response = null;
            string _urlRequest = "api.twitch.tv/kraken/streams/";

            TwitchStream _twitchStream = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _urlRequest = string.Concat(_urlRequest, @"/", channelName_);

                _requestOk = _factory.ExecuteRequest(_urlRequest, out _response, true);

                if (_requestOk)
                {
                    JObject _jobject = JObject.Parse(_response);
                    _twitchStream = _jobject.ToObject<TwitchStream>();
                }
            }

            return _twitchStream;
        }


        public void Dispose()
        {

        }
    }
}
