using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET;

namespace API.Problems.NPComplete.NPC_ExactCover.ReduceTo.NPC_ARCSET;

class NCOV_TO_ARCSETReduction : IReduction<ExactCover, ARCSET> {

  

    // --- Fields ---
    private string _reductionDefinition = "This Reduction is an implementation of Lawler and Karp's reduction as laid out in Karp's 21 NP_Complete Problems.";
    private string _source = "http://cgi.di.uoa.gr/~sgk/teaching/grad/handouts/karp.pdf"; //Alex NOTE: Change later to real citation.
    private ExactCover _reductionFrom;
    private ARCSET _reductionTo;


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
    public ExactCover reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public ARCSET reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---

    public NCOV_TO_ARCSETReduction(){

        _reductionFrom = new ExactCover();
        _reductionTo = new ARCSET();

    }
    public NCOV_TO_ARCSETReduction(ExactCover from, ARCSET to) {
        _reductionFrom = from;
        _reductionTo = to;
    }
    public ARCSET reduce() {
        

        return new ARCSET();
    }
}
// return an instance of what you are reducing to