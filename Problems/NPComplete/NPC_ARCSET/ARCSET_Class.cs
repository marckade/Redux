using API.Interfaces;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;

namespace API.Problems.NPComplete.NPC_ARCSET;

class ARCSET : IProblem<GenericSolver,GenericVerifier>{

    // --- Fields ---
    private string _problemName = "ARCSET";
    private string _formalDefinition = "ARCSET = {<G,k> | G is a directed graph that can be rendered acyclic by removal of at most k edges}";
     private string _problemDefinition = "ARCSET, or the Feedback Arc Set satisfiability problem, is an NP-complete problem that can be described like the following. Given a directed graph, does removing a given set of edges render the graph acyclical? That is, does removing the edges break every cycle in the graph?";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";

    //ALEX NOTE: The standard mathematical form for a DIGRAPH is A = { x,y,z} r = {(x,y),(y,z),(z,x)} where A is a set of nodes and r is a set of pairs of edges. (r stands for relation)
    private string _defaultInstance = "A = {1,2,3,4} r = {(4,1),(1,2),(4,3),(3,2),(2,4)}";
    private string _phi = string.Empty; 
    private GenericSolver _defaultSolver = new GenericSolver();
    private GenericVerifier _defaultVerifier = new GenericVerifier(); //Verifier needs to implement a Depth First Search. 

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
    public string phi {
        get {
            return _phi;
        }
        set {
            _phi = value;
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
    public ARCSET() {
        _phi = defaultInstance;
    }
    public ARCSET(string phiInput) {
        _phi = phiInput;
    }

    public void ParseProblem(string phiInput) {

    }
}