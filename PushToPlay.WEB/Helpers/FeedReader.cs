using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Xml;
using System.Configuration;
using PushToPlay.WEB.Models;
using PushToPlay.Model.Steam;
using PushToPlay.Model.Nuuvem;
using PushToPlay.Core.PlatformFactory.Nuuvem;
using PushToPlay.Core.PlatformFactory.Steam;
using System.ServiceModel.Syndication;

namespace PushToPlay.WEB.Helper
{
    public class FeedReader
    {
        #region Game News Feed
        private static List<string> _gameFeedUrlList = new List<string>();

        /// <summary>
        /// TODO - Implementar armazenamento das listas em cache
        /// </summary>
        private static void GetGameNewsConfiguredFeeds()
        {
            string[] _configuredFeeds = ConfigurationManager.AppSettings[Constants.CONFIG_KEY_RSS_FEED_GAME].Split(',');

            _gameFeedUrlList.Clear();

            for (int i = 0; i <= _configuredFeeds.Count() - 1; i++)
            {
                try
                {
                    _gameFeedUrlList.Add(ConfigurationManager.AppSettings[_configuredFeeds[i]]);
                }
                catch { }
            }
        }

        public static RssFeedModel GetGameNewsFeeds()
        {
            GetGameNewsConfiguredFeeds();

            RssFeedModel _feedModel = new RssFeedModel();

            foreach (string feedUrl in _gameFeedUrlList)
            {
                try
                {
                    XmlReader _xmlFeed = XmlReader.Create(feedUrl);

                    SyndicationFeed _feed = SyndicationFeed.Load(_xmlFeed);

                    var _updateFeed = _feed.Items.Where(i => !i.Title.Text.ToUpper().StartsWith("FORUM:") && !i.Title.Text.ToUpper().StartsWith("FÓRUM:") &&
                                                             !i.Title.Text.ToUpper().StartsWith("[JOGOS ONLINE]") &&
                                                             !i.Title.Text.ToUpper().StartsWith("PAPO DA REDAÇÃO:") &&
                                                             !i.Title.Text.ToUpper().StartsWith("GUIA DE COMPRAS:") &&
                                                             !i.Title.Text.ToUpper().Contains("POWER UP!") &&
                                                             !i.Title.Text.ToUpper().StartsWith("UOL JOGOS MOBILE:"));

                    if (_updateFeed.Count() > 0)
                    {
                        RssFeedModelItem _feedModelItem = null;
                        _feedModel.FeedTitle = "Últimas notícias";
                        _feedModel.FeedHeight = "360px";
                        _feedModel.FeedType = FeedType.NewsFeed;

                        foreach (SyndicationItem _feedItem in _updateFeed)
                        {
                            _feedModelItem = new RssFeedModelItem();

                            /* Check if there is 2 descriptions  */
                            if (_feedItem.Summary != null &&
                                !string.IsNullOrWhiteSpace(_feedItem.Summary.Text))
                            {
                                _feedModelItem.Title = System.Text.RegularExpressions.Regex.Replace(_feedItem.Summary.Text, "<.*?>", string.Empty);
                                _feedModelItem.Title = _feedModelItem.Title.Trim();
                            }
                            else
                            {
                                _feedModelItem.Title = _feedItem.Title.Text;
                            }

                            _feedModelItem.Link = _feedItem.Links[0].Uri.ToString();
                            _feedModelItem.PublishDate = _feedItem.PublishDate.Date;
                            _feedModel.FeedItems.Add(_feedModelItem);
                        }
                    }
                }
                catch
                { }
            }

            if (_feedModel.FeedItems.Count > 0)
            {
                var _orderedList = from RssFeedModelItem _item in _feedModel.FeedItems
                                   orderby _item.PublishDate descending
                                   select _item;

                _feedModel.FeedItems = _orderedList.ToList();
            }

            return _feedModel;
        }

        #endregion

        #region Promocao Diaria Steam

