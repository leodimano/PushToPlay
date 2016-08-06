using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model
{
    public class ActivityMessageProcessed : ActivityMessage
    {
        public string BaseUserName { get; set; }
        public string BaseName { get; set; }
        public string TargetUserName { get; set; }
        public string TargetName { get; set; }
        public string BaseGameName { get; set; }
        public string TargetGameName { get; set; }
        public Nullable<int> BasePlatformId { get; set; }
        public Nullable<int> TargetPlatformId { get; set; }
        public string BaseGroupName { get; set; }
        public string TargetGroupName { get; set; }
    }
}
