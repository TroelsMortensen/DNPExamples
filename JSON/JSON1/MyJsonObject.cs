using System.Text.Json.Serialization;

namespace JSON1 {

    public class MyJsonObject {
        public int number { get; set; }
        public string text { get; set; }
        public string[] manyStrings { get; set; }
        public bool b { get; set; }
        
        [JsonIgnore]
        public string thisIsIgnored { get; set; }
    }

}