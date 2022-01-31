using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

class KNAPSACK : IProblem<GenericSolver,GenericVerifier>{

    // --- Fields ---
    private string _problemName = "KNAPSACK";

    private string _formalDefinition = "{<H, W, V> | H is a set of items (w,v) and there is a subset of items in H whose collective weight is less than or equal to W and whose collective value is greater than or equal to V.}";
    private string _problemDefinition = "The KNAPSACK problem is a problem of determining whether there is a combination of items that provide enough value without going over weight. ";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = " ({(5, 6), (7,4), (8, 5), (10, 8)}, 24,  16)";
    private string _HWV = string.Empty;
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
    public string HWV {
        get {
            return _HWV;
        }
        set {
            _HWV = value;
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
    public KNAPSACK() {
        _HWV = defaultInstance;
    }
    public KNAPSACK(string HWVInput) {
        _HWV = HWVInput;
    }

    public void ParseProblem(string HWVInput) {

    }
}