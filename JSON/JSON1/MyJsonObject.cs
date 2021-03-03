using System.Text.Json.Serialization;

namespace JSON1 {

    public class MyJsonObject {
        public int Number { get; set; }
        public string Text { get; set; }
        public string[] ManyStrings { get; set; }
        public bool B { get; set; }
        
        [JsonIgnore]
        public string ThisIsIgnored { get; set; }
    }

}