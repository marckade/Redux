using API.Interfaces;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VERTEXCOVER : IProblem<GenericSolver,GenericVerifier>{

    // --- Fields ---
    private string _problemName = "VERTEXCOVER";
    private string _formalDefinition = "<G, k> | G in an undirected graph that has a k-node vertex cover";
    private string _problemDefinition = "A vertex cover is a subset of nodes S, such that every edge in the graph, G, touches a node in S.";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    


    private string _defaultInstance = "<{a,{b,c}}, {c,{d,e}}, {d,{f}}, {e,{f,g}}), 2>";
    private string _Gk = string.Empty;

    
    private GenericSolver _defaultSolver = new GenericSolver();
    private GenericVerifier _defaultVerifier = new GenericVerifier();

    // --- Properties ---
    public string problemName {
        get {
            return _problemName;
        }
    }
    public string formalDefinition {
        get {
            return _formalDefinition;
        }
    }
    public string problemDefinition {
        get {
            return _problemDefinition;
        }
    }

    public string source {
        get {
            return _source;
        }
    }
    public string defaultInstance {
        get {
            return _defaultInstance;
        }
    }
    public string Gk {
        get {
            return _Gk;
        }
        set {
            _Gk = value;
        }
    }

    public GenericSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public GenericVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public VERTEXCOVER() {
        _Gk = defaultInstance; 
    
    }
    public VERTEXCOVER(string GkInput) {
        _Gk = GkInput;
    }

    public void ParseProblem(string GkInput) {

    }
}

