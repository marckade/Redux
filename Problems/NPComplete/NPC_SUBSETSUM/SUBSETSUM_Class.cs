using API.Interfaces;
using API.Problems.NPComplete.NPC_SUBSETSUM.Solvers;
using API.Problems.NPComplete.NPC_SUBSETSUM.Verifiers;

namespace API.Problems.NPComplete.NPC_SUBSETSUM;

class SUBSETSUM : IProblem<GenericSolver,GenericVerifier> {

    // --- Fields ---
    private string _problemName = "Subset Sum";
    private string _formalDefinition = "<G, T> | G is a set of positive integers and there exists a subset of G, K where the sum of K's elements equals T";
    private string _problemDefinition = "The problem is to determine whether there exists a sum of elements that totals to the number T.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{{1,7,12,15} : 28}";
    private string _SS = string.Empty;
    private List<string> _Integers = new List<string>();
    private int _T = 3;
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
    public string SS {
        get {
            return _SS;
        }
        set {
            _SS = value;
        }
    }
    public List<string> Integers {
        get {
            return _Integers;
        }
        set {
            _Integers = value;
        }
    }
    public int T {
        get {
            return _T;
        }
        set {
            _T = value;
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
    public SUBSETSUM() {
        _SS = defaultInstance;
        Integers = getIntegers(_SS);
        T = getT(_SS);
    }
    public SUBSETSUM(string SSInput) {
        _SS = SSInput;
        Integers = getIntegers(_SS);
        T = getT(_SS);
    }
    public List<string> getIntegers(string SSInput) {

        List<string> allIntegers = new List<string>();
        string strippedInput = SSInput.Replace("{", "").Replace("}", "").Replace(" ", "");
        
        // [0] is integers,  [1] is T.
        string[] SSsections = strippedInput.Split(':');
        string[] SSintegers = SSsections[0].Split(',');
        
        foreach(string integer in SSintegers) {
            allIntegers.Add(integer);
        }

        return allIntegers;
    }
    

    public int getT(string SSInput) {
        string strippedInput = SSInput.Replace("{", "").Replace("}", "").Replace(" ", "");
        
        // [0] is integers,  [1] is T.
        string[] SSsections = strippedInput.Split(':');
        
        return Int32.Parse(SSsections[1]);
    }


}