        /// <summary>
        /// TODO - Implementar codigo defensivo
        /// </summary>
        /// <returns></returns>
        public static RssFeedModel GetSteamPromoFeeds()
        {
            RssFeedModel _feedModel = null;
            SteamPromotionApiResponse _steamPromoResponse = null;

            using (SteamFactory _steamFactory = new SteamFactory())
            {
                _steamPromoResponse = _steamFactory.GetSteamPromotions();
            }

            if (_steamPromoResponse != null &&
                _steamPromoResponse.status == 1)
            {
                _feedModel = new RssFeedModel();

                _feedModel.FeedType = FeedType.SteamPromoFeed;
                _feedModel.FeedHeight = "360px";
                _feedModel.FeedTitle = "Promoções do Steam";

                RssFeedModelItem _feedModelItem = null;

                if (_steamPromoResponse.specials != null &&
                    _steamPromoResponse.specials.items != null)
                {
                    foreach (Item _promo in _steamPromoResponse.specials.items)
                    {
                        var _originalPrice = (double)_promo.original_price / 100;
                        var _finalPrice = (double)_promo.final_price / 100;

                        _feedModelItem = new RssFeedModelItem();
                        _feedModelItem.ImagePath = _promo.small_capsule_image;
                        _feedModelItem.Title = _promo.name;
                        _feedModelItem.Link = PushToPlay.Helpers.NetHelper.GetSteamAppUrl(Convert.ToString(_promo.id), _promo.name);
                        _feedModelItem.Description = string.Format("-{0}%", _promo.discount_percent.ToString());
                        _feedModelItem.DiscountPrice = _finalPrice.ToString("C2").Replace(".", ",");
                        _feedModelItem.Price = _originalPrice.ToString("C2").Replace(".", ",");
                        _feedModelItem.PublishDate = DateTime.Now;
                        _feedModel.FeedItems.Add(_feedModelItem);
                    }
                }
            }

            return _feedModel;
        }

        public static RssFeedModel GetNuuvemPromoFeeds()
        {
            RssFeedModel _feedModel = null;

            var _nuuvemPromoList = NuuvemGameScrapper.Instance.GetPromotionList();

            if (_nuuvemPromoList.Count > 0)
            {
                _feedModel = new RssFeedModel();

                _feedModel.FeedType = FeedType.NuuvemPromoFeed;
                _feedModel.FeedHeight = "360px;";
                _feedModel.FeedTitle = "Promoções do Nuuvem";

                double _dPrice = 0;
                double _dDiscount = 0;

                RssFeedModelItem _item = null;

                foreach (var _nuuvem in _nuuvemPromoList)
                {
                    _item = new RssFeedModelItem();
                    _item.ImagePath = _nuuvem.GameImage;
                    _item.Title = _nuuvem.GameTitle;
                    _item.Link = _nuuvem.GameLink;


                    if (!string.IsNullOrWhiteSpace(_nuuvem.Price))
                    {
                        _dPrice = Convert.ToDouble(_nuuvem.Price.Replace(".", string.Empty).Replace(",", string.Empty).Replace("R$", string.Empty).Replace(" ", string.Empty)) / 100;
                    }

                    if (!string.IsNullOrWhiteSpace(_nuuvem.DiscountPrice))
                    {
                        _dDiscount = Convert.ToDouble(_nuuvem.DiscountPrice.Replace(".", string.Empty).Replace(",", string.Empty).Replace("R$", string.Empty).Replace(" ", string.Empty)) / 100;
                    }

                    var _diference = _dPrice - _dDiscount;

                    _diference = Math.Truncate((_diference * 100) / _dPrice);

                    _item.Description = string.Format("-{0}%", _diference);

                    _item.Price = _nuuvem.Price;
                    _item.DiscountPrice = _nuuvem.DiscountPrice;

                    _item.PublishDate = DateTime.Now;
                    _feedModel.FeedItems.Add(_item);
                }
            }

            return _feedModel;
        }

        #endregion
    }
}