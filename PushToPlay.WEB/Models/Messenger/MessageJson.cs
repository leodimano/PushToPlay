using PushToPlay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PushToPlay.WEB.Models
{
    public class MessageJson
    {
        public int uId { get; set; }

        public string Text { get; set; }

        public string MessageId { get; set; }

        private int _replyToId;

        public bool Delete { get; set; }

        public int BaseType { get; set; }

        public int BaseId { get; set; }

        private MessageSourceTypeEnum BaseEnum { get; set; }

        public int TargetType { get; set; }
                
        public int TargetId { get; set; }

        private MessageSourceTypeEnum TargetEnum { get; set; }


        public void Initialize()
        {
            BaseEnum = (MessageSourceTypeEnum)BaseType;
            TargetEnum = (MessageSourceTypeEnum)TargetType;

            Int32.TryParse(MessageId, out _replyToId);
        }

        public void PostMessage()
        {
            ActivityMessage.CreateActivity(BaseId,
                                           BaseEnum,
                                           TargetId,
                                           TargetEnum,
                                           Text,
                                           MessageTypeEnum.Message,
                                           MessageActionEnum.Message,
                                           _replyToId);
        }

        public void DeleteMessage()
        {
            ActivityMessage.DeleteMessage(_replyToId);
        }
    }
}