using API.Interfaces;
using API.Problems.NPComplete.NPC_KNAPSACK.Solvers;
using API.Problems.NPComplete.NPC_KNAPSACK.Verifiers;

namespace API.Problems.NPComplete.NPC_KNAPSACK;

class KNAPSACK : IProblem<GarrettKnapsackSolver, GarrettsSimple>{

    // --- Fields ---
    private string _problemName = "KNAPSACK";

    private string _formalDefinition = "{<H, W> | H is a set of items (w,v) and there is a subset of items in H whose collective weight is less than or equal to W and whose collective value is optimized.}";
    private string _problemDefinition = "The 0-1 KNAPSACK problem is given a knapsack with a maximum capacity W and a set of n items x_1, x_2,... x_n with weights w_1,w_2,... w_n and values v_1,v_2,... v_n optimize the combination of singular items that provide the most value while staying under W. ";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = " {{(1, 5) & (2,7) & (3, 9) & (1, 7)} : 5}";
    private string _HWV = string.Empty;

    private List<KeyValuePair<String, String>> _items = new List<KeyValuePair<String, String>>();

    private int _W = 0;


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
    public List<KeyValuePair<String, String>> items {
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
    }
    public KNAPSACK(string HWVInput) {
        _HWV = HWVInput;
        items = getItems(_HWV);
        W = getW(_HWV);
    }

    public List<KeyValuePair<string, string>> getItems(string HWVInput){
       
       List<KeyValuePair<string,string>> allItems = new List<KeyValuePair<string, string>>(); 

       string strippedInput = HWVInput.Replace("{", "").Replace("{","").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");

        //HWVsections[0] is the items and HWVsections[1] is W 
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



}