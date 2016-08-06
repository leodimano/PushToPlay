using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DotNetOpenAuth.AspNet.Clients;
using System.Web;
using System.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PushToPlay.Model.Steam;
using PushToPlay.Model.Steam.SteamPlayerGame;
using SteamKit2;

namespace PushToPlay.Core.PlatformFactory.Steam
{
    public class SteamFactory : IDisposable
    {
        public const string STEAM_OPENID = "Steam";

        public const string STEAM_COMMUNITY_URL_KEY = "SteamCommunityURL";
        public const string STEAM_OPENID_URL_KEY = "SteamOpenIDUrl";
        public const string STEAM_OPENID_RETURN_URL = "SteamOpenIDReturnURL";
        public const string STEAM_ID_RETURN_PARAMETER = "SteamIDReturnParameter";
        public const string STEAM_DEV_KEY = "SteamAppKey";
        public const string STEAM_ID_TESTE = "SteamID_TESTE";
        public const string STEAM_USERNAME = "SteamUser";
        public const string STEAM_PASSWORD = "SteamPass";

        public const string MSG_STEAM_CLIENT_INSTANCE = "SteamClient deve ser inicializado.";
        public const string MSG_STEAM_CLIENT_CONNECTION_REQUIRED = "SteamCliente deve estar conectado para executar essa operação.";

        const int STEAM_CALLBACK_TIMEOUT = 1;

        private string _steamID;
        private SteamUser _steamUser;
        public SteamClient _steamClient;
        private SteamApps _steamApps;
        private CallbackManager _steamCallBackManager;

        public bool IsSteamConnected { get { return _steamClient == null ? false : _steamClient.IsConnected; } }
        public bool IsSteamLoggedIn { get; set; }
        
        public Action<SteamClient.ConnectedCallback> ConnectedCallBack;
        public Action<SteamClient.DisconnectedCallback> DisconnectedCallBack;

        public Action<SteamUser.LoggedOnCallback> LoggedOnCallBack;
        public Action<SteamUser.LoggedOffCallback> LoggedOffCallBack;
        public Action<SteamUser.UpdateMachineAuthCallback, JobID> UpdateMachineAuth;
        public Action<SteamApps.AppInfoCallback, JobID> AppInfoCallBack;
        public Action<SteamClient.BaseJobCallback, JobID> JobProcessCallBack;

        #region SteamAPI

        public string SteamID
        {
            get { return this._steamID; }
        }
   
        public SteamFactory()
        {
            this._steamID = SteamIDTeste;
        }

        public SteamFactory(string steamID_)
        {
            this._steamID = SteamIDTeste;
        }

        private string DevKey
        {
            get
            {
                return string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[STEAM_DEV_KEY]) ? string.Empty : ConfigurationManager.AppSettings[STEAM_DEV_KEY];
            }
        }

