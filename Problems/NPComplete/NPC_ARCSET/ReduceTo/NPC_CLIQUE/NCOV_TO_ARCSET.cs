using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE;

namespace API.Problems.NPComplete.NPC_ARCSET.ReduceTo.NPC_CLIQUE;

class SipserARCSET_CLIQUE_Reduction : IReduction<ARCSET, CLIQUE> {

    //ALEX NOTE: This class describes "SIPSERS" reduction and is not a generic reduction
    //Will need to change class name to "Foo Bar's Reduction" when implementing properly

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private ARCSET _reductionFrom;
    private CLIQUE _reductionTo;


    // --- Properties ---
    public string reductionDefinition {
        get {
            return _reductionDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public ARCSET reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public CLIQUE reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public SipserARCSET_CLIQUE_Reduction(ARCSET from, CLIQUE to) {
        _reductionFrom = from;
        _reductionTo = to;
    }
    public CLIQUE reduce() {
        return new CLIQUE();
    }
}
// return an instance of what you are reducing to