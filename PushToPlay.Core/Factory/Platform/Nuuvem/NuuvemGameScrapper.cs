using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using PushToPlay.Model.Nuuvem;

namespace PushToPlay.Core.PlatformFactory.Nuuvem
{
    public class NuuvemGameScrapper
    {
        public const string NUUVEM_PROMO_LIST_KEY = "NuuvemPromoListKey";


        List<NuuvemGame> _promotionList;
        DateTime _lastUpdate;
        string _nuuvemUrlPromo = @"";


        #region Private Constructor

        private NuuvemGameScrapper()
        {
            _promotionList = new List<NuuvemGame>();
            _lastUpdate = DateTime.MinValue;
        }

        #endregion

        #region Singleton

        private static NuuvemGameScrapper _instance;

        public static NuuvemGameScrapper Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NuuvemGameScrapper();

                return _instance;
            }
        }

        #endregion

        public List<NuuvemGame> GetPromotionList()
        {
            string _urlRequest = string.Empty;
            TimeSpan _interval = DateTime.Now - _lastUpdate;

            if (_interval.Minutes >= 15 || _promotionList.Count == 0)
            {
                this._promotionList.Clear();

                try
                {
                    _nuuvemUrlPromo = ConfigurationManager.AppSettings[NUUVEM_PROMO_LIST_KEY];

                    int _times = 1;

                    do
                    {
                        if (!string.IsNullOrWhiteSpace(_nuuvemUrlPromo))
                        {
                            if (_times > 1)
                                _urlRequest = string.Concat(_nuuvemUrlPromo, "&page=", _times);
                            else
                                _urlRequest = _nuuvemUrlPromo;

                            HtmlAgilityPack.HtmlWeb _webGet = new HtmlAgilityPack.HtmlWeb();

                            var _htmlDocument = _webGet.Load(_urlRequest);

                            var _promoList = (from HtmlAgilityPack.HtmlNode _node in _htmlDocument.DocumentNode.Descendants()
                                              where _node.Name.ToUpper() == "DIV" &&
                                                    _node.Attributes["class"] != null &&
                                                    _node.Attributes["class"].Value == "info"
                                              select _node);

                            if (_webGet.ResponseUri.AbsoluteUri.Equals(_urlRequest))
                            {
                                if (_promoList != null &&
                                    _promoList.Count() > 0)
                                {
                                    string _gameLink = string.Empty;
                                    string _gameImage = string.Empty;
                                    string _gameTitle = string.Empty;
                                    string _price = string.Empty;
                                    string _discountPrice = string.Empty;

                                    foreach (HtmlAgilityPack.HtmlNode _node in _promoList)
                                    {
                                        var _nameLink = _node.Descendants().Where(d => d.Name.ToLower() == "a" && d.Attributes["class"] != null && d.Attributes["class"].Value.ToLower() == "name").FirstOrDefault();
                                        var _imageLink = _node.Descendants().Where(d => d.Name.ToLower() == "img").FirstOrDefault();
                                        var _priceLink = _node.ParentNode.ChildNodes.Where(c => c.Name.ToLower() == "div" && c.Attributes["class"] != null && c.Attributes["class"].Value.ToLower() == "purchase sale").FirstOrDefault();

                                        if (_nameLink != null)
                                        {
                                            _gameLink = _nameLink.Attributes["href"].Value;
                                            _gameTitle = _nameLink.InnerText;
                                        }

                                        if (_imageLink != null)
                                        {
                                            _gameImage = _imageLink.Attributes["src"].Value.Replace("mini/", string.Empty);
                                        }

                                        if (_priceLink != null)
                                        {
                                            string[] _prices = _priceLink.InnerText.Trim().Split('\n');
                                            _price = _prices[0].Trim();
                                            _discountPrice = _prices[1].Trim();
                                        }

                                        var _nuuvemGame = new NuuvemGame();

                                        _nuuvemGame.GameTitle = _gameTitle;
                                        _nuuvemGame.GameLink = _gameLink;
                                        _nuuvemGame.GameImage = _gameImage;
                                        _nuuvemGame.Price = _price;
                                        _nuuvemGame.DiscountPrice = _discountPrice;

                                        _promotionList.Add(_nuuvemGame);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                                _times++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    while (true);

                    _lastUpdate = DateTime.Now;
                }
                catch { }
            }

            return _promotionList;
        }
    }
}
