using API.Interfaces;
using API.Problems.NPComplete.NPC_CLIQUE.Solvers;
using API.Problems.NPComplete.NPC_CLIQUE.Verifiers;

namespace API.Problems.NPComplete.NPC_CLIQUE;

class CLIQUE : IProblem<GenericSolver,GenericVerifier> {

    // --- Fields ---
    private string _problemName = "Clique Cover";
    private string _formalDefinition = " ____";
    private string _problemDefinition = "A clique is the problem of uncovering a subset of vertices in an undirected graph G = (V, E) such that every two distinct vertices are adjacent";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{(x1,x2,x3,x4,x5), (x1:x3,x3:x2,x4:x1)}";
    private string _G = string.Empty;
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

    }
    public CLIQUE(string GInput) {
        _G = GInput;
    }

    public void ParseProblem(string phiInput) {

    }
}