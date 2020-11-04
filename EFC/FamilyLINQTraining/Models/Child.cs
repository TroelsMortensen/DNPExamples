using System.Collections.Generic;

namespace Models {
public class Child : Person {
    
    public List<ChildInterest> ChildInterests { get; set; }
    public List<Pet> Pets { get; set; }
}
}