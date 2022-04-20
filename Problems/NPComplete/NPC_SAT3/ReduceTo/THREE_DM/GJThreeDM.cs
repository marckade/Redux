using API.Interfaces;
using API.Problems.NPComplete.NPC_3DM;

namespace API.Problems.NPComplete.NPC_SAT3.ReduceTo.NPC_3DM;

class GareyAndJohnsonReduction : IReduction<SAT3, THREE_DM> {

    // --- Fields ---
    private string _reductionDefinition = "Garey and Johnson Reduction converts 3SAT to a set of elements, and constraints of a 3-dimensional matching problem. ";
    private string _source = "Garey, M. R. and David S. Johnson. “Computers and Intractability: A Guide to the Theory of NP-Completeness.” (1978).";
    private SAT3 _reductionFrom;
    private THREE_DM _reductionTo;


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
    public THREE_DM reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public GareyAndJohnsonReduction(SAT3 from) {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    /***************************************************
     * reduce() called after GareyAndJohnsonReduction reduction, and returns a THREE_DM object, that
     * is a reduction from the SAT3 object passed into GareyAndJohnsonReduction.
     */
    public THREE_DM reduce() {
        SAT3 SAT3Instance = _reductionFrom;
        THREE_DM reduced3DM = new THREE_DM();
        
        List<string> X = new List<string>();
        List<string> Y = new List<string>();
        List<string> Z = new List<string>();
        List<List<string>> M = new List<List<string>>();

        List<string> variables = new List<string>();

        //Creates a list of variable from the list of literals- may need updated if SAT3 is changed to
        //include a variable list.
        foreach(var l in SAT3Instance.literals){
            if(!variables.Contains(l.Replace("!", string.Empty))){
                variables.Add(l.Replace("!", string.Empty));
            }
        }
        int j;
        //adds the variable gadget matchings
        foreach(var literal in variables){
            j = 1;
            foreach(var clause in SAT3Instance.clauses){
                string x1 = "a["+literal+"]["+j+"]";
                string x2 = "a["+literal+"]["+(j+1)+"]";
                string xLast = "a["+literal+"]["+(1)+"]";
                string y = "b["+literal+"]["+j+"]";
                string z1 = "[!"+literal+"]["+j+"]";
                string z2 = "["+literal+"]["+j+"]";

                X.Add(x1);
                Y.Add(y);
                Z.Add(z1);
                Z.Add(z2);
                
                if(j == SAT3Instance.clauses.Count){
                    M.Add(new List<string>(){x1,y,z1});
                    M.Add(new List<string>(){xLast,y,z2});
                }
                else{
                    M.Add(new List<string>(){x1,y,z1});
                    M.Add(new List<string>(){x2,y,z2});
                }
                    //garbage collection for each literal
                for(int k = 1; k<=(SAT3Instance.clauses.Count)*(variables.Count-1);k++){
                    string xg = "g1["+k+"]";
                    string yg = "g2["+k+"]";
                    M.Add(new List<string>(){xg,yg,z2});
                    M.Add(new List<string>(){xg,yg,z1});
                }
                j++;
            }  
        }
        //creation of garbage gadgets for each variable in SAT3, not k = m*(n-1), where m is the number
        //of clauses and n is the nuber of variables.
        for(int k = 1; k<=(SAT3Instance.clauses.Count)*(variables.Count-1);k++){
            X.Add( "g1["+k+"]");
            Y.Add("g2["+k+"]");
        }

        //creates constraints of the clause gadgets
        j = 1;
        foreach(var clause in SAT3Instance.clauses){
            string xc = "s1["+j+"]";
            string yc = "s2["+j+"]";
            X.Add(xc);
            Y.Add(yc);
            for(int i=0; i<3; i++){
                string zc = "["+clause[i]+"]["+j+"]";
                M.Add(new List<string>(){xc,yc,zc});
            }
            j ++;
        }

        
        reduced3DM.X = X;
        reduced3DM.Y = Y;
        reduced3DM.Z = Z;
        reduced3DM.M = M;

        reductionTo = reduced3DM;
        //return new THREE_DM();
        return reduced3DM;
    }
}