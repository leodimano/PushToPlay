using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PushToPlay.Model.LoL;
using PushToPlay.Core.PlatformFactory;
using Newtonsoft.Json.Linq;

using System.Collections;

namespace PushToPlay.Core.PlatformFactory.LoL
{
    public class LoLFactory
    {
        #region Singleton

        private static LoLFactory _currentInstance;

        public static LoLFactory Instance
        {
            get
            {
                if (_currentInstance == null)
                {
                    _currentInstance = new LoLFactory();
                    return _currentInstance;
                }
                else
                {
                    return _currentInstance;
                }
            }
        }

        #endregion

        #region Constructor/Initialization

        public void InitializeLoLFactory(string region_)
        {
            /* Initializes realm data */
            InitializeRealmData(region_);

            /* Initializes Cached Objects */
            InitializeCachedObjects(region_);
        }
                
        private void InitializeRealmData(string region_)
        {

            this._realm = this.GetRealmData(region_);
        }

        private void InitializeCachedObjects(string region_)
        {
            _championList = new List<LoLChampion>();
            _itemList = new List<LoLItem>();

            _championList = this.GetChampionList(region_);
            _itemList = this.GetItemList(region_);
        }

        #endregion

        #region Cached Data

        private List<LoLChampion> _championList;
        private List<LoLItem> _itemList;

        #endregion

        #region Addresses

        private const string API_DEFAULT_LOCATION = "br.api.pvp.net";

        private const string REALM_ADDRESS = "/api/lol/static-data/{region}/v1.2/realm";

        private const string CDN_GROUP_IMAGE = "{cdn}/{dd_version}/img/{group}/{full_name}";
        private const string CDN_CHAMPION_LOADING = "{cdn}/img/champion/loading/{key}_{number}.jpg";
        private const string CDN_CHAMPION_SPLASH = "{cdn}/img/champion/splash/{key}_{number}.jpg";

        private const string SUMMONER_BY_NAME_ADDRESS = "/api/lol/{region}/v1.4/summoner/by-name/{summonerNames}";
        private const string SUMMONER_SUMMARY_ADDRESS = "/api/lol/{region}/v1.3/stats/by-summoner/{summonerId}/summary";
        private const string SUMMONER_RECENT_ADDRESS = "/api/lol/{region}/v1.3/game/by-summoner/{summonerId}/recent";

        private const string CHAMPION_ADDRESS = "/api/lol/static-data/{region}/v1.2/champion/{id}";
        private const string CHAMPION_LIST_ADDRESS = "/api/lol/static-data/{region}/v1.2/champion";

        private const string ITEM_ADDRESS = "/api/lol/static-data/{region}/v1.2/item/{id}";
        private const string ITEM_LIST_ADDRESS = "/api/lol/static-data/{region}/v1.2/item";

        private string AddressRealm(string region_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 REALM_ADDRESS.Replace("{region}", region_));
        }

