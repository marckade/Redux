using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT.Solvers;
using API.Problems.NPComplete.NPC_SAT.Verifiers;



namespace Redux.Problems.NPComplete.NPC_SAT;

    public class SAT : IProblem<GenericSolver, IgbokweSATVerifier> {


    #region Fields

    // --- Fields ---
    private string _problemName = "SAT";
    private string _formalDefinition = "PHI | PHI is a satisfiabile Boolean forumla in 3CNF";
    private string _problemDefinition = "SAT, or the Boolean satisfiability problem, is a problem that asks what is the fastest algorithm to tell for a given formula in Boolean algebra (with unknown number of variables) whether it is satisfiable, that is, whether there is some combination of the (binary) values of the variables that will give 1";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "(x1,x2*,x3) ^ (x1*,x3,x1) ^ (x2,x3*,x1)";
    private string _instance = string.Empty;
   
    private GenericSolver _defaultSolver = new GenericSolver();
    private IgbokweSATVerifier _defaultVerifier = new IgbokweSATVerifier();

    #endregion


    #region Properties
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

    public String instance  {
        get{
            return _instance ;
        }

        set {
            _instance  = value;
        }
    }

    public IgbokweSATVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    #endregion

    #region Constructors
    // --- Methods Including Constructors ---
    public SAT() {
        _instance = defaultInstance;
        _defaultSolver = new GenericSolver(this);
    }
    public SAT(string phiInput) {
        _instance = phiInput;
        _defaultSolver = new GenericSolver(this);
    }

    #endregion


    #region Methods

    public void ParseProblem(string phiInput) {
    }

    #endregion
        
}

