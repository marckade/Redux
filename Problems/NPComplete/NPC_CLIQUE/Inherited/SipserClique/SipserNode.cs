namespace API.Problems.NPComplete.NPC_CLIQUE.Inherited;

class SipserNode {

    // --- Fields ---
    private string _name = string.Empty;
    private string _cluster = string.Empty;

    // --- Properties ---
    public string name {
        get {
            return _name;
        }
        set {
            _name = value;
        }
    }
    public string cluster {
        get {
            return _cluster;
        }
        set {
            _cluster = value;
        }
    }

    // --- Methods Including Constructors ---
    public SipserNode() {

    }

    public SipserNode(string nodeName, string nodeCluster) {
        name = nodeName;
        cluster = nodeCluster;
    }
}