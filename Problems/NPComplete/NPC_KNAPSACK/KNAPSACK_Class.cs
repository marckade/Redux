using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

class KNAPSACK : IProblem<GarrettKnapsackSolver, GarrettVerifier>{

    // --- Fields ---
    private string _problemName = "KNAPSACK";

    private string _formalDefinition = "KNAPSACK = {<H, W> | H is a set of items (w,v) and there is a subset of items in H whose collective weight is less than or equal to W and whose collective value is optimized.}";
    private string _problemDefinition = "The 0-1 KNAPSACK problem is given a knapsack with a maximum capacity W and a set of n items x_1, x_2,... x_n with weights w_1,w_2,... w_n and values v_1,v_2,... v_n optimize the combination of singular items that provide the most value while staying under W. ";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string[] _contributers = { "Garret Stouffer", "Daniel Igbokwe"};
    
    private string _instance = string.Empty;


    private string _defaultInstance = "{{10,20,30},{(10,60),(20,100),(30,120)},50}";
 

    private string _wikiName = "Knapsack";
    private List<string> _nodes =  new List<string>();

    private List<KeyValuePair<string, string>> _items = new List<KeyValuePair<string, string>>();


    private int _W = 0;

    private KnapsackGraph _knapsackGraph;


    private GarrettKnapsackSolver _defaultSolver = new GarrettKnapsackSolver();
    private GarrettVerifier _defaultVerifier = new GarrettVerifier();

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

    public string[] contributers{
        get{
            return _contributers;
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

    public string wikiName {
        get {
            return _wikiName;
        }
    }


    public int W {
        get {
            return _W;
        }
        set {
            _W = value;
        }
    }

          public List<string> nodes {
        get {
            return _nodes;
        }
        set {
            _nodes = value;
        }
    }
    public List<KeyValuePair<string, string>> items {
        get {
            return _items;
        }
        set {
            _items = value;
        }
    }

    
    public GarrettKnapsackSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public GarrettVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public KNAPSACK() {
          
        _knapsackGraph = new KnapsackGraph(_defaultInstance, true);
        _instance  = _knapsackGraph.ToString();
        nodes = _knapsackGraph.nodesStringList;
        items  = _knapsackGraph.edgesKVP;
        _W = _knapsackGraph.K;
   
     
    }
    public KNAPSACK(string HWVInput) {
        _knapsackGraph = new KnapsackGraph(HWVInput, true);
        _instance  = _knapsackGraph.ToString();
        nodes = _knapsackGraph.nodesStringList;
        items  = _knapsackGraph.edgesKVP;
        _W = _knapsackGraph.K;
       
    }

    // public List<KeyValuePair<string, string>> getItems(string HWVInput){
       
    //    List<KeyValuePair<string,string>> allItems = new List<KeyValuePair<string, string>>(); 

    //    string strippedInput = HWVInput.Replace("{", "").Replace("{","").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

    //     //HWVsections[0] is the items and HWVsections[1] is W 
    //    string[] HWVsections = strippedInput.Split(":");
    //    string[] HWVitems = HWVsections[0].Split("&");

    //     foreach(string item in HWVitems) {
    //         string[] fromTo = item.Split(",");
    //         string nodeFrom = fromTo[0];
    //         string nodeTo = fromTo[1];

    //         KeyValuePair<string, string> fullItem = new KeyValuePair<string, string>(nodeFrom, nodeTo);
    //         allItems.Add(fullItem);
    //     }
    //     return allItems;
    // }

    // public int getW(string HWVInput){
        
    //     string strippedInput = HWVInput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

    //     //HWVsections[0] is the items [1] is W and [2] is V.
    //    string[] HWVsections = strippedInput.Split(":");
    //    return Int32.Parse(HWVsections[1]);
    // }



}