        private string AddresSummonerByName(string region_, string summonerName_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 SUMMONER_BY_NAME_ADDRESS.Replace("{region}", region_).Replace("{summonerNames}", summonerName_));
        }
        private string AddresSummonerStats(string region_, string summonerId_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 SUMMONER_SUMMARY_ADDRESS.Replace("{region}", region_).Replace("{summonerId}", summonerId_));
        }
        private string AddressSummonerRecent(string region_, string summonerId_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 SUMMONER_RECENT_ADDRESS.Replace("{region}", region_).Replace("{summonerId}", summonerId_));
        }

        private string AddressChampion(string region_, string championId_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 CHAMPION_ADDRESS.Replace("{region}", region_).Replace("{id}", championId_));
        }
        private string AddressChampionList(string region_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 CHAMPION_LIST_ADDRESS.Replace("{region}", region_));
        }

        private string AddressItem(string region_, string itemId_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 ITEM_ADDRESS.Replace("{region}", region_).Replace("{id}", itemId_));
        }
        private string AddressItemList(string region_)
        {
            return string.Concat(API_DEFAULT_LOCATION,
                                 ITEM_LIST_ADDRESS.Replace("{region}", region_));
        }

        private string CDNVersionGroupImage(string cdn_, string version_, string group_, string image_)
        {
            return CDN_GROUP_IMAGE.Replace("{cdn}", cdn_)
                                  .Replace("{dd_version}", version_)
                                  .Replace("{group}", group_)
                                  .Replace("{full_name}", image_);
        }
        private string CDNChampionLoading(string cdn_, string key_, int number)
        {
            return CDN_CHAMPION_LOADING.Replace("{cdn}", cdn_)
                                       .Replace("{key}", key_)
                                       .Replace("{number}", number.ToString());
        }
        private string CDNChampionSplash(string cdn_, string key_, int number)
        {
            return CDN_CHAMPION_SPLASH.Replace("{cdn}", cdn_)
                                      .Replace("{key}", key_)
                                      .Replace("{number}", number.ToString());
        }

        #endregion

        private LoLRealm _realm;

        public LoLRealm Realm
        {
            get { return this._realm; }
        }

        private string DevKey
        {
            get { return "e6a197f9-dc8d-4406-a6ae-d10acde944f1"; }
        }

        public LoLSummoner GetSummonerByName(string region_, string summonerName_)
        {
            bool _requestOK = false;
            string _response = null;
            LoLSummoner _summoner = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(AddresSummonerByName(region_, summonerName_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _summoner = _jObject.First.First.ToObject<LoLSummoner>();
                    }
                }
            }

            return _summoner;
        }

        public LoLSummonerStats GetSummonerStats(string region_, string summonerId_)
        {
            bool _requestOK = false;
            string _response = null;
            LoLSummonerStats _summonerStats = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddresSummonerStats(region_, summonerId_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _summonerStats = _jObject.ToObject<LoLSummonerStats>();
                    }
                }
            }

            return _summonerStats;
        }

        public LoLSummonerRecent GetSummonerRecent(string region_, string summonerId_)
        {
            bool _requestOK = false;
            string _response = null;
            LoLSummonerRecent _summonerRecent = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressSummonerRecent(region_, summonerId_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _summonerRecent = _jObject.ToObject<LoLSummonerRecent>();
                    }
                }
            }

            return _summonerRecent;
        }

        public LoLRealm GetRealmData(string region_)
        {
            bool _requestOK = false;
            string _response = null;
            LoLRealm _realm = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressRealm(region_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _realm = _jObject.ToObject<LoLRealm>();
                    }
                }
            }

            return _realm;
        }

        private LoLChampion ParseChampion(JObject champion_)
        {
            var _champion = champion_.ToObject<LoLChampion>();

            _champion.image.image_path = CDNVersionGroupImage(this._realm.cdn, this._realm.dd, _champion.image.group, _champion.image.full);
            _champion.passive.image.image_path = CDNVersionGroupImage(this._realm.cdn, this._realm.dd, _champion.passive.image.group, _champion.passive.image.full);

            foreach (var _spell in _champion.spells)
            {
                _spell.image.image_path = CDNVersionGroupImage(this._realm.cdn, this._realm.dd, _spell.image.group, _spell.image.full);
            }

            foreach (var _skin in _champion.skins)
            {
                _skin.imageLoading_path = CDNChampionLoading(this._realm.cdn, _champion.key, _skin.num);
                _skin.imageSplash_path = CDNChampionSplash(this._realm.cdn, _champion.key, _skin.num);

                if (_skin.num == 0)
                {
                    _champion.image.imageLoading_path = _skin.imageLoading_path;
                    _champion.image.imageSplash_path = _skin.imageSplash_path;
                }
            }

            return _champion;
        }

        private LoLItem ParseItem(JObject item_)
        {
            var _item = item_.ToObject<LoLItem>();

            _item.image.image_path = CDNVersionGroupImage(this._realm.cdn, this._realm.dd, _item.image.group, string.Concat(_item.id, ".png"));

            return _item;
        }

        public LoLChampion GetChampion(string region_, string championId_, bool cached = true)
        {
            /* Cache managing */
            int _index = this._championList.FindIndex(c => c.id == Convert.ToInt32(championId_));
            if (_index > -1 && cached)
                return (LoLChampion)this._championList[Convert.ToInt32(championId_)];

            bool _requestOK = false;
            string _response = null;
            LoLChampion _champion = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("champData", "all");
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressChampion(region_, championId_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _champion = ParseChampion(_jObject);
                    }
                }
            }

            /* Cache managing */
            if (_index < 0)
                this._championList.Add(_champion);
            else
                this._championList[_index] = _champion;

            return _champion;
        }

        public List<LoLChampion> GetChampionList(string region_, bool cached = true)
        {
            bool _requestOK = false;
            string _response = null;

            /* Cache Managing */
            if (this._championList.Count > 0 && cached)
                return this._championList;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("champData", "all");
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressChampionList(region_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        var _championListTemp = new List<LoLChampion>();

                        foreach (var _node in _jObject["data"])
                        {
                            _championListTemp.Add(ParseChampion(JObject.Parse(_node.Last.ToString())));
                        }

                        this._championList = _championListTemp;
                    }
                }
            }

            return this._championList;
        }

        public LoLItem GetItem(string region_, string itemId_, bool cached = true)
        {
            /* Cache managing */
            int _index = this._itemList.FindIndex(c => c.id == Convert.ToInt32(itemId_));
            if (_index > -1 && cached)
                return (LoLItem)_itemList[Convert.ToInt32(itemId_)];

            bool _requestOK = false;
            string _response = null;
            LoLItem _item = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("itemData", "all");
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressItem(region_, itemId_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        _item = ParseItem(_jObject);                        
                    }
                }
            }

            /* Cache managing */
            if (_index < 0)
                this._itemList.Add(_item);
            else
                this._itemList[_index] = _item;

            return _item;
        }

        public List<LoLItem> GetItemList(string region_, bool cached = true)
        {
            bool _requestOK = false;
            string _response = null;

            /* Cache Managing */
            if (this._itemList.Count > 0 && cached)
                return this._itemList;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("itemListData", "all");
                _factory.ParamList.Add("api_key", this.DevKey);
                _requestOK = _factory.ExecuteRequest(this.AddressItemList(region_), out _response, true);

                if (_requestOK)
                {
                    JObject _jObject = JObject.Parse(_response);

                    if (_jObject.Count > 0)
                    {
                        var _itemListTemp = new List<LoLItem>();

                        foreach (var _node in _jObject["data"])
                        {
                            _itemListTemp.Add(ParseItem(JObject.Parse(_node.Last.ToString())));
                        }

                        this._itemList = _itemListTemp;
                    }
                }
            }

            return this._itemList;
        }

    }
}
