using API.Interfaces;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Solvers;
using API.Problems.NPComplete.NPC_VERTEXCOVER.Verifiers;
using API.Interfaces.Graphs;

namespace API.Problems.NPComplete.NPC_VERTEXCOVER;

class VERTEXCOVER : IProblem<VCSolverJanita,VCVerifierJanita>{

    // --- Fields ---
    private string _problemName = "VERTEXCOVER";
    private string _formalDefinition = "VERTEXCOVER = {<G, k> | G in an undirected graph that has a k-node vertex cover}";
    private string _problemDefinition = "A vertex cover is a subset of nodes S, such that every edge in the graph, G, touches a node in S.";
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";
    private string _defaultInstance = "{{a,b,c,d,e,f,g} : {{a,b} & {a,c} & {c,d} & {c,e} & {d,f} & {e,f} & {e,g}} : 3}";
    private string _instance = string.Empty;
    private string _wikiName ="";
    private List<string> _nodes = new List<string>();
    private List<KeyValuePair<string, string>> _edges = new List<KeyValuePair<string, string>>();
    private int _K = 3;
    private VCSolverJanita _defaultSolver = new VCSolverJanita();
    private VCVerifierJanita _defaultVerifier = new VCVerifierJanita();

    private UndirectedGraph _VCAsGraph;
    private string _vertexCover = string.Empty;

    

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

    public int K {
        get {
            return _K;
        }
        set {
            _K = value;
        }
    }

    public VCSolverJanita defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public VCVerifierJanita defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }


    // --- Methods Including Constructors ---
    public VERTEXCOVER() {
        // string VCDefaultString = _defaultInstance;
        // _VCAsGraph = new UndirectedGraph();
        // _vertexCover = _vertexCover.ToString();
        _instance = defaultInstance;
        nodes = getNodes(_instance);
        edges = getEdges(_instance);
        K = getK(_instance);
        _VCAsGraph = new VertexCoverGraph(_defaultInstance);

    }
    public VERTEXCOVER(string instanceInput) {
        // _VCAsGraph = new UndirectedGraph(GkInput);
        // _vertexCover = _VCAsGraph.ToString();
        _instance = instanceInput;
        nodes = getNodes(_instance);
        edges = getEdges(_instance);
        K = getK(_instance);
        _VCAsGraph = new VertexCoverGraph(instanceInput);
    }

    public List<string> getNodes(string Ginput) {
        List<string> allGNodes = new List<string>();
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        string[] Gnodes = Gsections[0].Split(',');
        
        foreach(string node in Gnodes) {
            allGNodes.Add(node);
        }

        return allGNodes;
    }

    public List<KeyValuePair<string, string>> getEdges(string Ginput) {

        List<KeyValuePair<string, string>> allGEdges = new List<KeyValuePair<string, string>>();

        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
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
        string strippedInput = Ginput.Replace("{", "").Replace("}", "").Replace(" ", "").Replace("(", "").Replace(")","");
        
        // [0] is nodes,  [1] is edges,  [2] is k.
        string[] Gsections = strippedInput.Split(':');
        return Int32.Parse(Gsections[2]);
    }
}

