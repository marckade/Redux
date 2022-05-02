using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;

class GRAPHCOLORING : IProblem<IgbokweSolver, IgbokweVerifier>{



    #region Fields
    private readonly string _problemName = "GRAPHCOLORING";
    private readonly string _formalDefinition = "{<G,k> | G is a graph that has a k-coloring}";
    private readonly string _problemDefinition = "An assignment of labels (e.g., colors) to the vertices of a graph such that no two adjacent vertices are of the same label. This is called a vertex coloring.";

    private readonly string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{ { {a,b,c,d,e,f,g,h,i} : { {a,b} & {b,a} & {b,c} & {c, a} & {a,c} & {c,b} & {a,d} & {d,a} & {d,e} & {e, a} & {a,e} & {e,d} & {a,f} & {f,a} & {f,g} & {g, a}&{a,g} & {g,f} & {a,h} & {h,a} & {h,i} & {i, a} & {a,i}  & {i,h}  } } : 3}";

    private string _instance  =  string.Empty;

    private List<string> _nodes =  new List<string>();

    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();

    private Dictionary<string, string> _nodeColoring = new Dictionary<string, string>();

    private SortedSet<string> _colors = new SortedSet<string>(){"0", "1","2"};
  
    private int _K = 3;

    private IgbokweSolver _defaultSolver = new IgbokweSolver();
    private IgbokweVerifier _defaultVerifier = new IgbokweVerifier();


    #endregion


    #region Properties

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

    public String instance  {
        get{
            return _instance ;
        }

        set {
            _instance  = value;
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
    public List<KeyValuePair<string, string>> edges {
        get {
            return _edges;
        }
        set {
            _edges = value;
        }
    }

    public Dictionary<string, string> nodeColoring {

        get{
            return _nodeColoring;
        }

        set {
            _nodeColoring = value;
        }
    }

    public int K {
        get {
            return _K;
        }
        set {
            _K = value;
        }
    }

   

    
    public SortedSet<string> colors {
        get {
            return _colors;
        }
        set {
            _colors = value;
        }
    }
    
    public IgbokweSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public IgbokweVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }

    #endregion


    #region Constructors
      public GRAPHCOLORING() {
        _instance  = defaultInstance;
        nodes = getNodes(_instance );
        edges  = getEdges(_instance );
        K = getK(_instance );
        setColors(K);
        initializeDictionary();
      
    }
    public GRAPHCOLORING(string GInput) {
        _instance  = GInput;
        nodes = getNodes(_instance );
        edges  = getEdges(_instance );
        K = getK(_instance );
        setColors(K);
        initializeDictionary();
        
    }

    #endregion


    #region Methods

    
    public List<string> getNodes(string Ginput){
        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "");

        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gnodes = Gsections[0].Split(',');
        
        foreach(string node in Gnodes) {
            allGNodes.Add(node);
        }

        return allGNodes;
    }

    public List<string> getAdjNodes(string node){

        List<string> adjNodes = new List<string>();

        for(int i = 0; i < this._edges.Count; i++){

            if(this._edges[i].Key.ToLower().Equals(node.ToLower())){

                adjNodes.Add(this._edges[i].Value);
            }

        }

        return adjNodes;
    }



    public List<KeyValuePair<string, string>> getEdges(string Ginput) {

        
        List<KeyValuePair<string, string>> allGEdges = new List<KeyValuePair<string, string>>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "");

        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gedges = Gsections[1].Split('&');
        
        foreach (string edge in Gedges) {
            string[] fromTo = edge.Split(',');
            string nodeFrom = fromTo[0];
            string nodeTo = fromTo[1];
            
            KeyValuePair<string,string> fullEdge = new KeyValuePair<string,string>(nodeFrom, nodeTo);
            allGEdges.Add(fullEdge);
        }

        return allGEdges;
    }


    private void initializeDictionary(){

        foreach(string node in this.nodes){
            this.nodeColoring.Add(node, "-1");
        }
    }




    public int getK(string Ginput) {

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "");

        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        return Int32.Parse(Gsections[2]);
    } 

    public string getNodeColor(string node) {
        return this.nodeColoring[node];
        
    }

    public void setColors(int K ){ 
        for(int i = 0; i < K; i++){
            this.colors.Add(i.ToString());
        }
    }

    public Boolean validColor(string color){
        return this.colors.Contains(color);
    }





    public void parseProblem() {

        string problem = "{{ {";

        // Parse nodes
        for(int i = 0; i < this._nodes.Count - 1; i++){
            problem += this._nodes[i] + ",";
        }
        problem += this._nodes[this._nodes.Count - 1] + "}  : {";

        // Parse edges
        for(int i= 0; i< this._edges.Count -1 ; i++){
            problem += "{"+ this._edges[i].Key + "," + this._edges[i].Value + "} &";
        
        }
        // Parse k
        problem += this._K + "}";
        this._defaultInstance = problem;
        this._instance  = this._defaultInstance;

    }

    #endregion

  




}