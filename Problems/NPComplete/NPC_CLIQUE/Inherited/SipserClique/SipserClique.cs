using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;
using API.Problems.NPComplete.NPC_CLIQUE;
using API.Problems.NPComplete.NPC_CLIQUE.Inherited;

namespace API.Problems.NPComplete.NPC_CLIQUE.Inherited;

class SipserClique : CLIQUE {

    // --- Fields ---

    // Adding cluster field to class
    private List<SipserNode> _clusterNodes = new List<SipserNode>();

    // --- Properties ---
    public List<SipserNode> clusterNodes {
        get {
            return _clusterNodes;
        }
        set {
            _clusterNodes = value;
        }
    }
}