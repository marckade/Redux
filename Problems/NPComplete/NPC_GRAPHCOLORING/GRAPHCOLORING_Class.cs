using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;

class GRAPHCOLORING : IProblem<GenericSolver, GenericVerifier>{


    // ---- Fields  -----

    private string _problemName = "GRAPHCOLORING";
    private string _formalDefinition = "<G,k> | G is a graph that has a k-coloring";
    private string _problemDefinition = "Vertex coloring problem is defined as given m colors,find a way of coloring the vertices of a graph such that no two adjacent vertices are colored using the same color";


    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{x1,x2,x3,x4}";

    private string _g =  string.Empty;



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

    public String G {
        get{
            return _g;
        }

        set {
            _g = value;
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
    public GRAPHCOLORING() {
      
    }
    public GRAPHCOLORING(string gInput) {
        _g = gInput;
        
    }

    public void ParseProblem(string gInput) {

    }








}