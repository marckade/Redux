using API.Interfaces;
using API.Interfaces.Graphs;
using API.Problems.NPComplete.NPC_ARCSET.Solvers;
using API.Problems.NPComplete.NPC_ARCSET.Verifiers;
using System;
namespace API.Problems.NPComplete.NPC_ARCSET;

class ARCSET : IProblem<AlexNaiveSolver,AlexArcsetVerifier>{

    // --- Fields ---
    private string _problemName = "ARCSET";
    private string _formalDefinition = "{<G,k> | G is a directed graph that can be rendered acyclic by removal of at most k edges}";
     private string _problemDefinition = "ARCSET, or the Feedback Arc Set satisfiability problem, is an NP-complete problem that can be described like the following. Given a directed graph, does removing a given set of edges render the graph acyclical? That is, does removing the edges break every cycle in the graph?";

    // How we want format
    private string _source = "Karp, Richard M. Reducibility among combinatorial problems. Complexity of computer computations. Springer, Boston, MA, 1972. 85-103.";

    //ALEX NOTE: The standard mathematical form for a DIGRAPH is A = { x,y,z} r = {(x,y),(y,z),(z,x)} where A is a set of nodes and r is a set of pairs of edges. (r stands for relation)
    // G = {A,r} 
    //private string _defaultInstance = "A = {1,2,3,4} r = {(4,1),(1,2),(4,3),(3,2),(2,4)} k = 1";
    private string _defaultInstance = "{{1,2,3,4} : {(4,1) & (1,2) & (4,3) & (3,2) & (2,4)} : 1}";
    private string _instance = string.Empty;

    private string _wikiName ="";
    private ArcsetGraph _arcsetAsGraph;
    private AlexNaiveSolver _defaultSolver = new AlexNaiveSolver();
    private AlexArcsetVerifier _defaultVerifier = new  AlexArcsetVerifier(); //Verifier needs to implement a Depth First Search. 
    

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
    public string wikiName {
        get {
            return _wikiName;
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
    public AlexNaiveSolver defaultSolver {
        get {
            return _defaultSolver;
        }
    }
    public AlexArcsetVerifier defaultVerifier {
        get {
            return _defaultVerifier;
        }
    }
    public ArcsetGraph directedGraph{
        get{
            return _arcsetAsGraph;
        }
    }


    // --- Methods Including Constructors ---
    public ARCSET() {
       // _arcset = defaultInstance;
        //Should be a graph object.

       string arcDefaultString = _defaultInstance;

        _arcsetAsGraph = new ArcsetGraph(_defaultInstance);
        _instance = _arcsetAsGraph.ToString(); 
        _defaultVerifier = new AlexArcsetVerifier();

        
    }
    public ARCSET(string arcInput) {
    
        _arcsetAsGraph = new ArcsetGraph(arcInput);

        //testGraph = {{1,2,3,4} : {[1,2] & [2,3] & [3,1]} : 1}
        //_nCoverAsGraph = new UndirectedGraph(arcInput)
        // _nCoverAsGraph.toString()
        _instance = _arcsetAsGraph.ToString();
        _defaultVerifier = new AlexArcsetVerifier(); 


    }

      
    public void ParseProblem(string arcInput) {


    }


}