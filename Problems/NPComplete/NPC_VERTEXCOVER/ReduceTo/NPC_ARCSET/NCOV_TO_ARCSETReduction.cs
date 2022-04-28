using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET;
using API.Problems.NPComplete.NPC_VERTEXCOVER;


namespace API.Problems.NPComplete.NPC_VERTEXCOVER.ReduceTo.NPC_ARCSET;

class NCOV_TO_ARCSETReduction : IReduction<VERTEXCOVER, ARCSET> {

  

    // --- Fields ---
    private string _reductionDefinition = @"This Reduction is an implementation of Lawler and Karp's reduction as laid out in Karp's 21 NP_Complete Problems. 
                                            It takes an instance of an undirected graph (specifically an instance of VERTEXCOVER) and returns an instance of ARCSET (ie. a Directed Graph)
                                            Specifically, a reduction follows the following algorithm: 
                                            For an undirected graph H: Where H is made up of <V,E>
                                            Convert the undirected edges in E to pairs of directed edges. So an undirected edge {{A,B}} turns into the directed pair of edges {(A,B),(B,A)} 
                                            Then turn every node into a pair of nodes denoted by 0 and 1. So a node 'A' turns into the two nodes '<A,0>' and '<A,1>'
                                            Now looks at the pairs of edges in E and maps from 1 to 0. So an edge (A,B) turns into (<A,1>, <B,0>) and edge (B,A) becomes (<B,1>,<A,0>)
                                            Then add directed edges from every 0 node 'u' to 1 node 'u'. ie. creates edges from <A,0> to <A,1>, <B,0> to <B,1> … <Z,0> to <Z,1>
                                            Now the algortithm has created an ARCSET instance (in other words, a Digraph). ";
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