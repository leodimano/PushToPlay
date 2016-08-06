using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Web;
using System.Configuration;
using System.Web.Mvc;


namespace PushToPlay.Helpers
{
    public static class NetHelper
    {
        public static bool DownloadPicture(string url_, string pathFileName_)
        {
            using (System.Net.WebClient _client = new System.Net.WebClient())
            {
                try
                {
                    _client.DownloadFile(url_, pathFileName_);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public static string GetIdFileName(int id)
        {
            return string.Concat(id, ".jpg");
        }

        public static bool SaveFile(Stream stream_, string fileName_, string saveFilePath_)
        {
            fileName_ = Path.Combine(saveFilePath_, fileName_);

            if (stream_ != null)
            {
                using (FileStream _fileStream = new FileStream(fileName_, FileMode.Create, FileAccess.Write))
                {
                    stream_.CopyTo(_fileStream);
                }

                return true;
            }
            else
                return false;
        }

        public static int GetPageSize()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
        }

        public static string ProcessGameUrlName(string url_)
        {
            if (!string.IsNullOrWhiteSpace(url_))
            {
                return url_.Replace(".", string.Empty).
                            Replace(":", string.Empty).
                            Replace("/", string.Empty).
                            Replace("'", string.Empty).
                            Replace("&", string.Empty).
                            Replace("*", string.Empty).
                            Replace("¿", string.Empty).
                            Replace("?", string.Empty).
                            Replace("-", string.Empty).
                            Replace("@", string.Empty).
                            Replace("(", string.Empty).
                            Replace(")", string.Empty).
                            Replace(" ", "-").
                            Replace("--", "-").TrimStart('-').Trim('_').ToLower();
            }
            else
            {
                return string.Empty;
            }
        }

        #region Style

        public static string GetCSSStrongTextGame(int platformId_)
        {
            switch (platformId_)
            {
                case 1: // MAC
                case 9: // LINUX
                case 5: // PC
                    return "fieldTextStrong";
                case 2: // XBOX 360                    
                case 7: // XBOX ONE
                    return "xboxFieldTextStrong";
                case 3: // PLAYSTATION 3
                case 8: // PLAYSTATION 4                    
                    return "psnFieldTextStrong";
                case 4: // WII
                case 6: // WII U
                    return "fieldTextLight";
                default:
                    return "fieldTextStrong";
            }
        }

        public static string GetCSSNormalTextGame(int platformId_)
        {
            switch (platformId_)
            {
                case 1: // MAC
                case 9: // LINUX
                case 5: // PC
                    return "fieldText";
                case 2: // XBOX 360                    
                case 7: // XBOX ONE
                    return "xboxFieldText";
                case 3: // PLAYSTATION 3
                case 8: // PLAYSTATION 4                    
                    return "psnFieldText";
                case 4: // WII
                case 6: // WII U
                    return "fieldTextLight";
                default:
                    return "fieldText";
            }
        }

        public static string GetCSSBGGame(int platformId_)
        {
            switch (platformId_)
            {
                case 1: // MAC
                case 9: // LINUX
                case 5: // PC
                    return "BGColor";
                case 2: // XBOX 360                    
                case 7: // XBOX ONE
                    return "xboxBGColor";
                case 3: // PLAYSTATION 3
                case 8: // PLAYSTATION 4                    
                    return "psnBGColor";
                case 4: // WII
                case 6: // WII U
                    return "BGColorLight";
                default:
                    return "BGColor";
            }
        }

        public static string GetCSSLinkGame(int platformId_)
        {
            switch (platformId_)
            {
                case 1: // MAC
                case 9: // LINUX
                case 5: // PC
                    return "linkItem";
                case 2: // XBOX 360                    
                case 7: // XBOX ONE
                    return "xboxLinkColor";
                case 3: // PLAYSTATION 3
                case 8: // PLAYSTATION 4                    
                    return "psnLinkColor";
                case 4: // WII
                case 6: // WII U
                    return "lightLinkColor";
                default:
                    return "linkItem";
            }
        }

        #endregion

        #region Steam Web Helper

        public static string GetSteamAppUrl(string steamAppId_, string steamAppName)
        {
            string _steamAppUrl = string.Format(ConfigurationManager.AppSettings["SteamCommunityURL"], steamAppId_);

            if (steamAppName.ToUpper().EndsWith("Collection") ||
                steamAppName.ToUpper().EndsWith("Pack"))
                _steamAppUrl = _steamAppUrl.Replace("/app/", "/sub/");

            return _steamAppUrl;
        }

        #endregion 
    }
}
