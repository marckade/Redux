using API.Interfaces;
using API.Problems.NPComplete.NPC_SAT3.Solvers;
using API.Problems.NPComplete.NPC_SAT3.Verifiers;

namespace API.Problems.NPComplete.NPC_SAT3;

class SAT3 : IProblem<GenericSolver,KadensSimple>{

    // --- Fields ---
    private string _problemName = "3SAT";
    private string _formalDefinition = "{Φ | Φ is a satisfiabile Boolean forumla in 3CNF}";
    private string _problemDefinition = "3SAT, or the Boolean satisfiability problem, is a problem that asks for a list of assignments to the literals of phi (with a maximum of 3 literals per clause) to result in 'True'";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "(x1 & !x2 & x3) | (!x1 & x3 & x1) | (x2 & !x3 & x1)";
    private GenericSolver _defaultSolver = new GenericSolver();
    private KadensSimple _defaultVerifier = new KadensSimple();
    private string _phi = string.Empty;
    private List<List<string>> _clauses = new List<List<string>>();
    private List<string> _literals = new List<string>();

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
    public KadensSimple defaultVerifier {
        get {
            return _defaultVerifier;
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
    public List<List<string>> clauses {
        get {
            return _clauses;
        }
        set {
            _clauses = value;
        }
    }
    public List<string> literals {
        get {
            return _literals;
        }
        set {
            _literals = value;
        }
    }


    // --- Methods Including Constructors ---
    public SAT3() {
        _phi = defaultInstance;
        clauses = getClauses(_phi);
        literals = getLiterals(_phi);
    }
    public SAT3(string phiInput) {

        // TODO Validate there are only a maximum of 3 literals in each clause

        _phi = phiInput;
        clauses = getClauses(_phi);
        literals = getLiterals(_phi);
    }

    public List<List<string>> getClauses(string phiInput) {
        
        List<List<string>> clauses = new List<List<string>>();

        // Strip extra characters
        string strippedInput = phiInput.Replace(" ", "").Replace("(", "").Replace(")","");

        // Parse on | to collect each clause
        string[] rawClauses = strippedInput.Split('|');

        foreach(string clause in rawClauses) {
            List<string> clauseToAdd = new List<string>();
            string[] literals = clause.Split('&');

            foreach(string literal in literals) {
                clauseToAdd.Add(literal);
            }
            clauses.Add(clauseToAdd);
        }

        return clauses;

    }

    public List<string> getLiterals(string phiInput) {
        
        List<string> literals = new List<string>();
        string strippedInput = phiInput.Replace(" ", "").Replace("(", "").Replace(")","");

        // Parse on | to collect each clause
        string[] rawClauses = strippedInput.Split('|');

        foreach(string clause in rawClauses) {
            string[] rawLiterals = clause.Split('&');

            foreach(string literal in rawLiterals) {
                literals.Add(literal);
            }
        }
        return literals;
    }
}