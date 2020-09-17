using System;

namespace ProtocolServer {
[Serializable]
public class TransferObj {
    public string Action { get; set; }
    public string Arg { get; set; }
}
}