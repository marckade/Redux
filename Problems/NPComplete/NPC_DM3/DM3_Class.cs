using API.Interfaces;
using API.Problems.NPComplete.NPC_DM3.Solvers;
using API.Problems.NPComplete.NPC_DM3.Verifiers;
using System.Collections;

namespace API.Problems.NPComplete.NPC_DM3;

class DM3 : IProblem<HurkensShrijver,GenericVerifierDM3> {

    // --- Fields ---
    private string _problemName = "3-Dimensional Matching";
    private string _formalDefinition = "{<M,X,Y,Z> | M is a subset of X*Y*Z,|X|=|Y|=|Z| and a subset of M, M', exists, where |M'| = |A|,|B|,|C|, and no two elements of M' agree in any cooridinate}" ;
    private string _problemDefinition = "3-Dimensional Matching is when, given 3 equally sized sets, X, Y, and Z, and a set of constraints M, being a subset of XxYxZ, are you able to select a set of constraints which contain each element of X, Y, and Z in one and only one 3-tuple.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{Paul,Sally,Dave}{Madison,Austin,Bob}{Chloe,Frank,Jake}{Paul,Madison,Chloe}{Paul,Austin,Jake}{Sally,Bob,Chloe}{Sally,Madison,Frank}{Dave,Austin,Chloe}{Dave,Bob,Chloe}"; // simply a list of sets with the elements divided by commas, the first three are asumed to be X, Y, and Z, and all subsequent sets are sets in M
    private string _instance = string.Empty;
    private List<List<List<string>>> _problem;
    private List<string> _X;
    private List<string> _Y;
    private List<string> _Z;
    private List<List<string>> _M;
    private HurkensShrijver _defaultSolver = new HurkensShrijver();
    private GenericVerifierDM3 _defaultVerifier = new GenericVerifierDM3();

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
    public string instance {
        get {
            return _instance;
        }
        set {
            _instance = value;
        }
    }
    public List<string> X {
        get {
            return _X;
        }
        set {
            _X = value;
            _problem[0][0]=_X;
        }
    }
    public List<string> Y {
        get {
            return _Y;
        }
        set {
            _Y = value;
            _problem[0][1]=_Y;
        }
    }
    public List<string> Z {
        get {
            return _Z;
        }
        set {
            _Z = value;
            _problem[0][2]=_Z;
        }
    }
    public List<List<string>> M {
        get {
            return _M;
        }
        set {
            _M = value;
            _problem[1]=_M;
        }
    }
    public List<List<List<string>>> problem {
        get {
            return _problem;
        }
    }
    public HurkensShrijver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public GenericVerifierDM3 defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public DM3() {
        _instance = defaultInstance;
        _problem = ParseProblem(_instance);
        _X = _problem[0][0];
        _Y = _problem[0][1];
        _Z = _problem[0][2];
        _M = _problem[1];
    }
    public DM3(string instanceInput) {
        _instance = instanceInput;
        _problem = ParseProblem(_instance);
        _X = _problem[0][0];
        _Y = _problem[0][1];
        _Z = _problem[0][2];
        _M = _problem[1];
    }

/*************************************************
parseSet(List<string> Set,string instanceInput,int start), is meant to take one set inside of a string, and put it into an array
it is refferenced in ParseProblem, The Set should be an empty List<string>, created by ParseProblem, instanceInput should be 
input of PareseProblem, and start should be the index of the '{' at the begining of the set in the string. It works by
iterating through each charecter from the start index, until it reaches the end of the set '}'. and crestes string of anything
between ','s excluding spaces, and places those strings inside Set.
**************************************************/
    private void parseSet(List<string> Set,string instanceInput,int start){
        int i = start + 1;
        string temp = "";
        while(instanceInput[i]!= '}'){
            if(instanceInput[i] == ','){
                if(temp != ""){Set.Add(temp);}
                temp = "";
                i++;
            }
            else if(instanceInput[i] != ' '){
                temp += instanceInput[i];
                i++;
            }
            else{i++;}
        }
        Set.Add(temp);
        return;
    }
    /*************************************************
   ParseProblem(string instanceInput) takes the string representation of the 3-Dimensional Matching problem, and returns a 
   3 dimensional list, the first depths of list, contains two lists, one with the sets X,Y,and Z, and the other containing all the sets in M.
   ***************************************************/
    public List<List<List<string>>> ParseProblem(string instanceInput) {
        List<List<List<string>>> Problem = new List<List<List<string>>>(){new List<List<string>>(), new List<List<string>>()};
        int setIndex = 0;
        for(int i = 0; i< instanceInput.Length; i++){
            if(instanceInput[i] == '{'){ // at each occurence of {parseSet is called to put each element in the set, divided by commas, into the Problem list.
                List<string> Set = new List<string>();
                parseSet(Set,instanceInput,i);
                Problem[setIndex].Add(Set);
            }
            if(Problem[0].Count == 3){setIndex = 1;}
        }
        return Problem;
    }
}


