using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK;

namespace API.Problems.NPComplete.NPC_SUBSETSUM.ReduceTo.NPC_KNAPSACK;

class FengReduction : IReduction<SUBSETSUM, KNAPSACK> {

    // --- Fields ---
    private string _reductionDefinition = "Fengs reduction converts positive integers in SUBSETSUM to items in KNAPSACK";
    private string _source = "Feng, Thomas http://cgm.cs.mcgill.ca/~avis/courses/360/2003/assignments/sol4.pdf";
    private SUBSETSUM _reductionFrom;
    private KNAPSACK _reductionTo;


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
    public SUBSETSUM reductionFrom {
        get {
            return _reductionFrom;
        }
        set {
            _reductionFrom = value;
        }
    }
    public KNAPSACK reductionTo {
        get {
            return _reductionTo;
        }
        set {
            _reductionTo = value;
        }
    }

    // --- Methods Including Constructors ---
    public FengReduction(SUBSETSUM from) {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public KNAPSACK reduce() {
        SUBSETSUM SUBSETSUMInstance = _reductionFrom;
        KNAPSACK reducedKNAPSACK = new KNAPSACK();

        //We reduce by setting T from SUBSETSUM equal to both the minimum value and maxmimum weight constraints. 
        reducedKNAPSACK.W = SUBSETSUMInstance.T;
        reducedKNAPSACK.V = SUBSETSUMInstance.T;
        
        // We reduce the set of integers to a set of items by having each integer n equal (n,n) as an item. 
        List<KeyValuePair<string, string>> Items = new List<KeyValuePair<string, string>>();
        List<string> integers = SUBSETSUMInstance.Integers;
        for(int i=0; i < SUBSETSUMInstance.Integers.Count; i++) {
            KeyValuePair<string, string> item = new KeyValuePair<string, string>(integers[i], integers[i]);
            Items.Add(item);
        }

        reducedKNAPSACK.items = Items;


        reductionTo = reducedKNAPSACK;
        return reducedKNAPSACK;
    }
}
// return an instance of what you are reducing to