        public static string SteamIDTeste
        {
            get
            {
                return string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings[STEAM_ID_TESTE]) ? string.Empty : ConfigurationManager.AppSettings[STEAM_ID_TESTE];                    
            }
        }

        public string GetSteamID(string steamUrl_)
        {
            string _steamID = string.Empty;
            bool foundNumber = false;

            for (int i = steamUrl_.Length - 1; i >= 0; i--)
            {
                int digit;

                if (Int32.TryParse(steamUrl_[i].ToString(), out digit))
                {
                    foundNumber = true;
                    _steamID = string.Concat(steamUrl_[i], _steamID);
                }
                else
                {
                    if (foundNumber)
                        return _steamID;
                }
            }

            return _steamID;
        }

        public void Authenticate(HttpContextBase httpContext_)
        {
            OpenIdClient _openIdClient = new OpenIdClient(STEAM_OPENID, ConfigurationManager.AppSettings[STEAM_OPENID_URL_KEY]);

            _openIdClient.RequestAuthentication(httpContext_, new Uri(ConfigurationManager.AppSettings[STEAM_OPENID_RETURN_URL]));
        }

        public string HandleSteamOpenIDReturn(HttpContextBase httpContext_)
        {
            string _steamIDParameter = ConfigurationManager.AppSettings[STEAM_ID_RETURN_PARAMETER];

            if (!string.IsNullOrWhiteSpace(httpContext_.Request.QueryString[_steamIDParameter]))
            {
                using (SteamFactory _steamFactory = new SteamFactory())
                {
                    return _steamFactory.GetSteamID(httpContext_.Request.QueryString[_steamIDParameter]);
                }                
            }

            return string.Empty;
        }

        public SteamPlayerSummaryAPI  GetPlayerSummaryAPI(string steamId_)
        {            
            bool _requestOK = false;
            string _response = null;            
            string _urlRequest = "api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";

            SteamPlayerSummaryAPI _playerSummary = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("key", this.DevKey);

                if (!string.IsNullOrWhiteSpace(steamId_))
                    _factory.ParamList.Add("steamids", steamId_);
                else
                    _factory.ParamList.Add("steamids", this._steamID);

                _requestOK = _factory.ExecuteRequest(_urlRequest, out _response);
            }

            if (_requestOK)
            {
                JObject _jObject = JObject.Parse(_response);

                if ( _jObject["response"]["players"].HasValues)
                    _playerSummary = _jObject["response"]["players"].First().ToObject<SteamPlayerSummaryAPI>();
            }
            else
            {
            }

            return _playerSummary;
        }

        public List<SteamAppAPI> GetCompactAppListAPI()
        {
            bool _requestOK = false;
            string _response = string.Empty;
            string _urlRequest = "api.steampowered.com/ISteamApps/GetAppList/v0002";
            
            List<SteamAppAPI> _appList = new List<SteamAppAPI>();

            using (RequestFactory _request = new RequestFactory())
            {
                _requestOK = _request.ExecuteRequest(_urlRequest, out _response);
            }

            if (_requestOK)
            {
                JObject _jObject = JObject.Parse(_response);

                if (_jObject["applist"]["apps"].HasValues)
                {
                    foreach (JObject _app in _jObject["applist"]["apps"])
                    {
                        SteamAppAPI _steamApp = _app.ToObject<SteamAppAPI>();
                        _appList.Add(_steamApp);
                    }
                }
            }

            return _appList;
        }

        public long GetNumberOfCurrentPlayersAPI(long appId_)
        {
            long _count = 0;
            bool _requestOK = false;
            string _response = string.Empty;

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("appid", appId_);
                _requestOK = _request.ExecuteRequest("api.steampowered.com/ISteamUserStats/GetNumberOfCurrentPlayers/v1/", out _response);

                if (_requestOK)
                {
                    _count = (int)JObject.Parse(_response)["response"]["player_count"];
                }
            }

            return _count;
        }

        public SteamAppDetailAPI GetAppDetailAPI(long appId_)
        {
            bool _requestOK = false;
            string _response = string.Empty;
            string _urlRequest = "store.steampowered.com/api/appdetails/";
            SteamAppDetailAPI _steamApp = null;

            using (RequestFactory _request = new RequestFactory())
            {
                _request.ParamList.Add("appids", appId_);
                _request.ParamList.Add("l", "portuguese");
                _requestOK = _request.ExecuteRequest(_urlRequest, out _response);
            }

            if (_requestOK)
            {
                JObject _jObject = JObject.Parse(_response);
                try
                {                   
                    if ((bool)_jObject.First.First["success"])
                    {
                        _steamApp = _jObject.First.First.Last.First.ToObject<SteamAppDetailAPI>();
                    }
                }
                catch (Exception ex) {
                    SteamFactoryDebugLog.WriteLine(DateTime.Now.ToString());
                    SteamFactoryDebugLog.WriteLine(ex.Message);
                    SteamFactoryDebugLog.WriteLine(_response);
                    SteamFactoryDebugLog.WriteLine(string.Empty);
                    SteamFactoryDebugLog.WriteLine(string.Empty);
                }                   
            }

            return _steamApp;
        }

        public SteamPlayerGameResponse GetPlayerGames(string steamId_)
        {
            bool _requestOK = false;
            string _response = null;
            string _urlRequest = "api.steampowered.com/IPlayerService/GetOwnedGames/v0001/";

            SteamPlayerGameResponse _playerGames = null;

            using (RequestFactory _factory = new RequestFactory())
            {
                _factory.ParamList.Add("key", this.DevKey);

                if (!string.IsNullOrWhiteSpace(steamId_))
                    _factory.ParamList.Add("steamid", steamId_);
                else
                    _factory.ParamList.Add("steamid", this._steamID);

                _requestOK = _factory.ExecuteRequest(_urlRequest, out _response);
            }

            if (_requestOK)
            {
                JObject _jObject = JObject.Parse(_response);
                _playerGames = _jObject.ToObject<SteamPlayerGameResponse>();
            }

            return _playerGames;
        }

        public SteamPromotionApiResponse GetSteamPromotions()
        {
            bool _requestOK = false;
            string _response = null;
            string _urlRequest = "store.steampowered.com/api/featuredcategories/?cc=BR&l=portuguese&v=1&trailer=1";

            SteamPromotionApiResponse _steamPromotions = null;

            try
            {
                using (RequestFactory _factory = new RequestFactory())
                {
                    _requestOK = _factory.ExecuteRequest(_urlRequest, out _response);
                }

                if (_requestOK)
                {
                    JObject _jobject = JObject.Parse(_response);
                    _steamPromotions = _jobject.ToObject<SteamPromotionApiResponse>();
                }
            }
            catch { }

            return _steamPromotions;
        }


        #endregion

        #region SteamKit2

        #region SteamClient Operations

        public void InitializeSteamClient()
        {
            _steamClient = new SteamClient( System.Net.Sockets.ProtocolType.Tcp );
            //DebugLog.Enabled = true;
            //DebugLog.AddListener(new SteamFactoryDebugLog());
            _steamCallBackManager = new CallbackManager(_steamClient);
            _steamUser = _steamClient.GetHandler<SteamUser>();
            _steamApps = _steamClient.GetHandler<SteamApps>();

            if (ConnectedCallBack == null)
                new Callback<SteamClient.ConnectedCallback>(OnConnected, _steamCallBackManager);
            else
                new Callback<SteamClient.ConnectedCallback>(ConnectedCallBack, _steamCallBackManager);

            if (DisconnectedCallBack == null)
                new Callback<SteamClient.DisconnectedCallback>(OnDisconnected, _steamCallBackManager);
            else
                new Callback<SteamClient.DisconnectedCallback>(DisconnectedCallBack, _steamCallBackManager);

            if (LoggedOnCallBack == null)
                new Callback<SteamUser.LoggedOnCallback>(OnLoggedOn, _steamCallBackManager);
            else
                new Callback<SteamUser.LoggedOnCallback>(LoggedOnCallBack, _steamCallBackManager);

            if (LoggedOffCallBack == null)
                new Callback<SteamUser.LoggedOffCallback>(OnLoggedOff, _steamCallBackManager);
            else
                new Callback<SteamUser.LoggedOffCallback>(LoggedOffCallBack, _steamCallBackManager);

            if (AppInfoCallBack == null)
                new JobCallback<SteamApps.AppInfoCallback>(OnApiInfo, _steamCallBackManager);
            else
                new JobCallback<SteamApps.AppInfoCallback>(AppInfoCallBack, _steamCallBackManager);

            if (UpdateMachineAuth != null)
                new JobCallback<SteamUser.UpdateMachineAuthCallback>(UpdateMachineAuth, _steamCallBackManager);

            if (JobProcessCallBack != null)
                new JobCallback<SteamClient.BaseJobCallback>(JobProcessCallBack, _steamCallBackManager);
        }

        public void SteamClientConnect()
        {
            if (_steamClient != null)
            {
                _steamClient.Connect();
                _steamCallBackManager.RunWaitCallbacks(TimeSpan.FromSeconds(STEAM_CALLBACK_TIMEOUT));
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientLogin()
        {
            this.SteamClientLogIn(new SteamUser.LogOnDetails()
                        {
                            Username = ConfigurationManager.AppSettings[STEAM_USERNAME],
                            Password = ConfigurationManager.AppSettings[STEAM_PASSWORD]
                        });
        }

        public void SteamClientLogIn(SteamUser.LogOnDetails _logOnDetails)
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    _steamUser.LogOn(_logOnDetails);

                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientLogInAnonymous()
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    _steamUser.LogOnAnonymous();

                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientLogOff()
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    _steamUser.LogOff();


                    // Aparentement o CallBack de LogOff nao funciona
                    this.IsSteamLoggedIn = false;
                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientDisconnect()
        {
            if (_steamClient != null)
            {
                if (_steamClient.IsConnected)
                {
                    _steamClient.Disconnect();

                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientAppInfo(uint appId_)
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    _steamApps.GetAppInfo(new SteamApps.AppDetails() { AppID = appId_ });
                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientSendMachineAuth(SteamUser.MachineAuthDetails machineAuthDetails_)
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    _steamUser.SendMachineAuthResponse(machineAuthDetails_);

                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamClientRunWaitCallBack()
        {
            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    //_steamCallBackManager.RunWaitCallbacks(TimeSpan.FromSeconds(STEAM_CALLBACK_TIMEOUT));
                    _steamCallBackManager.RunCallbacks();
                }
                else
                {
                    throw new Exception(MSG_STEAM_CLIENT_CONNECTION_REQUIRED);
                }
            }
            else
            {
                throw new Exception(MSG_STEAM_CLIENT_INSTANCE);
            }
        }

        public void SteamCallJobById()
        {
            SteamUnifiedMessages _steamMessage = _steamClient.GetHandler<SteamUnifiedMessages>();
            _steamMessage.SendMessage<SteamKit2.Internal.CMsgClientGetClientAppList>("teste", new SteamKit2.Internal.CMsgClientGetClientAppList() { games = true, media = false, only_changing = false, only_installed = false, tools = false });
        }

        #endregion 

        #region Steam Handlers

        private void OnConnected(SteamClient.ConnectedCallback callBack_)
        {
            if (callBack_.Result == EResult.OK)
            {
            }
            else
            {

            }
        }

        private void OnDisconnected(SteamClient.DisconnectedCallback callBack_)
        {
            if (_steamClient.IsConnected)
            {
            }
        }

        private void OnLoggedOn(SteamUser.LoggedOnCallback callBack_)
        {
            if (callBack_.Result == EResult.OK)
            {
                this.IsSteamLoggedIn = true;
            }
            else
            {
                this.IsSteamLoggedIn = false;
            }
        }

        private void OnLoggedOff(SteamUser.LoggedOffCallback callBack_)
        {
            this.IsSteamLoggedIn = false;
        }

        private void OnApiInfo(SteamApps.AppInfoCallback callBack_, JobID jObjId_)
        {
            return;
        }

        #endregion

        #endregion

        public void Dispose()
        {
            _steamID = null;

            if (_steamClient != null)
            {
                if (this.IsSteamConnected)
                {
                    if (this.IsSteamLoggedIn)
                    {
                        this.SteamClientLogOff();
                    }

                    this.SteamClientDisconnect();
                }

                _steamClient = null;
                _steamUser = null;
                _steamCallBackManager = null;
            }
        }
    }

    public class SteamFactoryDebugLog
    {
        public static void WriteLine(string msg)
        {
            try
            {
                System.IO.StreamWriter _writer = new System.IO.StreamWriter("SteamClient.Log", true);
                _writer.WriteLine(msg);
                _writer.Flush();
                _writer.Close();
                _writer.Dispose();
            }
            catch { }
        }
    }

}
