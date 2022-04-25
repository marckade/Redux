using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;


namespace API.Problems.NPComplete.NPC_ExactCover.ReduceTo.NPC_ARCSET;

class NCOV_TO_ARCSETReduction : IReduction<VERTEXCOVER, ARCSET> {

  

    // --- Fields ---
    private string _reductionDefinition = "This Reduction is an implementation of Lawler and Karp's reduction as laid out in Karp's 21 NP_Complete Problems.";
    private string _source = "http://cgi.di.uoa.gr/~sgk/teaching/grad/handouts/karp.pdf"; //Alex NOTE: Change later to real citation.
    private VERTEXCOVER _reductionFrom;
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
    public VERTEXCOVER reductionFrom {
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

        _reductionFrom = new VERTEXCOVER();
        _reductionTo = new ARCSET();
    }
    public NCOV_TO_ARCSETReduction(VERTEXCOVER from, ARCSET to) {
         _reductionFrom = from;
        _reductionTo = reduce();
        
    }
    public ARCSET reduce() {
        UndirectedGraph ug = new UndirectedGraph(_reductionFrom.Gk);
         string dgString = ug.reduction();
        //DirectedGraph dg = new DirectedGraph(dgString);
        ARCSET arcset = new ARCSET(dgString);
        
        return arcset;
    }
}
// return an instance of what you are reducing to