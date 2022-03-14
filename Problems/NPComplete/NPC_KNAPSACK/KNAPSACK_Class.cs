using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

class KNAPSACK : IProblem<GarrettKnapsackSolver, GarrettsSimple>{

    // --- Fields ---
    private string _problemName = "KNAPSACK";

    private string _formalDefinition = "{<H, W, V> | H is a set of items (w,v) and there is a subset of items in H whose collective weight is less than or equal to W and whose collective value is greater than or equal to V.}";
    private string _problemDefinition = "The KNAPSACK problem is a problem of determining whether there is a combination of items that provide enough value without going over weight. ";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = " {{(1, 5) & (2,7) & (3, 9) & (1, 7)} : 5 :  20}";
    private string _HWV = string.Empty;

    private List<KeyValuePair<string, string>> _items = new List<KeyValuePair<string, string>>();

    private int _W = 0;

    private int _V = 0;

    private GarrettKnapsackSolver _defaultSolver = new GarrettKnapsackSolver();
    private GarrettsSimple _defaultVerifier = new GarrettsSimple();

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
    public string HWV {
        get {
            return _HWV;
        }
        set {
            _HWV = value;
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

    public int W {
        get {
            return _W;
        }
        set {
            _W = value;
        }
    }
    public int V {
        get {
            return _V;
        }
        set {
            _V = value;
        }
    }
    public GarrettKnapsackSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public GarrettsSimple defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public KNAPSACK() {
        _HWV = defaultInstance;
        items = getItems(_HWV);
        W = getW(_HWV);
        V = getV(_HWV);
    }
    public KNAPSACK(string HWVInput) {
        _HWV = HWVInput;
        items = getItems(_HWV);
        W = getW(_HWV);
        V = getV(_HWV);
    }

    public List<KeyValuePair<string, string>> getItems(string HWVInput){
       
       List<KeyValuePair<string,string>> allItems = new List<KeyValuePair<string, string>>(); 

       string strippedInput = HWVInput.Replace("{", "").Replace("{","").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

        //HWVsections[0] is the items [1] is W and [2] is V.
       string[] HWVsections = strippedInput.Split(":");
       string[] HWVitems = HWVsections[0].Split("&");

        foreach(string item in HWVitems) {
            string[] fromTo = item.Split(",");
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];

            KeyValuePair<string, string> fullItem = new KeyValuePair<string, string>(nodeFrom, nodeTo);
            allItems.Add(fullItem);
        }
        return allItems;
    }

    public int getW(string HWVInput){
        
        string strippedInput = HWVInput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

        //HWVsections[0] is the items [1] is W and [2] is V.
       string[] HWVsections = strippedInput.Split(":");
       return Int32.Parse(HWVsections[1]);
    }

  public int getV(string HWVInput){
        
        string strippedInput = HWVInput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

        //HWVsections[0] is the items [1] is W and [2] is V.
       string[] HWVsections = strippedInput.Split(":");
       return Int32.Parse(HWVsections[2]);
    }

    public void ParseProblem(string HWVInput) {

    }
}