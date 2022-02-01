using API.Interfaces;
using API.Problems.NPComplete.NPC_3DM;

namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_3DM;

class SipserSAT3_3DM_Reduction : IReduction<SAT3, THREE_DM> {

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private SAT3 _reductionFrom;
    private THREE_DM _reductionTo;


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
    public SAT3 reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public THREE_DM reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public SipserSAT3_3DM_Reduction(SAT3 from, THREE_DM to) {
        _reductionFrom = from;
        _reductionTo = to;
    }
    public THREE_DM reduce(SAT3 from, THREE_DM to) {
        return new THREE_DM();
    }
}
// return an instance of what you are reducing to