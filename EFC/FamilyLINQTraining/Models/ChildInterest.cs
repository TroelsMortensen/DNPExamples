using System.Text.Json.Serialization;

namespace Models {
public class ChildInterest {
    public int ChildId { get; set; }
    [JsonIgnore]
    public Child Child { get; set; }
    
    public string InterestId { get; set; }
    [JsonIgnore]
    public Interest Interest { get; set; }

    public override bool Equals(object? obj) {
        ChildInterest ci = ((ChildInterest) obj);
        if (ci.Child.Id==ChildId && ci.Interest.Type.Equals(InterestId)) return true;
        return base.Equals(obj);
    }
}
}