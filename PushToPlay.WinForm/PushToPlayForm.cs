using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using PushToPlay.Core.PlatformFactory;
using PushToPlay.Core.PlatformFactory.Steam;
using PushToPlay.Core.PlatformFactory.GiantBomb;
using SteamKit2;
using System.IO;
using System.Configuration;
using PushToPlay.Model.Steam;
using PushToPlay.Core.PlatformFactory.Twitch;
using PushToPlay.Core.PlatformFactory.LoL;

namespace PushToPlay.WinForm
{
    public partial class PushToPlayForm : Form
    {
        bool isRunning = false;

        SteamFactory _steamFactory = null;
        GiantBombFactory _giantBombFactory = null;
        string _authCode;

        public PushToPlayForm()
        {
            InitializeComponent();

            txtAuthCode.Enabled = false;
            _steamFactory = new SteamFactory();
            _giantBombFactory = new GiantBombFactory();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        public void ConnectCallBack(SteamClient.ConnectedCallback callBack_)
        {
            byte[] sentryHash = null;
            if (File.Exists("sentry.bin"))
            {
                // if we have a saved sentry file, read and sha-1 hash it
                byte[] sentryFile = File.ReadAllBytes("sentry.bin");
                sentryHash = CryptoHelper.SHAHash(sentryFile);
            }

            if (!string.IsNullOrEmpty(txtAuthCode.Text)){
                _authCode = txtAuthCode.Text;
                txtAuthCode.Text = string.Empty;
            }

            _steamFactory.SteamClientLogIn(new SteamUser.LogOnDetails()
            {
                Username = ConfigurationManager.AppSettings[SteamFactory.STEAM_USERNAME],
                Password = ConfigurationManager.AppSettings[SteamFactory.STEAM_PASSWORD],
                AuthCode = _authCode,
                SentryFileHash = sentryHash
            });
        }

        public void DisconnectCallBack(SteamClient.DisconnectedCallback callBack_)
        {
            this.isRunning = false;
        }

        public void LoggedOnCallBack(SteamUser.LoggedOnCallback callBack_)
        {
            if (callBack_.Result == EResult.AccountLogonDenied)
            {
                txtAuthCode.Enabled = true;
                return;
            }

            if (callBack_.Result == EResult.OK)
            {
                /* Rotina */    
            }
        }

        public void GetAppInfoCallBack(SteamApps.AppInfoCallback appIndoCallBack_, JobID jObid_)
        {
            if (appIndoCallBack_.Apps != null)
            {
            }
        }

        public void LoggedOffCallBack(SteamUser.LoggedOffCallback callBack_)
        {
            if (callBack_.Result == EResult.OK)
            {
            }
        }

        public void MachineAuthCallBack(SteamUser.UpdateMachineAuthCallback callBack_, JobID jobId_)
        {
            byte[] sentryHash = CryptoHelper.SHAHash(callBack_.Data);

            // write out our sentry file
            // ideally we'd want to write to the filename specified in the callback
            // but then this sample would require more code to find the correct sentry file to read during logon
            // for the sake of simplicity, we'll just use "sentry.bin"
            File.WriteAllBytes("sentry.bin", callBack_.Data);

            // inform the steam servers that we're accepting this sentry file
            _steamFactory.SteamClientSendMachineAuth(new SteamUser.MachineAuthDetails
            {
                JobID = jobId_,

                FileName = callBack_.FileName,

                BytesWritten = callBack_.BytesToWrite,
                FileSize = callBack_.Data.Length,
                Offset = callBack_.Offset,

                Result = EResult.OK,
                LastError = 0,

                OneTimePassword = callBack_.OneTimePassword,

                SentryFileHash = sentryHash,
            });
        }

        private void timerRunCallBack_Tick(object sender, EventArgs e)
        {
            if (_steamFactory != null)
            {
                _steamFactory.SteamClientRunWaitCallBack();
            }
        }

        private void timerCheckForUpdate_Tick(object sender, EventArgs e)
        {
            List<SteamAppAPI> _listApp = _steamFactory.GetCompactAppListAPI();

            /* Recupera a lista de Applicacoes */
            foreach (SteamAppAPI _app in _listApp)
            {
                _steamFactory.GetAppDetailAPI(_app.AppID);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (PushToPlay.Core.PushToPlayFactory _factory = new Core.PushToPlayFactory())
            {
                _factory.InitializeDBGenres();
                _factory.InitializeDBPlatform();
                _factory.InitializeDBGames(txtPlatform.Text);
                _factory.InitializeDBSteamGames();
                _factory.DeleteUnusedImage();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (PushToPlay.Core.PushToPlayFactory _factory = new Core.PushToPlayFactory())
            {
                _factory.InitializeDBSteamGames();
                _factory.DeleteUnusedImage();
            }
        }

        private void btnImportSteamPlayer_Click(object sender, EventArgs e)
        {
            using (PushToPlay.Core.PushToPlayFactory _factory = new Core.PushToPlayFactory())
            {
                _factory.ImportSteamProfile(txtSteamId.Text);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPlatform_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTwitchChannel_Click(object sender, EventArgs e)
        {
            using (TwitchFactory _factory = new TwitchFactory())
            {

            }
        }

        private void btnStreamByGame_Click(object sender, EventArgs e)
        {
            using (TwitchFactory _factory = new TwitchFactory())
            {
                _factory.GetStreamListByGame(txtTwitchGameStream.Text);
            }
        }

        private void btnSummonerByName_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetSummonerByName(txtLoLRegion.Text, txtLoLSummonerName.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetSummonerStats(txtLoLRegion.Text, txtLoLSummonerID.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetSummonerRecent(txtLoLRegion.Text, txtLoLSummonerID.Text);
        }

        private void btnChampionbyId_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetChampion(txtLoLRegion.Text, txtLoLChampionId.Text, false);
        }

        private void btnLoLChampionList_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            var championTable = LoLFactory.Instance.GetChampionList(txtLoLRegion.Text, false);
        }

        private void btnLoLItemList_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetItemList(txtLoLRegion.Text, false);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoLFactory.Instance.InitializeLoLFactory(txtLoLRegion.Text);
            LoLFactory.Instance.GetItem(txtLoLRegion.Text, txtLoLItemId.Text, false);
        }
    }
}
