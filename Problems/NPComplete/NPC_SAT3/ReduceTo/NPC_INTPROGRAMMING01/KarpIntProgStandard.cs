using API.Interfaces;
using API.Problems.NPComplete.NPC_INTPROGRAMMING01;

namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_INTPROGRAMMING01;

class KarpIntProgStandard : IReduction<SAT3, INTPROGRAMMING01> {

    // --- Fields ---
    private string _reductionDefinition = "Sipsers reduction converts clauses from 3SAT into clusters of nodes in a graph for which CLIQUES exist";
    private string _source = "Sipser, Michael. Introduction to the Theory of Computation.ACM Sigact News 27.1 (1996): 27-29.";
    private SAT3 _reductionFrom;
    private INTPROGRAMMING01 _reductionTo;


    // --- Properties ---
    public string reductionDefinition {
        get {
            return _reductionDefinition;
        }
    }
    public string source {
        get {
            return _source;
        }
    }
    public SAT3 reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public INTPROGRAMMING01 reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public KarpIntProgStandard(SAT3 from) {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public INTPROGRAMMING01 reduce() {
        SAT3 SAT3Instance = _reductionFrom;
        INTPROGRAMMING01 reduced01INT = new INTPROGRAMMING01();
        
        List<int> dVector = new List<int>();
        List<List<int>> Cmatrix = new List<List<int>>();

        //Creates a list of variable from the list of literals- may need updated if SAT3 is changed to
        //include a variable list.
        List<string> variables = new List<string>();
        foreach(var l in SAT3Instance.literals){
            if(!variables.Contains(l.Replace("!", string.Empty))){
                variables.Add(l.Replace("!", string.Empty));
            }
        }

        //Creates the rows or ineqlalities of the matrix C, for each clause in SAT3
        for(int i=0; i<SAT3Instance.clauses.Count; i++){
            List<int> row = new List<int>();
            dVector.Add(-1);
            for(int j=0; j<variables.Count; j++){
                if(SAT3Instance.clauses[i].Contains(variables[j]) && !SAT3Instance.clauses[i].Contains("!"+variables[j])){
                    row.Add(-1);
                }
                else if(!SAT3Instance.clauses[i].Contains(variables[j]) && SAT3Instance.clauses[i].Contains("!"+variables[j])){
                    row.Add(1);
                }
                else{row.Add(0);}
                //constructs dVector
                if(SAT3Instance.clauses[i].Contains("!"+variables[j])){
                    dVector[i] += 1;
                }
            }
            Cmatrix.Add(row);
        }



        reduced01INT.C = Cmatrix;
        reduced01INT.d = dVector;
        
        //K is the number of clauses
        
        reductionTo = reduced01INT;
        return reduced01INT;
    }
}
// return an instance of what you are reducing to