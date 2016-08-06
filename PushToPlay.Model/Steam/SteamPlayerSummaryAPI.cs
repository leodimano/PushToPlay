using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.Steam
{
    public class SteamPlayerSummaryAPI
    {
        /// <summary>
        /// 64bit SteamID of the user
        /// </summary>
        public string SteamID { get; set; }

        /// <summary>
        /// This represents whether the profile is visible or not, and if it is visible, why you are allowed to see it. 
        /// Note that because this WebAPI does not use authentication, there are only two possible values returned: 
        /// 1 - the profile is not visible to you (Private, Friends Only, etc), 
        /// 3 - the profile is "Public", and the data is visible. 
        /// </summary>
        public int CommunityVisibilityState { get; set; }

        /// <summary>
        /// If set, indicates the user has a community profile configured (will be set to '1')
        /// </summary>
        public int ProfileState { get; set; }

        public string PersonaName { get; set; }

        /// <summary>
        /// The last time the user was online, in unix time.
        /// </summary>
        public int LastLogoff { get; set; }

        /// <summary>
        /// The full URL of the player's Steam Community profile.
        /// </summary>
        public string ProfileUrl { get; set; }

        /// <summary>
        /// The full URL of the player's 32x32px avatar. If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// The full URL of the player's 64x64px avatar. If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        public string AvatarMedium { get; set; }

        /// <summary>
        /// The full URL of the player's 184x184px avatar. If the user hasn't configured an avatar, this will be the default ? avatar.
        /// </summary>
        public string AvatarFull { get; set; }

        /// <summary>
        /// The user's current status.
        /// 0 - Offline
        /// 1 - Online
        /// 2 - Busy
        /// 3 - Away
        /// 4 - Snooze
        /// 5 - looking to trade
        /// 6 - looking to play.
        /// If the player's profile is private, this will always be "0", except is the user has set his status to looking to trade or looking to play, 
        /// because a bug makes those status appear even if the profile is private.
        /// </summary>
        public int PersonaState { get; set; }

        /// <summary>
        /// The player's "Real Name", if they have set it.
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// The player's primary group, as configured in their Steam Community profile.
        /// </summary>
        public string PrimaryClanID { get; set; }

        /// <summary>
        /// The time the player's account was created.
        /// </summary>
        public int TimeCreated { get; set; }

        /// <summary>
        /// If the user is currently in-game, this will be the name of the game they are playing. This may be the name of a non-Steam game shortcut.
        /// </summary>
        public string GameExtraInfo { get; set; }

        /// <summary>
        /// If the user is currently in-game, this value will be returned and set to the gameid of that game.
        /// </summary>
        public string GameID { get; set; }

        /// <summary>
        /// If set on the user's Steam Community profile, The user's country of residence, 2-character ISO country code
        /// </summary>
        public string LOCCountryCode { get; set; }

        /// <summary>
        /// If set on the user's Steam Community profile, The user's state of residence
        /// </summary>
        public string LOCStateCode { get; set; }

        /// <summary>
        /// An internal code indicating the user's city of residence. A future update will provide this data in a more useful way.
        /// </summary>
        public int LOCCityID { get; set; }
    }
}
