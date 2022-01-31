using API.Interfaces;
using API.Problems.NPComplete.NPC_3DM.Solvers;
using API.Problems.NPComplete.NPC_3DM.Verifiers;
using System.Collections;

namespace API.Problems.NPComplete.NPC_3DM;

class THREE_DM : IProblem<GenericSolver,GenericVerifier> {

    // --- Fields ---
    private string _problemName = "3-Dimensional Matching";
    private string _formalDefinition = "<U,A,B,C> | U is a subset of A*B*C,|A|=|B|=|C| and a subset of U, M, exists, where |M| = |A|,|B|,|C|, and no two elements of M agree in any cooridinate" ;
    private string _problemDefinition = "The 3-DImensional Matching problem, is when, given 3 equally sived sets, A, B, and C, and a set of constraints U, which is a subset of AxBxC, are you able to a set of 3-tuples, which contains each element of A, B, and C in one and only one 3-tuple, while following the constraints U. ";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{a1,a2,a3,a4}{b1,b2,b3,b4}{c1,c2,c3,c4}{a1,b2,c1}{a1,b2,c4}{a2,b1,c1}{a2,b1,c2}{a2,b2,c1}{a2,b2,c4}{a2,b4,c3}{a3,b3,c2}{a3,b3,c3}{a4,b1,c1}{a4,b1,c2}"; // simply a list of sets with the elements divided by commas, the first three are asumed to be A, B, and C, and all subsequent sets are sets in U
    private string _phi = string.Empty;
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
    public THREE_DM() {
        _phi = defaultInstance;
    }
    public THREE_DM(string phiInput) {
        _phi = phiInput;
    }

/*************************************************
parseSet(List<string> Set,string phiInput,int start), is meant to take one set inside of a string, and put it into an array
it is refferenced in ParseProblem, The Set should be an empty List<string>, created by ParseProblem, phiInput should be 
input of PareseProblem, and start should be the index of the '{' at the begining of the set in the string. It works by
iterating through each charecter from the start index, until it reaches the end of the set '}'. and crestes string of anything
between ','s excluding spaces, and places those strings inside Set.
**************************************************/
    private void parseSet(List<string> Set,string phiInput,int start){
        int i = start + 1;
        string temp = "";
        while(phiInput[i]!= '}'){
            if(phiInput[i] == ','){
                if(temp != ""){Set.Add(temp);}
                temp = "";
                i++;
            }
            else if(phiInput[i] != ' '){
                temp += phiInput[i];
                i++;
            }
            else{i++;}
        }
        Set.Add(temp);
        return;
    }
    /*************************************************
   ParseProblem(string phiInput) takes the string representation of the 3-Dimensional Matching problem, and returns a 
   3 dimensional list, the first depths of list, contains two lists, one with the sets A,B,and C, and the other containing all the sets in U.
   ***************************************************/
    public List<List<List<string>>> ParseProblem(string phiInput) {
        List<List<List<string>>> Problem = new List<List<List<string>>>(){new List<List<string>>(), new List<List<string>>()};
        int setIndex = 0;
        for(int i = 0; i< phiInput.Length; i++){
            if(phiInput[i] == '{'){ // at each occurence of {parseSet is called to put each element in the set, divided by commas, into the Problem list.
                List<string> Set = new List<string>();
                parseSet(Set,phiInput,i);
                Problem[setIndex].Add(Set);
            }
            if(Problem[0].Count == 3){setIndex = 1;}
        }
        return Problem;
    }
}
