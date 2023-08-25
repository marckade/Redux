using API.Interfaces;
using API.Problems.NPComplete.NPC_PARTITION;

namespace API.Problems.NPComplete.NPC_KNAPSACK.ReduceTo.NPC_PARTITION;

class PARTITIONReduction : IReduction<KNAPSACK, PARTITION>
{

    // --- Fields ---
    private string _reductionName = "PARTITION Reduction";
    private string _reductionDefinition = "Karp's Reduction from Graph Coloring to Clique Cover";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string[] _contributers = { "Andrija Sevaljevic" };

    private string _complexity = "";
    private Dictionary<Object, Object> _gadgetMap = new Dictionary<Object, Object>();

    private KNAPSACK _reductionFrom;
    private PARTITION _reductionTo;


    // --- Properties ---
    public string reductionName
    {
        get
        {
            return _reductionName;
        }
    }
    public string reductionDefinition
    {
        get
        {
            return _reductionDefinition;
        }
    }
    public string source
    {
        get
        {
            return _source;
        }
    }
    public string[] contributers
    {
        get
        {
            return _contributers;
        }
    }
    public Dictionary<Object, Object> gadgetMap
    {
        get
        {
            return _gadgetMap;
        }
        set
        {
            _gadgetMap = value;
        }
    }
    public KNAPSACK reductionFrom
    {
        get
        {
            return _reductionFrom;
        }
        set
        {
            _reductionFrom = value;
        }
    }
    public PARTITION reductionTo
    {
        get
        {
            return _reductionTo;
        }
        set
        {
            _reductionTo = value;
        }
    }



    // --- Methods Including Constructors ---
    public PARTITIONReduction(KNAPSACK from)
    {
        _reductionFrom = from;
        _reductionTo = reduce();

    }
    public PARTITION reduce()
    {
        KNAPSACK KNAPSACKInstance = _reductionFrom;
        PARTITION reducedPARTITION = new PARTITION();

        List<string> nodes = new List<string>();
        string instance = "{";
        int sum = 0;

        foreach(var i in reductionFrom.nodes) {
            instance += i + ",";
            sum += Int32.Parse(i);
        }
        instance += (reductionFrom.W + 1).ToString() + "," + (sum + 1 - reductionFrom.W) + "}";

        reducedPARTITION.S = instance.Replace("{","").Replace("}","").Split(',').ToList();
        reducedPARTITION.instance = instance;

        reductionTo = reducedPARTITION;
        return reducedPARTITION;
    }

    public string mapSolutions(KNAPSACK reductionFrom, PARTITION problemTo, string reductionFromSolution)
    {
        if (!reductionFrom.defaultVerifier.verify(reductionFrom, reductionFromSolution))
        {
            return "Solution is incorect";
        }

        return false.ToString();




    }
}
// return an instance of what you are reducing to