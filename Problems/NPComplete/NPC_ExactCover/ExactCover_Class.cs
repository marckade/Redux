using API.Interfaces;
using API.Problems.NPComplete.NPC_ExactCover.Solvers;
using API.Problems.NPComplete.NPC_ExactCover.Verifiers;

namespace API.Problems.NPComplete.NPC_ExactCover;

class ExactCover : IProblem<GenericSolver,GenericVerifier> {

    // --- Fields ---
    private string _problemName = "Exact Cover";
    private string _formalDefinition = "<S, X, S*> | S is a collection of subsets of a set X and S* is a subcollection, exact cover, of S ";
    private string _problemDefinition = "The exact cover problem is a decision problem to determine if an exact cover exists for some <S, X>";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{{ (), (1 & 3), (2 & 3), (2 & 4)} : {1,2,3,4} : {(1 & 3), (2 & 4)}}";
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
    public ExactCover() {
    }
    public ExactCover(string GInput) {
    }


}