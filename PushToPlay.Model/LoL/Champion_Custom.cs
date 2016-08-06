using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PushToPlay.Model.LoL
{
    public partial class LoLChampionImage
    {
        public string image_path { get; set; }
        public string imageLoading_path { get; set; }
        public string imageSplash_path { get; set; }
    }

    public partial class LoLChampionImagePassive
    {
        public string image_path { get; set; }
    }

    public partial class LoLChampionImageSpell
    {
        public string image_path { get; set; }
    }

    public partial class LoLChampionSkin
    {
        public string imageLoading_path { get; set; }
        public string imageSplash_path { get; set; }
    }

    public class SpellRangeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null &&
                reader.Value.ToString().Equals("self"))
                return null;
            else
                return serializer.Deserialize<List<int>>(reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
