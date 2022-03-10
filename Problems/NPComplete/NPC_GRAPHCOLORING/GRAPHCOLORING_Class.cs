using API.Interfaces;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Solvers;
using API.Problems.NPComplete.NPC_GRAPHCOLORING.Verifiers;

namespace API.Problems.NPComplete.NPC_GRAPHCOLORING;

class GRAPHCOLORING : IProblem<GenericSolver, IgbokwesSimple>{



    #region Fields
    private readonly string _problemName = "GRAPHCOLORING";
    private readonly string _formalDefinition = "{<G,k> | G is a graph that has a k-coloring}";
    private readonly string _problemDefinition = "Graph Coloring is an assignment of labels traditionally called colors to elements of a graph subject to certain constraints.The most common example of graph coloring is the vertex coloring which in its simplest form,  is a way of coloring the vertices of a graph such that no two adjacent vertices are of the same color; this is called a vertex coloring";

    private readonly string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{ { {a,b,c,d,e,f,g,h,i} : { {a,b} & {b,a} & {b,c} & {c, a} & {a,c} & {c,b} & {a,d} & {d,a} & {d,e} & {e, a} & {a,e} & {e,d} & {a,f} & {f,a} & {f,g} & {g, a}&{a,g} & {g,f} & {a,h} & {h,a} & {h,i} & {i, a} & {a,i}  & {i,h}  } } : 3}";

    private string _G =  string.Empty;

    private List<string> _nodes =  new List<string>();

    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();

    private Dictionary<string, string> nodeColoring = new Dictionary<string, string>();

    private HashSet<string> colors = new HashSet<string>();
  


    private int _K;

    private int size;

    private GenericSolver _defaultSolver = new GenericSolver();
    private IgbokwesSimple _defaultVerifier = new IgbokwesSimple();


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

    public String G {
        get{
            return _G;
        }

        set {
            _G = value;
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

    public Dictionary<string, string> NodeColoring {

        get{
            return nodeColoring;
        }

        set {
            nodeColoring = value;
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


        public int Size {
        get {
            return size;
        }
        set {
            size = _edges.Count;
        }
    }
    
    public GenericSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public IgbokwesSimple defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }

    #endregion


    #region Constructors
      public GRAPHCOLORING() {
        _G = defaultInstance;
        nodes = getNodes(_G);
        edges  = getEdges(_G);
        K = getK(_G);
        setColors(K);
      
    }
    public GRAPHCOLORING(string GInput) {
        _G = GInput;
        nodes = getNodes(_G);
        edges  = getEdges(_G);
        K = getK(_G);
        setColors(K);
        
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

        Console.WriteLine("This is the adjacent list ",adjNodes, "\n" );

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



    public int getK(string Ginput) {

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "");

        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        return Int32.Parse(Gsections[2]);
    } 

    public string getNodeColor(string node) {

        Console.WriteLine("This is the current nodes color : "+this.nodeColoring[node]);

        return this.nodeColoring[node];
        
    }

    public void setColors(int K ){ 

        for(int i = 0; i < K; i++){

            this.colors.Add(i.ToString());

        }

    }

    public Boolean validColor(string color){

        Console.WriteLine("is the color valid "+color +" "+this.colors.Contains(color));

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

               Console.WriteLine("This is SIZE OF EDGES "+this._edges.Count);
    
             problem += "{"+ this._edges[i].Key + "," + this._edges[i].Value + "} &";
         

        }

    

        // Parse k
        problem += this._K + "}";
        this._defaultInstance = problem;
        this.G = this._defaultInstance;

    }

    #endregion

  









}