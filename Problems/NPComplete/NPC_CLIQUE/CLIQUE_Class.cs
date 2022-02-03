using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;

namespace API.Problems.NPComplete.NPC_CLIQUE;

class CLIQUE : IProblem<GenericSolver,GenericVerifier> {

    // --- Fields ---
    private string _problemName = "Clique";
    private string _formalDefinition = "<G, k> | G is an graph that has a set of k mutually adjacent nodes";
    private string _problemDefinition = "A clique is the problem of uncovering a subset of vertices in an undirected graph G = (V, E) such that every two distinct vertices are adjacent";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "<{a,{b,c}}, {c,{d,e}}, {d,{f}}, {e,{f,g}}), 3>";
    private string _G = string.Empty;
    private List<string> _nodes = new List<string>();
    private Dictionary<string,string> _edges = new Dictionary<string, string>();
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
    public string G {
        get {
            return _G;
        }
        set {
            _G = value;
        }
    }
    public List<string> nodes {
        get {
            return _nodes;
        }
        set {
            _nodes = value;
        }
    }
    public Dictionary<string,string> edges {
        get {
            return _edges;
        }
        set {
            _edges = value;
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
    public CLIQUE() {
        _G = defaultInstance;
    }
    public CLIQUE(string GInput) {
        _G = GInput;
    }

    public List<string> getNodes(string Ginput) {
        return new List<string>();
    }


}