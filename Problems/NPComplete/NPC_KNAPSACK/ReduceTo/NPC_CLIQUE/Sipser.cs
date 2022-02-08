
using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE;

namespace API.Problems.NPComplete.NPC_KNAPSACK.ReduceTo.NPC_CLIQUE;

class SipserKNAPSACK_CLIQUE_Reduction : IReduction<KNAPSACK, CLIQUE> {

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private KNAPSACK _reductionFrom;
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
    public KNAPSACK reductionFrom {
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
    public SipserKNAPSACK_CLIQUE_Reduction(KNAPSACK from, CLIQUE to) {
        _reductionFrom = from;
        _reductionTo = to;
    }
    public CLIQUE reduce() {
        return new CLIQUE();
    }
}
// return an instance of what you are reducing to

