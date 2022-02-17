using API.Interfaces;
using API.Problems.NPComplete.NPC_INTPROGRAMMING0_1.Solvers;
using API.Problems.NPComplete.NPC_INTPROGRAMMING0_1.Verifiers;

namespace API.Problems.NPComplete.NPC_INTPROGRAMMING0_1;

class INTPROGRAMMING0_1 : IProblem<GenericSolver,GenericVerifier>{

    // --- Fields ---
    private string _problemName = "0-1 Integer Programming";
    private string _formalDefinition = "{<C,d> | C is an m*n matrix, d is a m-vector, and a n-vector x exicst such that Cx is <= d. }";
    private string _problemDefinition = "0-1 Integer Programming is a system of inequalities, where each variable can be either a 0 or a 1. It is represented by a matrix, where each collumn is a variable, and each row is an inequality. In this implementation the inequality is alway <=. A problem is 0-1 integer programable, if each variable has an assignment of 0 or 1, such that each inequality is satisfiable.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "(-1 1 -1),(0 0 -1),(-1 -1 1)<=(0 0 0)";
    private List<List<int>> _C = new List<List<int>>();
    private List<int> _d = new List<int>();
    private GenericSolver _defaultSolver = new GenericSolver();
    private GenericVerifier _defaultVerifier = new GenericVerifier();
    private string _phi = string.Empty;

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
    public string phi {
        get {
            return _phi;
        }
        set {
            _phi = value;
        }
    }
    public List<List<int>> C {
        get {
            return _C;
        }
        set {
            _C = value;
        }
    }
    public List<int> d {
        get {
            return _d;
        }
        set {
            _d = value;
        }
    }

    
    // --- Methods Including Constructors ---
    public INTPROGRAMMING0_1() {
        _phi = defaultInstance;
        C = getMatrixC(_phi);
        d = getVectorD(_phi);

    }
    public INTPROGRAMMING0_1(string phiInput) {
        // TODO Validate there are only a maximum of 3 literals in each clause
        _phi = phiInput;
        C = getMatrixC(_phi);
        d = getVectorD(_phi);

        
    }

    public List<List<int>> getMatrixC(string phi){
        string strippedPhi = phi.Replace("(","").Replace(")","");
        string[] matrixString = strippedPhi.Split("<=")[0].Split(",");
        List<List<int>> C = new List<List<int>>();
        for(int i=0; i < matrixString.Length; i++){
            string[] stringVariables = matrixString[i].Split(" ");
            List<int> row = new List<int>();
            for(int j=0; j<stringVariables.Length; j++){
                row.Add(int.Parse(stringVariables[j]));
                //row.Add(stringVariables[j]);
            }
            C.Add(row);
        }
        return C;
        
    }

    public List<int> getVectorD(string phi){
        string strippedPhi = phi.Replace("(","").Replace(")","");
        string[] vectorStringArray = strippedPhi.Split("<=")[1].Split(" ");
        List<int> d = new List<int>();
        for(int i=0; i<vectorStringArray.Length; i++){
            d.Add(int.Parse(vectorStringArray[i]));
            //d.Add(vectorStringArray[i]);
        }
        return d;

    }

   

}