using PushToPlay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class MessengerViewModel
    {

        public ListObject<ActivityMessageProcessed> MessageList { get; set; }

        public int BaseId { get; set; }

        public MessageSourceTypeEnum BaseType { get; set; }

        public int TargetId { get; set; }

        public MessageSourceTypeEnum TargetType { get; set; }

        public int? PlatformId { get; set; }
    }
}